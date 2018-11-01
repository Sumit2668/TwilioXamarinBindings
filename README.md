# Twilio SDKs Xamarin Bindings

Twilio SDKs Bindings for Xamarin apps

[![NuGet][ios-video-nuget-img]][ios-video-nuget-link]
[![NuGet][ios-voice-nuget-img]][ios-voice-nuget-link]
[![NuGet][android-video-nuget-img]][android-video-nuget-link]
[![NuGet][android-voice-nuget-img]][android-voice-nuget-link]

[ios-video-nuget-img]: https://img.shields.io/badge/Twilio.Video.iOS%20NuGet-1.4.2-blue.svg 
[ios-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.XamarinBinding 
[ios-voice-nuget-img]: https://img.shields.io/badge/Twilio.Voice.iOS%20NuGet-2.0.5-blue.svg  
[ios-voice-nuget-link]: https://www.nuget.org/packages/Twilio.Voice.iOS.XamarinBinding 
[android-video-nuget-img]: https://img.shields.io/badge/Twilio.Video.Android%20NuGet-2.2.1-blue.svg  
[android-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.Android.XamarinBinding 
[android-voice-nuget-img]: https://img.shields.io/badge/Twilio.Voice.Android%20NuGet-2.0.9-blue.svg 
[android-voice-nuget-link]: https://www.nuget.org/packages/Twilio.Voice.Android.XamarinBinding 

## How to Build

### Twilio.Video iOS 1.4.2 (2.* update wip, help wanted)
Download Twilio Video SDK iOS 1.4.2 https://github.com/twilio/twilio-video-ios/releases/download/1.4.2/TwilioVideo.framework.zip and copy TwilioVideo.framework into the source\Twilio.Video.iOS\Native References directory

Add --registrar:static as additional mtouch arguments on iOS Build dialog for your iOS application


### Twilio.Voice iOS 2.0.5 (August 3, 2018)
Download Programmable Voice iOS SDK 2.0.5 static library from https://media.twiliocdn.com/sdk/ios/voice/releases/2.0.5/twilio-voice-ios-static-2.0.5.tar.bz2 and copy libTwilioVoice.a library into the source/Twilio.Voice.iOS directory

Add --registrar:static as additional mtouch arguments on iOS Build dialog for your iOS application


### Twilio.Video Android 2.2.1 (September 13, 2018)
Download aar/jar version you needed from https://bintray.com/twilio/releases/video-android and copy it to source\TwilioVideo.Android\Jars

##### Proguard settings

-keep class com.getkeepsafe.relinker.** { *; }

-keep class com.webrtc.** { *; }

-keep class com.webrtc.voiceengine.** { *; }

-keepclassmembers class com.webrtc.** { *; }

-keepclassmembernames class com.webrtc.** { *; }

-keepattributes InnerClasses


### Twilio.Video Android 2.0.9 (September 7, 2018)
Download aar/jar version you needed from https://bintray.com/twilio/releases/voice-android and copy it to source\TwilioVideo.Android\Jars

##### Proguard settings

-keep class com.twilio.** { *; }

-keepattributes InnerClasses


## Sample

#### I don't update xamarin sample projects often, so I highly recommend you to read about using native library bindings for xamarin and you check official Twilio quickstart guides.

[TwilioParticipantDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioParticipantDelegate.cs)

[TwilioRoomDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioRoomDelegate.cs)

[TwilioVideoViewDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioVideoViewDelegate.cs)


### Contributions
Members of the community have contributed to improving and update bindings:

 - @codeanees

If you have a bugfix or an update you'd like to add, please open an issue. 
All pull requests should be opened against the `master` branch.
