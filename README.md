# Twilio SDKs Xamarin Bindings

Twilio SDKs Bindings for Xamarin apps

[![NuGet][ios-video-nuget-img]][ios-video-nuget-link]
[![NuGet][ios-voice-nuget-img]][ios-voice-nuget-link]
[![NuGet][android-video-nuget-img]][android-video-nuget-link]

[ios-video-nuget-img]: https://img.shields.io/badge/nuget-1.3.8-blue.svg?label=Twilio.Video.iOS%20NuGet
[ios-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.XamarinBinding
[ios-voice-nuget-img]: https://img.shields.io/badge/nuget-2.0.5-blue.svg?label=Twilio.Voice.iOS%20NuGet
[ios-voice-nuget-link]: https://github.com/dkornev/TwilioXamarinBindings
[android-video-nuget-img]: https://img.shields.io/badge/nuget-1.3.13-blue.svg?label=Twilio.Video.Android%20NuGet
[android-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.Android.XamarinBinding

## How to Build

### Twilio.Video iOS (2.* update wip)
You will need to download Twilio.Video iOS native SDK from https://www.twilio.com/docs/api/video/download-video-sdks#ios-sdk and copy TwilioVideo.framework folder to source\Twilio.Video.iOS\Native References directory

### Twilio.Voice iOS 2.0.5 (August 3, 2018)
Download Programmable Voice iOS SDK 2.0.5 static library from https://media.twiliocdn.com/sdk/ios/voice/releases/2.0.5/twilio-voice-ios-static-2.0.5.tar.bz2 and copy libTwilioVoice.a library into the source/Twilio.Voice.iOS directory

### Twilio.Video Android (2.* update wip)
Download aar/jar version you needed from https://bintray.com/twilio/releases/video-android and copy it to source\TwilioVideo.Android\Jars

#### Proguard settings

-keep class com.getkeepsafe.relinker.** { *; }

-keep class com.webrtc.** { *; }

-keep class com.webrtc.voiceengine.** { *; }

-keepclassmembers class com.webrtc.** { *; }

-keepclassmembernames class com.webrtc.** { *; }

-keepattributes InnerClasses

## Sample

#### Check official Twilio quickstart guides for samples

[TwilioParticipantDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioParticipantDelegate.cs)

[TwilioRoomDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioRoomDelegate.cs)

[TwilioVideoViewDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioVideoViewDelegate.cs)
