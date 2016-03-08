#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

// http://media.twiliocdn.com/sdk/maven/com/twilio/sdk/twilio-common-android/maven-metadata.xml
var TWILIO_COMMON_ANDROID_VERSION = "0.2.0";
var TWILIO_COMMON_ANDROID = string.Format ("http://media.twiliocdn.com/sdk/maven/com/twilio/sdk/twilio-common-android/{0}/twilio-common-android-{0}.jar", TWILIO_COMMON_ANDROID_VERSION);
var TWILIO_IPMESSAGING_ANDROID = "https://media.twiliocdn.com/sdk/android/ip-messaging/v0.4/twilio-ip-messaging-android.tar.bz2";
var TWILIO_VIDEO_ANDROID_VERSION = "0.8.1";
var TWILIO_VIDEO_ANDROID = string.Format ("https://bintray.com/artifact/download/twilio/releases/com/twilio/conversations-android/{0}/conversations-android-{0}.aar", TWILIO_VIDEO_ANDROID_VERSION);

var TWILIO_PODSPEC = new [] { 
	"source 'https://github.com/twilio/cocoapod-specs'",
	"platform :ios, '8.1'",
	"pod 'TwilioIPMessagingClient', '0.13.6'",
	"pod 'TwilioCommon', '0.2.0'",
	"pod 'TwilioConversationsClient', '0.21.6'",
};

var TARGET = Argument ("target", Argument ("t", "lib"));

Task ("libs").IsDependentOn ("externals").Does (() => 
{
	NuGetRestore ("./Twilio.sln");
	DotNetBuild ("./Twilio.sln");
});

Task ("samples").IsDependentOn ("libs").Does (() => 
{
	var sampleSlns = new [] { 
		"./samples/TwilioIPMessagingSampleAndroid.sln",
		"./samples/TwilioIPMessagingSampleiOS.sln",
		"./samples/TwilioConversationsSampleAndroid.sln",
		"./samples/TwilioConversationsSampleiOS.sln",
	};

	foreach (var sln in sampleSlns) {
		NuGetRestore (sln);
		DotNetBuild (sln);
	}
});

Task ("externals-android")
	.WithCriteria (!FileExists ("./externals/android/twilio-common-android.jar"))
	.Does (() => 
{
	if (!DirectoryExists ("./externals/android"))
		CreateDirectory ("./externals/android");

	DownloadFile (TWILIO_COMMON_ANDROID, "./externals/android/twilio-common-android.jar");
	
	DownloadFile (TWILIO_VIDEO_ANDROID, "./externals/android/twilio-conversations-android.aar");

	DownloadFile (TWILIO_IPMESSAGING_ANDROID, "./externals/android/twilio-ip-messaging-android.tar.bz2");
	StartProcess ("tar", "-xvzf ./externals/android/twilio-ip-messaging-android.tar.bz2 -C ./externals/android/");
});
Task ("externals-ios")
	.WithCriteria (!FileExists ("./externals/ios/libTwilioCommon.a"))
	.Does (() => 
{
	if (!DirectoryExists ("./externals/ios"))
		CreateDirectory ("./externals/ios");

	FileWriteLines ("./externals/ios/Podfile", TWILIO_PODSPEC);
	CocoaPodInstall ("./externals/ios", new CocoaPodInstallSettings { NoIntegrate = true });

	CopyFile ("./externals/ios/Pods/TwilioCommon/TwilioCommon.framework/Versions/A/TwilioCommon", 
		"./externals/ios/libTwilioCommon.a");
	CopyFile ("./externals/ios/Pods/TwilioIPMessagingClient/TwilioIPMessagingClient.framework/Versions/A/TwilioIPMessagingClient", 
		"./externals/ios/libTwilioIPMessagingClient.a");
	CopyFile ("./externals/ios/Pods/TwilioConversationsClient/TwilioConversationsClient.framework/Versions/A/TwilioConversationsClient", 
		"./externals/ios/libTwilioConversationsClient.a");
});
Task ("externals").IsDependentOn ("externals-android").IsDependentOn ("externals-ios");

Task ("clean").Does (() => 
{
	if (DirectoryExists ("./externals"))
		DeleteDirectory ("./externals", true);

	CleanDirectories ("./**/bin");
	CleanDirectories ("./**/obj");

	if (DirectoryExists ("./tools"))
		DeleteDirectory ("./tools", true);
});

RunTarget (TARGET);
