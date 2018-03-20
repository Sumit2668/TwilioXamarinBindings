# Twilio Xamarin Bindings

Twilio Bindings for Xamarin apps

### iOS
[![NuGet][ios-video-nuget-img]][ios-video-nuget-link]
### Android
[![NuGet][android-video-nuget-img]][android-video-nuget-link]

## How to Build

### iOS
You will need to download Twilio.Video iOS native SDK from https://www.twilio.com/docs/api/video/download-video-sdks#ios-sdk and copy TwilioVideo.framework folder to source\Twilio.Video.iOS\Native References directory

### Android
Download aar/jar version you needed from https://bintray.com/twilio/releases/video-android and copy it to source\TwilioVideo.Android\Jars

#### Proguard settings

-keep class com.getkeepsafe.relinker.** { *; }

-keep class com.webrtc.** { *; }

-keep class com.webrtc.voiceengine.** { *; }

-keepclassmembers class com.webrtc.** { *; }

-keepclassmembernames class com.webrtc.** { *; }

-keepattributes InnerClasses

[ios-video-nuget-img]: https://img.shields.io/badge/nuget-1.3.8-blue.svg
[ios-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.XamarinBinding
[android-video-nuget-img]: https://img.shields.io/badge/nuget-1.3.13-blue.svg
[android-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.Android.XamarinBinding

## Sample

#### Check official Twilio quickstart guides for samples

[TwilioParticipantDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioParticipantDelegate.cs)

[TwilioRoomDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioRoomDelegate.cs)

[TwilioVideoViewDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioVideoViewDelegate.cs)
