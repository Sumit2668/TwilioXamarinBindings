# Twilio Xamarin Bindings

Twilio Bindings for Xamarin apps

### iOS
Twilio.Video 1.3.8 [![NuGet][video-nuget-img]][video-nuget-link]
### Android
Twilio.Video 1.0.1 - will be added soon

## How to Build

### iOS
You will need to download Twilio.Video iOS native SDK from https://www.twilio.com/docs/api/video/download-video-sdks#ios-sdk and copy TwilioVideo.framework folder to source\Twilio.Video.iOS\Native References directory

### Android
Download jar version you needed from https://bintray.com/twilio/releases/video-android and copy it to source\Twilio.Video.Android\Jars

[video-nuget-img]: https://img.shields.io/badge/nuget-1.3.8-blue.svg
[video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.XamarinBinding

## Sample

[TwilioParticipantDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioParticipantDelegate.cs)

[TwilioRoomDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioRoomDelegate.cs)

[TwilioVideoViewDelegate.cs](sample/Twilio.Video.Sample.iOS/TwilioVideoViewDelegate.cs)
