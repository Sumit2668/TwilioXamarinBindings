# Twilio SDKs Xamarin Bindings

Twilio SDKs Bindings for Xamarin apps

> NOTE: New version of Twilio.Voice.iOS binding is pushed to a separate [repository](https://github.com/dkornev/TwilioVoiceXamarinIOS) 
> NOTE: New version of Twilio.Video.iOS binding is pushed to a separate [repository](https://github.com/dkornev/TwilioVideoXamarinIOS) 

[![NuGet][ios-video-nuget-img]][ios-video-nuget-link]
[![NuGet][ios-voice-nuget-img]][ios-voice-nuget-link]
[![NuGet][android-video-nuget-img]][android-video-nuget-link]
[![NuGet][android-voice-nuget-img]][android-voice-nuget-link]

[ios-video-nuget-img]: https://img.shields.io/badge/Twilio.Video.iOS%20NuGet-blue.svg 
[ios-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.XamarinBinding 
[ios-voice-nuget-img]: https://img.shields.io/badge/Twilio.Voice.iOS%20NuGet-blue.svg  
[ios-voice-nuget-link]: https://www.nuget.org/packages/Twilio.Voice.iOS.XamarinBinding 
[android-video-nuget-img]: https://img.shields.io/badge/Twilio.Video.Android%20NuGet-3.2.2-blue.svg  
[android-video-nuget-link]: https://www.nuget.org/packages/Twilio.Video.Android.XamarinBinding 
[android-voice-nuget-img]: https://img.shields.io/badge/Twilio.Voice.Android%20NuGet-2.0.9-blue.svg 
[android-voice-nuget-link]: https://www.nuget.org/packages/Twilio.Voice.Android.XamarinBinding 

## How to Build

### Twilio.Video iOS
> NOTE: New version of Twilio.Video.iOS binding is pushed to a separate [repository](https://github.com/dkornev/TwilioVideoXamarinIOS) 


### Twilio.Voice iOS
> NOTE: New version of Twilio.Voice.iOS binding is pushed to a separate [repository](https://github.com/dkornev/TwilioVoiceXamarinIOS) 

### Twilio.Video Android 3.2.2 (January 8th, 2019)
```
The aar is already included into this repostiory. So just build the project.
```

or

Download aar version you needed from https://bintray.com/twilio/releases/video-android and copy it to source\Twilio.Video.Android\Jars; Then you will need to change res/values/values.xml and add missing \<attr format="boolean" name="overlaySurface"/> attribute there.	Download aar version you needed from https://bintray.com/twilio/releases/video-android and copy it to source\Twilio.Video.Android\Jars
```	
$ unzip video-android-3.2.2.aar -d tempFolder	
# Change whatever you need	
$ jar cvf video-android-3.2.2.aar -C tempFolder/ .	
```

VS 2019 is recommended.

##### Proguard settings

-keep class com.getkeepsafe.relinker.** { *; }

-keep class org.webrtc.** { *; }

-keep class com.twilio.video.** { *; }

-keep class com.twilio.common.** { *; }

-keep class twiliovideo.** { *; }

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


### Contributions
Members of the community have contributed to improving and update bindings:

 - ksavidetove

If you have a bugfix or an update you'd like to add, please open an issue. 
All pull requests should be opened against the `master` branch.
