using System;
using System.Runtime.InteropServices;
using CoreFoundation;
using CoreMedia;
using CoreVideo;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Twilio.Video
{
    public static class ConstantsExtensions
    {
        public static CMVideoDimensions IntPtrToCMVideoDimensions(IntPtr ptr)
        {
            var dimensions = new CMVideoDimensions();

            if (ptr != IntPtr.Zero)
            {
                dimensions = (CMVideoDimensions)Marshal.PtrToStructure<CMVideoDimensions>(ptr);
            }

            return dimensions;
        }

        public static TVIAspectRatio IntPtrToTVIAspectRatio(IntPtr ptr)
        {
            var dimensions = new TVIAspectRatio();

            if (ptr != IntPtr.Zero)
            {
                dimensions = (TVIAspectRatio)Marshal.PtrToStructure<TVIAspectRatio>(ptr);
            }

            return dimensions;
        }
    }

    // @interface TVIAudioConstraintsBuilder : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIAudioConstraintsBuilder
    {
        // @property (assign, nonatomic) BOOL autoGainControl;
        [Export("autoGainControl")]
        bool AutoGainControl { get; set; }

        // @property (assign, nonatomic) BOOL noiseReduction;
        [Export("noiseReduction")]
        bool NoiseReduction { get; set; }
    }

    // typedef void (^TVIAudioConstraintsBuilderBlock)(TVIAudioConstraintsBuilder * _Nonnull);
    delegate void TVIAudioConstraintsBuilderBlock(TVIAudioConstraintsBuilder arg0);

    // @interface TVIAudioConstraints : NSObject
    [BaseType(typeof(NSObject))]
    interface TVIAudioConstraints
    {
        // @property (readonly, assign, nonatomic) BOOL autoGainControl;
        [Export("autoGainControl")]
        bool AutoGainControl { get; }

        // @property (readonly, assign, nonatomic) BOOL noiseReduction;
        [Export("noiseReduction")]
        bool NoiseReduction { get; }

        // +(instancetype _Null_unspecified)constraints;
        [Static]
        [Export("constraints")]
        TVIAudioConstraints Constraints();

        // +(instancetype _Null_unspecified)constraintsWithBlock:(TVIAudioConstraintsBuilderBlock _Nonnull)block;
        [Static]
        [Export("constraintsWithBlock:")]
        TVIAudioConstraints ConstraintsWithBlock(TVIAudioConstraintsBuilderBlock block);
    }

    // @interface TVIAudioController : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIAudioController
    {
        // @property (assign, nonatomic) TVIAudioOutput audioOutput;
        [Export("audioOutput", ArgumentSemantic.Assign)]
        TVIAudioOutput AudioOutput { get; set; }
    }

    // @interface CallKit (TVIAudioController)
    [Category]
    [BaseType(typeof(TVIAudioController))]
    interface TVIAudioController_CallKit
    {
        // -(void)configureAudioSession:(TVIAudioOutput)audioOutput;
        [Export("configureAudioSession:")]
        void ConfigureAudioSession(TVIAudioOutput audioOutput);

        // -(BOOL)startAudio;
        [Static]
        [Export("startAudio")]
        bool StartAudio { get; }

        // -(void)stopAudio;
        [Export("stopAudio")]
        void StopAudio();
    }

    // @interface TVITrack : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVITrack
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull trackId;
        [Export("trackId")]
        string TrackId { get; }

        // @property (readonly, getter = isEnabled, assign, nonatomic) BOOL enabled;
        [Export("enabled")]
        bool Enabled { [Bind("isEnabled")] get; }

        // @property (readonly, assign, nonatomic) TVITrackState state;
        [Export("state", ArgumentSemantic.Assign)]
        TVITrackState State { get; }
    }

    // @interface TVIAudioTrack : TVITrack
    [BaseType(typeof(TVITrack))]
    [DisableDefaultCtor]
    interface TVIAudioTrack
    {
    }

    // @interface TVILocalAudioTrack : TVIAudioTrack
    [BaseType(typeof(TVIAudioTrack))]
    [DisableDefaultCtor]
    interface TVILocalAudioTrack
    {
        // @property (readonly, nonatomic, strong) TVIAudioConstraints * _Nonnull constraints;
        [Export("constraints", ArgumentSemantic.Strong)]
        TVIAudioConstraints Constraints { get; }

        // @property (getter = isEnabled, assign, nonatomic) BOOL enabled;
        [Export("enabled")]
        bool Enabled { [Bind("isEnabled")] get; set; }
    }

    // @interface TVIBaseTrackStats : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIBaseTrackStats
    {
        // @property (readonly, copy, nonatomic) NSString * _Nullable trackId;
        [NullAllowed, Export("trackId")]
        string TrackId { get; }

        // @property (readonly, assign, nonatomic) NSUInteger packetsLost;
        [Export("packetsLost")]
        nuint PacketsLost { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable codec;
        [NullAllowed, Export("codec")]
        string Codec { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable ssrc;
        [NullAllowed, Export("ssrc")]
        string Ssrc { get; }

        // @property (readonly, assign, nonatomic) CFTimeInterval timestamp;
        [Export("timestamp")]
        double Timestamp { get; }
    }

    // @interface TVITrackStats : TVIBaseTrackStats
    [BaseType(typeof(TVIBaseTrackStats))]
    [DisableDefaultCtor]
    interface TVITrackStats
    {
        // @property (readonly, assign, nonatomic) int64_t bytesReceived;
        [Export("bytesReceived")]
        long BytesReceived { get; }

        // @property (readonly, assign, nonatomic) NSUInteger packetsReceived;
        [Export("packetsReceived")]
        nuint PacketsReceived { get; }
    }

    // @interface TVIAudioTrackStats : TVITrackStats
    [BaseType(typeof(TVITrackStats))]
    [DisableDefaultCtor]
    interface TVIAudioTrackStats
    {
        // @property (readonly, assign, nonatomic) NSUInteger audioLevel;
        [Export("audioLevel")]
        nuint AudioLevel { get; }

        // @property (readonly, assign, nonatomic) NSUInteger jitter;
        [Export("jitter")]
        nuint Jitter { get; }
    }

    // @interface TVIVideoFrame : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIVideoFrame
    {
        // -(instancetype _Null_unspecified)initWithTimestamp:(int64_t)timestamp buffer:(CVImageBufferRef _Nonnull)imageBuffer orientation:(TVIVideoOrientation)orientation;
        [Export("initWithTimestamp:buffer:orientation:")]
        unsafe IntPtr Constructor(long timestamp, CVImageBuffer imageBuffer, TVIVideoOrientation orientation);

        // @property (readonly, assign, nonatomic) int64_t timestamp;
        [Export("timestamp")]
        long Timestamp { get; }

        // @property (readonly, assign, nonatomic) size_t width;
        [Export("width")]
        nuint Width { get; }

        // @property (readonly, assign, nonatomic) size_t height;
        [Export("height")]
        nuint Height { get; }

        // @property (readonly, assign, nonatomic) CVImageBufferRef _Nonnull imageBuffer;
        [Export("imageBuffer", ArgumentSemantic.Assign)]
        unsafe IntPtr ImageBuffer { get; }

        // @property (readonly, assign, nonatomic) TVIVideoOrientation orientation;
        [Export("orientation", ArgumentSemantic.Assign)]
        TVIVideoOrientation Orientation { get; }
    }

    // @interface TVIVideoFormat : NSObject
    [BaseType(typeof(NSObject))]
    interface TVIVideoFormat
    {
        // @property (assign, nonatomic) CMVideoDimensions dimensions;
        [Export("dimensions", ArgumentSemantic.Assign)]
        IntPtr Dimensions { get; set; }

        // @property (assign, nonatomic) NSUInteger frameRate;
        [Export("frameRate")]
        nuint FrameRate { get; set; }

        // @property (assign, nonatomic) TVIPixelFormat pixelFormat;
        [Export("pixelFormat", ArgumentSemantic.Assign)]
        /*TVIPixelFormat* TODO*/ ulong PixelFormat { get; set; }
    }

    // @protocol TVIVideoCaptureConsumer <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVIVideoCaptureConsumer
    {
        // @required -(void)consumeCapturedFrame:(TVIVideoFrame * _Nonnull)frame;
        [Abstract]
        [Export("consumeCapturedFrame:")]
        void ConsumeCapturedFrame(TVIVideoFrame frame);

        // @required -(void)captureDidStart:(BOOL)success;
        [Abstract]
        [Export("captureDidStart:")]
        void CaptureDidStart(bool success);
    }

    // @protocol TVIVideoCapturer <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVIVideoCapturer
    {
        // @required @property (readonly, getter = isScreencast, assign, nonatomic) BOOL screencast;
        [Abstract]
        [Export("screencast")]
        bool Screencast { [Bind("isScreencast")] get; }

        // @required @property (readonly, copy, nonatomic) NSArray<TVIVideoFormat *> * _Nonnull supportedFormats;
        [Abstract]
        [Export("supportedFormats", ArgumentSemantic.Copy)]
        TVIVideoFormat[] SupportedFormats { get; }

        // @required -(void)startCapture:(TVIVideoFormat * _Nonnull)format consumer:(id<TVIVideoCaptureConsumer> _Nonnull)consumer;
        [Abstract]
        [Export("startCapture:consumer:")]
        void StartCapture(TVIVideoFormat format, TVIVideoCaptureConsumer consumer);

        // @required -(void)stopCapture;
        [Abstract]
        [Export("stopCapture")]
        void StopCapture();
    }

    /*[Static] TODO*/
    partial interface Constants
    {
        // extern const CMVideoDimensions TVIVideoConstraintsSize352x288;
        [Field("TVIVideoConstraintsSize352x288", "__Internal")]
        IntPtr TVIVideoConstraintsSize352x288 { get; }

        // extern const CMVideoDimensions TVIVideoConstraintsSize480x360;
        [Field("TVIVideoConstraintsSize480x360", "__Internal")]
        IntPtr TVIVideoConstraintsSize480x360 { get; }

        // extern const CMVideoDimensions TVIVideoConstraintsSize640x480;
        [Field("TVIVideoConstraintsSize640x480", "__Internal")]
        IntPtr TVIVideoConstraintsSize640x480 { get; }

        // extern const CMVideoDimensions TVIVideoConstraintsSize960x540;
        [Field("TVIVideoConstraintsSize960x540", "__Internal")]
        IntPtr TVIVideoConstraintsSize960x540 { get; }

        // extern const CMVideoDimensions TVIVideoConstraintsSize1280x720;
        [Field("TVIVideoConstraintsSize1280x720", "__Internal")]
        IntPtr TVIVideoConstraintsSize1280x720 { get; }

        // extern const CMVideoDimensions TVIVideoConstraintsSize1280x960;
        [Field("TVIVideoConstraintsSize1280x960", "__Internal")]
        IntPtr TVIVideoConstraintsSize1280x960 { get; }

        // extern const NSUInteger TVIVideoConstraintsFrameRate30;
        [Field("TVIVideoConstraintsFrameRate30", "__Internal")]
        nuint TVIVideoConstraintsFrameRate30 { get; }

        // extern const NSUInteger TVIVideoConstraintsFrameRate24;
        [Field("TVIVideoConstraintsFrameRate24", "__Internal")]
        nuint TVIVideoConstraintsFrameRate24 { get; }

        // extern const NSUInteger TVIVideoConstraintsFrameRate20;
        [Field("TVIVideoConstraintsFrameRate20", "__Internal")]
        nuint TVIVideoConstraintsFrameRate20 { get; }

        // extern const NSUInteger TVIVideoConstraintsFrameRate15;
        [Field("TVIVideoConstraintsFrameRate15", "__Internal")]
        nuint TVIVideoConstraintsFrameRate15 { get; }

        // extern const NSUInteger TVIVideoConstraintsFrameRate10;
        [Field("TVIVideoConstraintsFrameRate10", "__Internal")]
        nuint TVIVideoConstraintsFrameRate10 { get; }
    }

    // @protocol TVICameraCapturerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVICameraCapturerDelegate
    {
        // @optional -(void)cameraCapturerPreviewDidStart:(TVICameraCapturer * _Nonnull)capturer;
        [Export("cameraCapturerPreviewDidStart:")]
        void CameraCapturerPreviewDidStart(TVICameraCapturer capturer);

        // @optional -(void)cameraCapturer:(TVICameraCapturer * _Nonnull)capturer didStartWithSource:(TVIVideoCaptureSource)source;
        [Export("cameraCapturer:didStartWithSource:")]
        void CameraCapturer(TVICameraCapturer capturer, TVIVideoCaptureSource source);

        // @optional -(void)cameraCapturerWasInterrupted:(TVICameraCapturer * _Nonnull)capturer;
        [Export("cameraCapturerWasInterrupted:")]
        void CameraCapturerWasInterrupted(TVICameraCapturer capturer);

        // @optional -(void)cameraCapturer:(TVICameraCapturer * _Nonnull)capturer didFailWithError:(NSError * _Nonnull)error;
        [Export("cameraCapturer:didFailWithError:")]
        void CameraCapturer(TVICameraCapturer capturer, NSError error);
    }

    // @interface TVICameraCapturer : NSObject <TVIVideoCapturer>
    [BaseType(typeof(NSObject))]
    interface TVICameraCapturer : TVIVideoCapturer
    {
        // @property (readonly, assign, nonatomic) TVIVideoCaptureSource source;
        [Export("source", ArgumentSemantic.Assign)]
        TVIVideoCaptureSource Source { get; }

        // @property (readonly, getter = isCapturing, assign, atomic) BOOL capturing;
        [Export("capturing")]
        bool Capturing { [Bind("isCapturing")] get; }

        [Wrap("WeakDelegate")]
        [NullAllowed]
        TVICameraCapturerDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<TVICameraCapturerDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, assign, nonatomic) CMVideoDimensions previewDimensions;
        [Export("previewDimensions", ArgumentSemantic.Assign)]
        IntPtr PreviewDimensions { get; }

        // @property (readonly, nonatomic, strong) TVICameraPreviewView * _Nonnull previewView;
        [Export("previewView", ArgumentSemantic.Strong)]
        TVICameraPreviewView PreviewView { get; }

        // @property (readonly, getter = isInterrupted, assign, nonatomic) BOOL interrupted;
        [Export("interrupted")]
        bool Interrupted { [Bind("isInterrupted")] get; }

        // -(instancetype _Nullable)initWithSource:(TVIVideoCaptureSource)source;
        [Export("initWithSource:")]
        IntPtr Constructor(TVIVideoCaptureSource source);

        // -(instancetype _Nullable)initWithSource:(TVIVideoCaptureSource)source delegate:(id<TVICameraCapturerDelegate> _Nullable)delegate;
        [Export("initWithSource:delegate:")]
        IntPtr Constructor(TVIVideoCaptureSource source, [NullAllowed] TVICameraCapturerDelegate @delegate);

        // -(void)selectSource:(TVIVideoCaptureSource)source;
        [Export("selectSource:")]
        void SelectSource(TVIVideoCaptureSource source);

        // -(void)selectNextSource;
        [Export("selectNextSource")]
        void SelectNextSource();

        // +(BOOL)isSourceAvailable:(TVIVideoCaptureSource)source;
        [Static]
        [Export("isSourceAvailable:")]
        bool IsSourceAvailable(TVIVideoCaptureSource source);

        // +(NSArray<NSNumber *> * _Nonnull)availableSources;
        [Static]
        [Export("availableSources")]
        NSNumber[] AvailableSources { get; }
    }

    // @interface TVICameraPreviewView : UIView
    [BaseType(typeof(UIView))]
    interface TVICameraPreviewView
    {
        // @property (readonly, assign, nonatomic) UIInterfaceOrientation orientation;
        [Export("orientation", ArgumentSemantic.Assign)]
        UIInterfaceOrientation Orientation { get; }
    }

    // @interface TVIConnectOptionsBuilder : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIConnectOptionsBuilder
    {
        // @property (nonatomic, strong) dispatch_queue_t _Nullable delegateQueue;
        [NullAllowed, Export("delegateQueue", ArgumentSemantic.Strong)]
        DispatchQueue DelegateQueue { get; set; }

        // @property (nonatomic, strong) TVIIceOptions * _Nullable iceOptions;
        [NullAllowed, Export("iceOptions", ArgumentSemantic.Strong)]
        TVIIceOptions IceOptions { get; set; }

        // @property (nonatomic, strong) TVILocalMedia * _Nullable localMedia;
        [NullAllowed, Export("localMedia", ArgumentSemantic.Strong)]
        TVILocalMedia LocalMedia { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable roomName;
        [NullAllowed, Export("roomName")]
        string RoomName { get; set; }
    }

    // @interface CallKit (TVIConnectOptionsBuilder)
    [Category]
    [BaseType(typeof(TVIConnectOptionsBuilder))]
    interface TVIConnectOptionsBuilder_CallKit
    {
        // @property (nonatomic, strong) NSUUID * _Nullable uuid;
        [Static]
        [NullAllowed, Export("uuid", ArgumentSemantic.Strong)]
        NSUuid Uuid { get; set; }
    }

    // typedef void (^TVIConnectOptionsBuilderBlock)(TVIConnectOptionsBuilder * _Nonnull);
    delegate void TVIConnectOptionsBuilderBlock(TVIConnectOptionsBuilder arg0);

    // @interface TVIConnectOptions : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIConnectOptions
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull accessToken;
        [Export("accessToken")]
        string AccessToken { get; }

        // @property (readonly, nonatomic, strong) dispatch_queue_t _Nullable delegateQueue;
        [NullAllowed, Export("delegateQueue", ArgumentSemantic.Strong)]
        DispatchQueue DelegateQueue { get; }

        // @property (readonly, nonatomic, strong) TVIIceOptions * _Nullable iceOptions;
        [NullAllowed, Export("iceOptions", ArgumentSemantic.Strong)]
        TVIIceOptions IceOptions { get; }

        // @property (readonly, nonatomic, strong) TVILocalMedia * _Nullable localMedia;
        [NullAllowed, Export("localMedia", ArgumentSemantic.Strong)]
        TVILocalMedia LocalMedia { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable roomName;
        [NullAllowed, Export("roomName")]
        string RoomName { get; }

        // +(instancetype _Nonnull)optionsWithToken:(NSString * _Nonnull)token;
        [Static]
        [Export("optionsWithToken:")]
        TVIConnectOptions OptionsWithToken(string token);

        // +(instancetype _Nonnull)optionsWithToken:(NSString * _Nonnull)token block:(TVIConnectOptionsBuilderBlock _Nonnull)block;
        [Static]
        [Export("optionsWithToken:block:")]
        TVIConnectOptions OptionsWithToken(string token, TVIConnectOptionsBuilderBlock block);
    }

    // @interface CallKit (TVIConnectOptions)
    [Category]
    [BaseType(typeof(TVIConnectOptions))]
    interface TVIConnectOptions_CallKit
    {
        // @property (readonly, nonatomic, strong) NSUUID * _Nullable uuid;
        [Static]
        [NullAllowed, Export("uuid", ArgumentSemantic.Strong)]
        NSUuid Uuid { get; }
    }

    /*[Static] TODO*/
    partial interface Constants
    {
        // extern NSString *const _Nonnull kTVIErrorDomain;
        [Field("kTVIErrorDomain", "__Internal")]
        NSString kTVIErrorDomain { get; }
    }

    // @interface TVIIceServer : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIIceServer
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull urlString;
        [Export("urlString")]
        string UrlString { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable username;
        [NullAllowed, Export("username")]
        string Username { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable password;
        [NullAllowed, Export("password")]
        string Password { get; }

        // -(instancetype _Null_unspecified)initWithURLString:(NSString * _Nonnull)serverURLString;
        [Export("initWithURLString:")]
        IntPtr Constructor(string serverURLString);

        // -(instancetype _Null_unspecified)initWithURLString:(NSString * _Nonnull)serverURLString username:(NSString * _Nullable)username password:(NSString * _Nullable)password;
        [Export("initWithURLString:username:password:")]
        IntPtr Constructor(string serverURLString, [NullAllowed] string username, [NullAllowed] string password);
    }

    // @interface TVIIceOptionsBuilder : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIIceOptionsBuilder
    {
        // @property (nonatomic, strong) NSArray<TVIIceServer *> * _Nullable servers;
        [NullAllowed, Export("servers", ArgumentSemantic.Strong)]
        TVIIceServer[] Servers { get; set; }

        // @property (assign, nonatomic) TVIIceTransportPolicy transportPolicy;
        [Export("transportPolicy", ArgumentSemantic.Assign)]
        TVIIceTransportPolicy TransportPolicy { get; set; }
    }

    // typedef void (^TVIIceOptionsBuilderBlock)(TVIIceOptionsBuilder * _Nonnull);
    delegate void TVIIceOptionsBuilderBlock(TVIIceOptionsBuilder arg0);

    // @interface TVIIceOptions : NSObject
    [BaseType(typeof(NSObject))]
    interface TVIIceOptions
    {
        // @property (readonly, copy, nonatomic) NSArray<TVIIceServer *> * _Nonnull servers;
        [Export("servers", ArgumentSemantic.Copy)]
        TVIIceServer[] Servers { get; }

        // @property (readonly, assign, nonatomic) TVIIceTransportPolicy transportPolicy;
        [Export("transportPolicy", ArgumentSemantic.Assign)]
        TVIIceTransportPolicy TransportPolicy { get; }

        // +(instancetype _Null_unspecified)options;
        [Static]
        [Export("options")]
        TVIIceOptions Options();

        // +(instancetype _Nonnull)optionsWithBlock:(TVIIceOptionsBuilderBlock _Nonnull)block;
        [Static]
        [Export("optionsWithBlock:")]
        TVIIceOptions OptionsWithBlock(TVIIceOptionsBuilderBlock block);
    }

    // @interface TVILocalMedia : NSObject
    [BaseType(typeof(NSObject))]
    interface TVILocalMedia
    {
        // @property (readonly, nonatomic, strong) TVIAudioController * _Nonnull audioController;
        [Export("audioController", ArgumentSemantic.Strong)]
        TVIAudioController AudioController { get; }

        // @property (readonly, nonatomic, strong) NSArray<TVILocalAudioTrack *> * _Nonnull audioTracks;
        [Export("audioTracks", ArgumentSemantic.Strong)]
        TVILocalAudioTrack[] AudioTracks { get; }

        // @property (readonly, nonatomic, strong) NSArray<TVILocalVideoTrack *> * _Nonnull videoTracks;
        [Export("videoTracks", ArgumentSemantic.Strong)]
        TVILocalVideoTrack[] VideoTracks { get; }

        // -(TVILocalAudioTrack * _Nullable)addAudioTrack:(BOOL)enabled;
        [Export("addAudioTrack:")]
        [return: NullAllowed]
        TVILocalAudioTrack AddAudioTrack(bool enabled);

        // -(TVILocalAudioTrack * _Nullable)addAudioTrack:(BOOL)enabled constraints:(TVIAudioConstraints * _Nullable)constraints error:(NSError * _Nullable * _Nullable)error;
        [Export("addAudioTrack:constraints:error:")]
        [return: NullAllowed]
        TVILocalAudioTrack AddAudioTrack(bool enabled, [NullAllowed] TVIAudioConstraints constraints, [NullAllowed] out NSError error);

        // -(BOOL)removeAudioTrack:(TVILocalAudioTrack * _Nonnull)track;
        [Export("removeAudioTrack:")]
        bool RemoveAudioTrack(TVILocalAudioTrack track);

        // -(BOOL)removeAudioTrack:(TVILocalAudioTrack * _Nonnull)track error:(NSError * _Nullable * _Nullable)error;
        [Export("removeAudioTrack:error:")]
        bool RemoveAudioTrack(TVILocalAudioTrack track, [NullAllowed] out NSError error);

        // -(TVILocalVideoTrack * _Nullable)addVideoTrack:(BOOL)enabled capturer:(id<TVIVideoCapturer> _Nonnull)capturer;
        [Export("addVideoTrack:capturer:")]
        [return: NullAllowed]
        TVILocalVideoTrack AddVideoTrack(bool enabled, TVIVideoCapturer capturer);

        // -(TVILocalVideoTrack * _Nullable)addVideoTrack:(BOOL)enabled capturer:(id<TVIVideoCapturer> _Nonnull)capturer constraints:(TVIVideoConstraints * _Nullable)constraints error:(NSError * _Nullable * _Nullable)error;
        [Export("addVideoTrack:capturer:constraints:error:")]
        [return: NullAllowed]
        TVILocalVideoTrack AddVideoTrack(bool enabled, TVIVideoCapturer capturer, [NullAllowed] TVIVideoConstraints constraints, [NullAllowed] out NSError error);

        // -(BOOL)removeVideoTrack:(TVILocalVideoTrack * _Nonnull)track;
        [Export("removeVideoTrack:")]
        bool RemoveVideoTrack(TVILocalVideoTrack track);

        // -(BOOL)removeVideoTrack:(TVILocalVideoTrack * _Nonnull)track error:(NSError * _Nullable * _Nullable)error;
        [Export("removeVideoTrack:error:")]
        bool RemoveVideoTrack(TVILocalVideoTrack track, [NullAllowed] out NSError error);
    }

    // @interface TVILocalTrackStats : TVIBaseTrackStats
    [BaseType(typeof(TVIBaseTrackStats))]
    [DisableDefaultCtor]
    interface TVILocalTrackStats
    {
        // @property (readonly, assign, nonatomic) int64_t bytesSent;
        [Export("bytesSent")]
        long BytesSent { get; }

        // @property (readonly, assign, nonatomic) NSUInteger packetsSent;
        [Export("packetsSent")]
        nuint PacketsSent { get; }

        // @property (readonly, assign, nonatomic) int64_t roundTripTime;
        [Export("roundTripTime")]
        long RoundTripTime { get; }
    }

    // @interface TVILocalAudioTrackStats : TVILocalTrackStats
    [BaseType(typeof(TVILocalTrackStats))]
    [DisableDefaultCtor]
    interface TVILocalAudioTrackStats
    {
        // @property (readonly, assign, nonatomic) NSUInteger audioLevel;
        [Export("audioLevel")]
        nuint AudioLevel { get; }

        // @property (readonly, assign, nonatomic) NSUInteger jitter;
        [Export("jitter")]
        nuint Jitter { get; }
    }

    // @interface TVILocalParticipant : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVILocalParticipant
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull identity;
        [Export("identity")]
        string Identity { get; }

        // @property (readonly, nonatomic, strong) TVILocalMedia * _Nonnull media;
        [Export("media", ArgumentSemantic.Strong)]
        TVILocalMedia Media { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nonnull sid;
        [Export("sid")]
        string Sid { get; }
    }

    // @interface TVILocalVideoTrackStats : TVILocalTrackStats
    [BaseType(typeof(TVILocalTrackStats))]
    [DisableDefaultCtor]
    interface TVILocalVideoTrackStats
    {
        // @property (readonly, assign, nonatomic) CMVideoDimensions captureDimensions;
        [Export("captureDimensions", ArgumentSemantic.Assign)]
        IntPtr CaptureDimensions { get; }

        // @property (readonly, assign, nonatomic) NSUInteger captureFrameRate;
        [Export("captureFrameRate")]
        nuint CaptureFrameRate { get; }

        // @property (readonly, assign, nonatomic) CMVideoDimensions dimensions;
        [Export("dimensions", ArgumentSemantic.Assign)]
        IntPtr Dimensions { get; }

        // @property (readonly, assign, nonatomic) NSUInteger frameRate;
        [Export("frameRate")]
        nuint FrameRate { get; }
    }

    // @interface TVIMedia : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIMedia
    {
        // @property (readonly, copy, nonatomic) NSArray<TVIAudioTrack *> * _Nonnull audioTracks;
        [Export("audioTracks", ArgumentSemantic.Copy)]
        TVIAudioTrack[] AudioTracks { get; }

        // @property (readonly, copy, nonatomic) NSArray<TVIVideoTrack *> * _Nonnull videoTracks;
        [Export("videoTracks", ArgumentSemantic.Copy)]
        TVIVideoTrack[] VideoTracks { get; }

        // -(TVITrack * _Nullable)getTrack:(NSString * _Nonnull)trackId;
        [Export("getTrack:")]
        [return: NullAllowed]
        TVITrack GetTrack(string trackId);

        // -(TVIAudioTrack * _Nullable)getAudioTrack:(NSString * _Nonnull)trackId;
        [Export("getAudioTrack:")]
        [return: NullAllowed]
        TVIAudioTrack GetAudioTrack(string trackId);

        // -(TVIVideoTrack * _Nullable)getVideoTrack:(NSString * _Nonnull)trackId;
        [Export("getVideoTrack:")]
        [return: NullAllowed]
        TVIVideoTrack GetVideoTrack(string trackId);
    }

    // @interface TVIParticipant : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIParticipant
    {
        // @property (readonly, getter = isConnected, assign, nonatomic) BOOL connected;
        [Export("connected")]
        bool Connected { [Bind("isConnected")] get; }

        [Wrap("WeakDelegate")]
        [NullAllowed]
        TVIParticipantDelegate Delegate { get; set; }

        // @property (atomic, weak) id<TVIParticipantDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, copy, nonatomic) NSString * _Nonnull identity;
        [Export("identity")]
        string Identity { get; }

        // @property (readonly, nonatomic, strong) TVIMedia * _Nonnull media;
        [Export("media", ArgumentSemantic.Strong)]
        TVIMedia Media { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nullable sid;
        [NullAllowed, Export("sid")]
        string Sid { get; }
    }

    // @protocol TVIParticipantDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVIParticipantDelegate
    {
        // @optional -(void)participant:(TVIParticipant * _Nonnull)participant addedVideoTrack:(TVIVideoTrack * _Nonnull)videoTrack;
        [Export("participant:addedVideoTrack:")]
        void AddedVideoTrack(TVIParticipant participant, TVIVideoTrack videoTrack);

        // @optional -(void)participant:(TVIParticipant * _Nonnull)participant removedVideoTrack:(TVIVideoTrack * _Nonnull)videoTrack;
        [Export("participant:removedVideoTrack:")]
        void RemovedVideoTrack(TVIParticipant participant, TVIVideoTrack videoTrack);

        // @optional -(void)participant:(TVIParticipant * _Nonnull)participant addedAudioTrack:(TVIAudioTrack * _Nonnull)audioTrack;
        [Export("participant:addedAudioTrack:")]
        void AddedAudioTrack(TVIParticipant participant, TVIAudioTrack audioTrack);

        // @optional -(void)participant:(TVIParticipant * _Nonnull)participant removedAudioTrack:(TVIAudioTrack * _Nonnull)audioTrack;
        [Export("participant:removedAudioTrack:")]
        void RemovedAudioTrack(TVIParticipant participant, TVIAudioTrack audioTrack);

        // @optional -(void)participant:(TVIParticipant * _Nonnull)participant enabledTrack:(TVITrack * _Nonnull)track;
        [Export("participant:enabledTrack:")]
        void EnabledTrack(TVIParticipant participant, TVITrack track);

        // @optional -(void)participant:(TVIParticipant * _Nonnull)participant disabledTrack:(TVITrack * _Nonnull)track;
        [Export("participant:disabledTrack:")]
        void DisabledTrack(TVIParticipant participant, TVITrack track);
    }

    // typedef void (^TVIRoomGetStatsBlock)(NSArray<TVIStatsReport *> * _Nonnull);
    delegate void TVIRoomGetStatsBlock(TVIStatsReport[] arg0);

    // @interface TVIRoom : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIRoom
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        TVIRoomDelegate Delegate { get; }

        // @property (readonly, nonatomic, weak) id<TVIRoomDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; }

        // @property (readonly, nonatomic, strong) TVILocalParticipant * _Nullable localParticipant;
        [NullAllowed, Export("localParticipant", ArgumentSemantic.Strong)]
        TVILocalParticipant LocalParticipant { get; }

        // @property (readonly, copy, nonatomic) NSString * _Nonnull name;
        [Export("name")]
        string Name { get; }

        // @property (readonly, copy, nonatomic) NSArray<TVIParticipant *> * _Nonnull participants;
        [Export("participants", ArgumentSemantic.Copy)]
        TVIParticipant[] Participants { get; }

        // @property (readonly, getter = isRecording, assign, nonatomic) BOOL recording;
        [Export("recording")]
        bool Recording { [Bind("isRecording")] get; }

        // @property (readonly, copy, nonatomic) NSString * _Nonnull sid;
        [Export("sid")]
        string Sid { get; }

        // @property (readonly, assign, nonatomic) TVIRoomState state;
        [Export("state", ArgumentSemantic.Assign)]
        TVIRoomState State { get; }

        // -(TVIParticipant * _Nullable)getParticipantWithSid:(NSString * _Nonnull)sid;
        [Export("getParticipantWithSid:")]
        [return: NullAllowed]
        TVIParticipant GetParticipantWithSid(string sid);

        // -(void)disconnect;
        [Export("disconnect")]
        void Disconnect();

        // -(void)getStatsWithBlock:(TVIRoomGetStatsBlock _Nonnull)block;
        [Export("getStatsWithBlock:")]
        void GetStatsWithBlock(TVIRoomGetStatsBlock block);
    }

    // @interface CallKit (TVIRoom)
    [Category]
    [BaseType(typeof(TVIRoom))]
    interface TVIRoom_CallKit
    {
        // @property (readonly, nonatomic) NSUUID * _Nullable uuid;
        [Static]
        [NullAllowed, Export("uuid")]
        NSUuid Uuid { get; }
    }

    // @protocol TVIRoomDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVIRoomDelegate
    {
        // @optional -(void)didConnectToRoom:(TVIRoom * _Nonnull)room;
        [Export("didConnectToRoom:")]
        void DidConnectToRoom(TVIRoom room);

        // @optional -(void)room:(TVIRoom * _Nonnull)room didFailToConnectWithError:(NSError * _Nonnull)error;
        [Export("room:didFailToConnectWithError:")]
        void RoomDidFailToConnectWithError(TVIRoom room, NSError error);

        // @optional -(void)room:(TVIRoom * _Nonnull)room didDisconnectWithError:(NSError * _Nullable)error;
        [Export("room:didDisconnectWithError:")]
        void RoomDidDisconnectWithError(TVIRoom room, [NullAllowed] NSError error);

        // @optional -(void)room:(TVIRoom * _Nonnull)room participantDidConnect:(TVIParticipant * _Nonnull)participant;
        [Export("room:participantDidConnect:")]
        void RoomParticipantDidConnect(TVIRoom room, TVIParticipant participant);

        // @optional -(void)room:(TVIRoom * _Nonnull)room participantDidDisconnect:(TVIParticipant * _Nonnull)participant;
        [Export("room:participantDidDisconnect:")]
        void RoomParticipantDidDisconnect(TVIRoom room, TVIParticipant participant);

        // @optional -(void)roomDidStartRecording:(TVIRoom * _Nonnull)room;
        [Export("roomDidStartRecording:")]
        void RoomDidStartRecording(TVIRoom room);

        // @optional -(void)roomDidStopRecording:(TVIRoom * _Nonnull)room;
        [Export("roomDidStopRecording:")]
        void RoomDidStopRecording(TVIRoom room);
    }

    // @interface TVIScreenCapturer : NSObject <TVIVideoCapturer>
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIScreenCapturer : TVIVideoCapturer
    {
        // @property (readonly, getter = isCapturing, assign, atomic) BOOL capturing;
        [Export("capturing")]
        bool Capturing { [Bind("isCapturing")] get; }

        // -(instancetype _Null_unspecified)initWithView:(UIView * _Nonnull)view;
        [Export("initWithView:")]
        IntPtr Constructor(UIView view);
    }

    // @interface TVIStatsReport : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIStatsReport
    {
        // @property (readonly, copy, nonatomic) NSString * _Nonnull peerConnectionId;
        [Export("peerConnectionId")]
        string PeerConnectionId { get; }

        // @property (readonly, nonatomic, strong) NSArray<TVILocalAudioTrackStats *> * _Nonnull localAudioTrackStats;
        [Export("localAudioTrackStats", ArgumentSemantic.Strong)]
        TVILocalAudioTrackStats[] LocalAudioTrackStats { get; }

        // @property (readonly, nonatomic, strong) NSArray<TVILocalVideoTrackStats *> * _Nonnull localVideoTrackStats;
        [Export("localVideoTrackStats", ArgumentSemantic.Strong)]
        TVILocalVideoTrackStats[] LocalVideoTrackStats { get; }

        // @property (readonly, nonatomic, strong) NSArray<TVIAudioTrackStats *> * _Nonnull audioTrackStats;
        [Export("audioTrackStats", ArgumentSemantic.Strong)]
        TVIAudioTrackStats[] AudioTrackStats { get; }

        // @property (readonly, nonatomic, strong) NSArray<TVIVideoTrackStats *> * _Nonnull videoTrackStats;
        [Export("videoTrackStats", ArgumentSemantic.Strong)]
        TVIVideoTrackStats[] VideoTrackStats { get; }
    }

    // @interface TVIVideoClient : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIVideoClient
    {
        // +(TVIRoom * _Nonnull)connectWithOptions:(TVIConnectOptions * _Nonnull)options delegate:(id<TVIRoomDelegate> _Nullable)delegate;
        [Static]
        [Export("connectWithOptions:delegate:")]
        TVIRoom ConnectWithOptions(TVIConnectOptions options, [NullAllowed] TVIRoomDelegate @delegate);

        // +(NSString * _Nonnull)version;
        [Static]
        [Export("version")]
        string Version { get; }

        // +(TVILogLevel)logLevel;
        // +(void)setLogLevel:(TVILogLevel)logLevel;
        [Static]
        [Export("logLevel")]
        TVILogLevel LogLevel { get; set; }

        // +(void)setLogLevel:(TVILogLevel)logLevel module:(TVILogModule)module;
        [Static]
        [Export("setLogLevel:module:")]
        void SetLogLevel(TVILogLevel logLevel, TVILogModule module);
    }

    [Static]
    partial interface Constants
    {
        // extern const NSUInteger TVIVideoConstraintsMaximumFPS;
        [Field("TVIVideoConstraintsMaximumFPS", "__Internal")]
        nuint TVIVideoConstraintsMaximumFPS { get; }

        // extern const NSUInteger TVIVideoConstraintsMinimumFPS;
        [Field("TVIVideoConstraintsMinimumFPS", "__Internal")]
        nuint TVIVideoConstraintsMinimumFPS { get; }

        // extern const CMVideoDimensions TVIVideoConstraintsSizeNone;
        [Field("TVIVideoConstraintsSizeNone", "__Internal")]
        IntPtr TVIVideoConstraintsSizeNone { get; }

        // extern const NSUInteger TVIVideoConstraintsFrameRateNone;
        [Field("TVIVideoConstraintsFrameRateNone", "__Internal")]
        nuint TVIVideoConstraintsFrameRateNone { get; }

        // extern const TVIAspectRatio TVIVideoConstraintsAspectRatioNone;
        [Field("TVIVideoConstraintsAspectRatioNone", "__Internal")]
        IntPtr TVIVideoConstraintsAspectRatioNone { get; }

        // extern const TVIAspectRatio TVIAspectRatio11x9;
        [Field("TVIAspectRatio11x9", "__Internal")]
        IntPtr TVIAspectRatio11x9 { get; }

        // extern const TVIAspectRatio TVIAspectRatio4x3;
        [Field("TVIAspectRatio4x3", "__Internal")]
        IntPtr TVIAspectRatio4x3 { get; }

        // extern const TVIAspectRatio TVIAspectRatio16x9;
        [Field("TVIAspectRatio16x9", "__Internal")]
        IntPtr TVIAspectRatio16x9 { get; }
    }

    // @interface TVIVideoConstraintsBuilder : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVIVideoConstraintsBuilder
    {
        // @property (assign, nonatomic) CMVideoDimensions maxSize;
        [Export("maxSize", ArgumentSemantic.Assign)]
        IntPtr MaxSize { get; set; }

        // @property (assign, nonatomic) CMVideoDimensions minSize;
        [Export("minSize", ArgumentSemantic.Assign)]
        IntPtr MinSize { get; set; }

        // @property (assign, nonatomic) NSUInteger maxFrameRate;
        [Export("maxFrameRate")]
        nuint MaxFrameRate { get; set; }

        // @property (assign, nonatomic) NSUInteger minFrameRate;
        [Export("minFrameRate")]
        nuint MinFrameRate { get; set; }

        // @property (assign, nonatomic) TVIAspectRatio aspectRatio;
        [Export("aspectRatio", ArgumentSemantic.Assign)]
        IntPtr AspectRatio { get; set; }
    }

    // typedef void (^TVIVideoConstraintsBuilderBlock)(TVIVideoConstraintsBuilder * _Nonnull);
    delegate void TVIVideoConstraintsBuilderBlock(TVIVideoConstraintsBuilder arg0);

    // @interface TVIVideoConstraints : NSObject
    [BaseType(typeof(NSObject))]
    interface TVIVideoConstraints
    {
        // +(instancetype _Null_unspecified)constraints;
        [Static]
        [Export("constraints")]
        TVIVideoConstraints Constraints();

        // +(instancetype _Null_unspecified)constraintsWithBlock:(TVIVideoConstraintsBuilderBlock _Nonnull)builderBlock;
        [Static]
        [Export("constraintsWithBlock:")]
        TVIVideoConstraints ConstraintsWithBlock(TVIVideoConstraintsBuilderBlock builderBlock);

        // @property (readonly, assign, nonatomic) CMVideoDimensions maxSize;
        [Export("maxSize", ArgumentSemantic.Assign)]
        IntPtr MaxSize { get; }

        // @property (readonly, assign, nonatomic) CMVideoDimensions minSize;
        [Export("minSize", ArgumentSemantic.Assign)]
        IntPtr MinSize { get; }

        // @property (readonly, assign, nonatomic) NSUInteger maxFrameRate;
        [Export("maxFrameRate")]
        nuint MaxFrameRate { get; }

        // @property (readonly, assign, nonatomic) NSUInteger minFrameRate;
        [Export("minFrameRate")]
        nuint MinFrameRate { get; }

        // @property (readonly, assign, nonatomic) TVIAspectRatio aspectRatio;
        [Export("aspectRatio", ArgumentSemantic.Assign)]
        IntPtr AspectRatio { get; }
    }

    // @protocol TVIVideoRenderer <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVIVideoRenderer
    {
        // @required -(void)renderFrame:(TVIVideoFrame * _Nonnull)frame;
        [Abstract]
        [Export("renderFrame:")]
        void RenderFrame(TVIVideoFrame frame);

        // @required -(void)updateVideoSize:(CMVideoDimensions)videoSize orientation:(TVIVideoOrientation)orientation;
        [Abstract]
        [Export("updateVideoSize:orientation:")]
        void UpdateVideoSize(CMVideoDimensions videoSize, TVIVideoOrientation orientation);

        // @optional @property (readonly, copy, nonatomic) NSArray<NSNumber *> * _Nonnull optionalPixelFormats;
        [Export("optionalPixelFormats", ArgumentSemantic.Copy)]
        NSNumber[] OptionalPixelFormats { get; }
    }

    // @protocol TVIVideoTrackDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVIVideoTrackDelegate
    {
        // @optional -(void)videoTrack:(TVIVideoTrack * _Nonnull)track dimensionsDidChange:(CMVideoDimensions)dimensions;
        [Export("videoTrack:dimensionsDidChange:")]
        void DimensionsDidChange(TVIVideoTrack track, CMVideoDimensions dimensions);
    }

    // @interface TVIVideoTrack : TVITrack
    [BaseType(typeof(TVITrack))]
    [DisableDefaultCtor]
    interface TVIVideoTrack
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        TVIVideoTrackDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<TVIVideoTrackDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, nonatomic, strong) NSArray<UIView *> * _Nonnull attachedViews;
        [Export("attachedViews", ArgumentSemantic.Strong)]
        UIView[] AttachedViews { get; }

        // @property (readonly, nonatomic, strong) NSArray<id<TVIVideoRenderer>> * _Nonnull renderers;
        [Export("renderers", ArgumentSemantic.Strong)]
        TVIVideoRenderer[] Renderers { get; }

        // @property (readonly, assign, nonatomic) CMVideoDimensions videoDimensions;
        [Export("videoDimensions", ArgumentSemantic.Assign)]
        IntPtr VideoDimensions { get; }

        // -(void)attach:(UIView * _Nonnull)view;
        [Export("attach:")]
        void Attach(UIView view);

        // -(void)detach:(UIView * _Nonnull)view;
        [Export("detach:")]
        void Detach(UIView view);

        // -(void)addRenderer:(id<TVIVideoRenderer> _Nonnull)renderer;
        [Export("addRenderer:")]
        void AddRenderer(TVIVideoRenderer renderer);

        // -(void)removeRenderer:(id<TVIVideoRenderer> _Nonnull)renderer;
        [Export("removeRenderer:")]
        void RemoveRenderer(TVIVideoRenderer renderer);
    }

    // @interface TVILocalVideoTrack : TVIVideoTrack
    [BaseType(typeof(TVIVideoTrack))]
    [DisableDefaultCtor]
    interface TVILocalVideoTrack
    {
        // @property (getter = isEnabled, assign, nonatomic) BOOL enabled;
        [Export("enabled")]
        bool Enabled { [Bind("isEnabled")] get; set; }

        // @property (readonly, nonatomic, strong) id<TVIVideoCapturer> _Nonnull capturer;
        [Export("capturer", ArgumentSemantic.Strong)]
        TVIVideoCapturer Capturer { get; }

        // @property (readonly, nonatomic, strong) TVIVideoConstraints * _Nonnull constraints;
        [Export("constraints", ArgumentSemantic.Strong)]
        TVIVideoConstraints Constraints { get; }
    }

    // @interface TVIVideoTrackStats : TVITrackStats
    [BaseType(typeof(TVITrackStats))]
    [DisableDefaultCtor]
    interface TVIVideoTrackStats
    {
        // @property (readonly, assign, nonatomic) CMVideoDimensions dimensions;
        [Export("dimensions", ArgumentSemantic.Assign)]
        IntPtr Dimensions { get; }

        // @property (readonly, assign, nonatomic) NSUInteger frameRate;
        [Export("frameRate")]
        nuint FrameRate { get; }
    }

    // @protocol TVIVideoViewRendererDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVIVideoViewRendererDelegate
    {
        // @optional -(void)rendererDidReceiveVideoData:(TVIVideoViewRenderer * _Nonnull)renderer;
        [Export("rendererDidReceiveVideoData:")]
        void RendererDidReceiveVideoData(TVIVideoViewRenderer renderer);

        // @optional -(void)renderer:(TVIVideoViewRenderer * _Nonnull)renderer dimensionsDidChange:(CMVideoDimensions)dimensions;
        [Export("renderer:dimensionsDidChange:")]
        void Renderer(TVIVideoViewRenderer renderer, CMVideoDimensions dimensions);

        // @optional -(void)renderer:(TVIVideoViewRenderer * _Nonnull)renderer orientationDidChange:(TVIVideoOrientation)orientation;
        [Export("renderer:orientationDidChange:")]
        void Renderer(TVIVideoViewRenderer renderer, TVIVideoOrientation orientation);

        // @optional -(BOOL)rendererShouldRotateContent:(TVIVideoViewRenderer * _Nonnull)renderer;
        [Export("rendererShouldRotateContent:")]
        bool RendererShouldRotateContent(TVIVideoViewRenderer renderer);
    }

    // @interface TVIVideoViewRenderer : NSObject <TVIVideoRenderer>
    [BaseType(typeof(NSObject))]
    interface TVIVideoViewRenderer : TVIVideoRenderer
    {
        // -(instancetype _Nonnull)initWithDelegate:(id<TVIVideoViewRendererDelegate> _Nullable)delegate;
        [Export("initWithDelegate:")]
        IntPtr Constructor([NullAllowed] TVIVideoViewRendererDelegate @delegate);

        // +(TVIVideoViewRenderer * _Nonnull)rendererWithDelegate:(id<TVIVideoViewRendererDelegate> _Nullable)delegate;
        [Static]
        [Export("rendererWithDelegate:")]
        TVIVideoViewRenderer RendererWithDelegate([NullAllowed] TVIVideoViewRendererDelegate @delegate);

        // +(TVIVideoViewRenderer * _Nonnull)rendererWithRenderingType:(TVIVideoRenderingType)renderingType delegate:(id<TVIVideoViewRendererDelegate> _Nullable)delegate;
        [Static]
        [Export("rendererWithRenderingType:delegate:")]
        TVIVideoViewRenderer RendererWithRenderingType(TVIVideoRenderingType renderingType, [NullAllowed] TVIVideoViewRendererDelegate @delegate);

        [Wrap("WeakDelegate")]
        [NullAllowed]
        TVIVideoViewRendererDelegate Delegate { get; }

        // @property (readonly, nonatomic, weak) id<TVIVideoViewRendererDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; }

        // @property (readonly, assign, nonatomic) CMVideoDimensions videoFrameDimensions;
        [Export("videoFrameDimensions", ArgumentSemantic.Assign)]
        IntPtr VideoFrameDimensions { get; }

        // @property (readonly, assign, nonatomic) TVIVideoOrientation videoFrameOrientation;
        [Export("videoFrameOrientation", ArgumentSemantic.Assign)]
        TVIVideoOrientation VideoFrameOrientation { get; }

        // @property (readonly, assign, atomic) BOOL hasVideoData;
        [Export("hasVideoData")]
        bool HasVideoData { get; }

        // @property (readonly, nonatomic, strong) UIView * _Nonnull view;
        [Export("view", ArgumentSemantic.Strong)]
        UIView View { get; }
    }
}

