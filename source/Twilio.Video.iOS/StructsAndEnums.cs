using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using ObjCRuntime;

namespace Twilio.Video
{
    [Native]
    public enum TVIAudioOutput : nuint
    {
        ideoChatDefault = 0,
        ideoChatSpeaker,
        oiceChatDefault,
        oiceChatSpeaker
    }

    [Native]
    public enum TVITrackState : nuint
    {
        Ended = 0,
        Live
    }

    [Native]
    public enum TVIVideoOrientation : nuint
    {
        Up = 0,
        Left,
        Down,
        Right
    }

    static class CFunctions
    {
        // CGAffineTransform TVIVideoOrientationMakeTransform (TVIVideoOrientation orientation);
        [DllImport("__Internal")]
        [Verify(PlatformInvoke)]
        static extern CGAffineTransform TVIVideoOrientationMakeTransform(TVIVideoOrientation orientation);

        // BOOL TVIVideoOrientationIsRotated (TVIVideoOrientation orientation);
        [DllImport("__Internal")]
        [Verify(PlatformInvoke)]
        static extern bool TVIVideoOrientationIsRotated(TVIVideoOrientation orientation);

        // TVIAspectRatio TVIAspectRatioMake (NSUInteger numerator, NSUInteger denominator);
        [DllImport("__Internal")]
        [Verify(PlatformInvoke)]
        static extern TVIAspectRatio TVIAspectRatioMake(nuint numerator, nuint denominator);
    }

    public enum TVIPixelFormat : uint
    {
        TVIPixelFormat32ARGB = kCVPixelFormatType_32ARGB,
        TVIPixelFormat32BGRA = kCVPixelFormatType_32BGRA,
        YUV420BiPlanarVideoRange = kCVPixelFormatType_420YpCbCr8BiPlanarVideoRange,
        YUV420BiPlanarFullRange = kCVPixelFormatType_420YpCbCr8BiPlanarFullRange,
        YUV420PlanarVideoRange = kCVPixelFormatType_420YpCbCr8Planar,
        YUV420PlanarFullRange = kCVPixelFormatType_420YpCbCr8PlanarFullRange
    }

    [Native]
    public enum TVIVideoCaptureSource : nuint
    {
        FrontCamera = 0,
        BackCameraWide,
        BackCameraTelephoto
    }

    [Native]
    public enum TVIError : nuint
    {
        Unknown = 0,
        AccessTokenInvalidError = 20101,
        AccessTokenHeaderInvalidError = 20102,
        AccessTokenIssuerInvalidError = 20103,
        AccessTokenExpiredError = 20104,
        AccessTokenNotYetValidError = 20105,
        AccessTokenGrantsInvalidError = 20106,
        AccessTokenSignatureInvalidError = 20107,
        SignalingConnectionError = 53000,
        SignalingConnectionDisconnectedError = 53001,
        SignalingConnectionTimeoutError = 53002,
        SignalingIncomingMessageInvalidError = 53003,
        SignalingOutgoingMessageInvalidError = 53004,
        RoomNameInvalidError = 53100,
        RoomNameTooLongError = 53101,
        RoomNameCharsInvalidError = 53102,
        RoomCreateFailedError = 53103,
        RoomConnectFailedError = 53104,
        RoomMaxParticipantsExceededError = 53105,
        RoomNotFoundError = 53106,
        RoomMaxParticipantsOutOfRangeError = 53107,
        RoomTypeInvalidError = 53108,
        RoomTimeoutOutOfRangeError = 53109,
        RoomStatusCallbackMethodInvalidError = 53110,
        RoomStatusCallbackInvalidError = 53111,
        ParticipantIdentityInvalidError = 53200,
        ParticipantIdentityTooLongError = 53201,
        ParticipantIdentityCharsInvalidError = 53202,
        ParticipantMaxTracksExceededError = 53203,
        ParticipantNotFoundError = 53204,
        TrackInvalidError = 53300,
        TrackNameInvalidError = 53301,
        TrackNameTooLongError = 53302,
        TrackNameCharsInvalidError = 53303,
        MediaClientLocalDescFailedError = 53400,
        MediaServerLocalDescFailedError = 53401,
        MediaClientRemoteDescFailedError = 53402,
        MediaServerRemoteDescFailedError = 53403,
        MediaNoSupportedCodecError = 53404,
        MediaConnectionError = 53405,
        ConfigurationAcquireFailedError = 53500,
        ConfigurationAcquireTurnFailedError = 53501
    }

    [Native]
    public enum TVIIceTransportPolicy : nuint
    {
        All = 0,
        Relay = 1
    }

    [Native]
    public enum TVIRoomState : nuint
    {
        Connecting = 0,
        Connected,
        Disconnected
    }

    [Native]
    public enum TVILogLevel : nuint
    {
        Off = 0,
        Fatal,
        Error,
        Warning,
        Info,
        Debug,
        Trace,
        All
    }

    [Native]
    public enum TVILogModule : nuint
    {
        Core = 0,
        Platform,
        Signaling,
        WebRTC
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TVIAspectRatio
    {
        public nuint numerator;

        public nuint denominator;
    }

    [Native]
    public enum TVIVideoRenderingType : nuint
    {
        Metal = 0,
        OpenGLES
    }

}

