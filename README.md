# Twilio SDKs Xamarin Bindings

Twilio SDKs Bindings for Xamarin apps

[![NuGet][ios-video-nuget-img]][ios-video-nuget-link]
[![NuGet][ios-voice-nuget-img]][ios-voice-nuget-link]
[![NuGet][android-video-nuget-img]][android-video-nuget-link]
[![NuGet][android-voice-nuget-img]][android-voice-nuget-link]

[ios-video-nuget-img]: https://img.shields.io/badge/Twilio.Video.iOS%20NuGet-2.6.0-blue.svg 
[ios-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.XamarinBinding 
[ios-voice-nuget-img]: https://img.shields.io/badge/Twilio.Voice.iOS%20NuGet-2.0.5-blue.svg  
[ios-voice-nuget-link]: https://www.nuget.org/packages/Twilio.Voice.iOS.XamarinBinding 
[android-video-nuget-img]: https://img.shields.io/badge/Twilio.Video.Android%20NuGet-2.2.1-blue.svg  
[android-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.Android.XamarinBinding 
[android-voice-nuget-img]: https://img.shields.io/badge/Twilio.Voice.Android%20NuGet-2.0.9-blue.svg 
[android-voice-nuget-link]: https://www.nuget.org/packages/Twilio.Voice.Android.XamarinBinding 

## How to Build

### Twilio.Video iOS 2.6.0 (December 19, 2018)
```
sh bootstrapper.sh
```

or

Download Twilio Video SDK iOS 2.6.0  https://github.com/twilio/twilio-video-ios/releases/download/2.6.0/TwilioVideo.framework.zip and copy TwilioVideo.framework into the source\Twilio.Video.iOS\Native References directory

Add --registrar:static as additional mtouch arguments on iOS Build dialog for your iOS application


### Twilio.Voice iOS 2.0.5 (August 3, 2018)
```
sh bootstrapper.sh
```

or

Download Programmable Voice iOS SDK 2.0.5 static library from https://media.twiliocdn.com/sdk/ios/voice/releases/2.0.5/twilio-voice-ios-static-2.0.5.tar.bz2 and copy libTwilioVoice.a library into the source/Twilio.Voice.iOS directory

Add --registrar:static as additional mtouch arguments on iOS Build dialog for your iOS application


### Twilio.Video Android 2.2.1 (September 13, 2018)
```
The aar is already included into this repostiory. So just build the project.
```

or

Download aar version you needed from https://bintray.com/twilio/releases/video-android and copy it to source\Twilio.Video.Android\Jars; Then you will need to change res/values/values.xml and add missing \<attr format="boolean" name="overlaySurface"/> attribute there.
```
$ unzip video-android-2.2.1.aar -d tempFolder
# Change whatever you need
$ jar cvf video-android-2.2.1.aar -C tempFolder/ .
```

##### Proguard settings

-keep class com.getkeepsafe.relinker.** { *; }

-keep class org.webrtc.** { *; }

-keep class com.twilio.video.** { *; }

-keep class com.twilio.common.** { *; }

-keepattributes InnerClasses


### Twilio.Voice Android 2.0.9 (September 7, 2018)
```
The aar is already included into this repostiory. So just build the project.
```

or

Download aar/jar version you needed from https://bintray.com/twilio/releases/voice-android and copy it to source\Twilio.Voice.Android\Jars

##### Proguard settings

-keep class com.twilio.** { *; }

-keepattributes InnerClasses


## Sample

#### I don't update xamarin sample projects often, so I highly recommend you to read about using native library bindings for xamarin and check official Twilio quickstart guides.

[Twilio.Voice.Sample](sample/Twilio.Voice.Sample.iOS)

[Twilio.Video.Sample](sample/Twilio.Video.Sample.iOS)


### Contributions
Members of the community have contributed to improving and update bindings:

 - none

If you have a bugfix or an update you'd like to add, please open an issue. 
All pull requests should be opened against the `master` branch.
