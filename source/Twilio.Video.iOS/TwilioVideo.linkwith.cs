using System;
using ObjCRuntime;

[assembly: LinkWith ("TwilioVideo.a",
    LinkTarget.ArmV7 | LinkTarget.x86_64 | LinkTarget.ArmV7s | LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.Arm64 | LinkTarget.i386,
    Frameworks = "AudioToolbox VideoToolbox AVFoundation CoreTelephony GLKit CoreMedia SystemConfiguration",
    LinkerFlags = "-ObjC -lstdc++ -lc++ -lz -dead_strip",
    IsCxx = true,
    SmartLink = true,
    ForceLoad = true)]