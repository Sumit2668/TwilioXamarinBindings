using System;
using System.Runtime.InteropServices;
using CoreFoundation;
using CoreGraphics;
using CoreMedia;
using CoreVideo;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Twilio.Video.iOS
{
	// @interface TVIAudioController : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface TVIAudioController
	{
		// @property (assign, nonatomic) TVIAudioOutput audioOutput;
		[Export("audioOutput", ArgumentSemantic.Assign)]
		TVIAudioOutput AudioOutput { get; set; }

		// +(instancetype _Nonnull)sharedController;
		[Static]
		[Export("sharedController")]
		TVIAudioController SharedController();
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
		[Export("startAudio")]
		//[Verify (MethodToProperty)]
		//bool StartAudio { get; }
		bool StartAudio();

		// -(void)stopAudio;
		[Export("stopAudio")]
		void StopAudio();
	}

	// @interface TVIAudioOptionsBuilder : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface TVIAudioOptionsBuilder
	{
		// @property (assign, nonatomic) BOOL audioJitterBufferFastAccelerate;
		[Export("audioJitterBufferFastAccelerate")]
		bool AudioJitterBufferFastAccelerate { get; set; }

		// @property (assign, nonatomic) int audioJitterBufferMaxPackets;
		[Export("audioJitterBufferMaxPackets")]
		int AudioJitterBufferMaxPackets { get; set; }

		// @property (assign, nonatomic) BOOL highpassFilter;
		[Export("highpassFilter")]
		bool HighpassFilter { get; set; }

		// @property (assign, nonatomic) BOOL levelControl;
		[Export("levelControl")]
		bool LevelControl { get; set; }

		// @property (assign, nonatomic) CGFloat levelControlInitialPeakLevelDBFS;
		[Export("levelControlInitialPeakLevelDBFS")]
		nfloat LevelControlInitialPeakLevelDBFS { get; set; }
	}

	// typedef void (^TVIAudioOptionsBuilderBlock)(TVIAudioOptionsBuilder * _Nonnull);
	delegate void TVIAudioOptionsBuilderBlock(TVIAudioOptionsBuilder arg0);

	// @interface TVIAudioOptions : NSObject
	[BaseType(typeof(NSObject))]
	interface TVIAudioOptions
	{
		// @property (readonly, assign, nonatomic) int audioJitterBufferMaxPackets;
		[Export("audioJitterBufferMaxPackets")]
		int AudioJitterBufferMaxPackets { get; }

		// @property (readonly, assign, nonatomic) BOOL audioJitterBufferFastAccelerate;
		[Export("audioJitterBufferFastAccelerate")]
		bool AudioJitterBufferFastAccelerate { get; }

		// @property (readonly, assign, nonatomic) BOOL highpassFilter;
		[Export("highpassFilter")]
		bool HighpassFilter { get; }

		// @property (readonly, assign, nonatomic) BOOL levelControl;
		[Export("levelControl")]
		bool LevelControl { get; }

		// @property (readonly, assign, nonatomic) CGFloat levelControlInitialPeakLevelDBFS;
		[Export("levelControlInitialPeakLevelDBFS")]
		nfloat LevelControlInitialPeakLevelDBFS { get; }

		// +(instancetype _Null_unspecified)options;
		[Static]
		[Export("options")]
		TVIAudioOptions Options();

		// +(instancetype _Null_unspecified)optionsWithBlock:(TVIAudioOptionsBuilderBlock _Nonnull)block;
		[Static]
		[Export("optionsWithBlock:")]
		TVIAudioOptions OptionsWithBlock(TVIAudioOptionsBuilderBlock block);
	}

	// @protocol TVIAudioSink <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface TVIAudioSink
	{
		// @required -(void)renderSample:(CMSampleBufferRef)audioSample;
		[Abstract]
		[Export("renderSample:")]
		unsafe void RenderSample(CMSampleBuffer audioSample);
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
		// @property (readonly, nonatomic, strong) NSArray<id<TVIAudioSink>> * _Nonnull sinks;
		[Export("sinks", ArgumentSemantic.Strong)]
		TVIAudioSink[] Sinks { get; }

		// -(void)addSink:(id<TVIAudioSink> _Nonnull)sink;
		[Export("addSink:")]
		void AddSink(TVIAudioSink sink);

		// -(void)removeSink:(id<TVIAudioSink> _Nonnull)sink;
		[Export("removeSink:")]
		void RemoveSink(TVIAudioSink sink);
	}

	// @interface TVILocalAudioTrack : TVIAudioTrack
	[BaseType(typeof(TVIAudioTrack))]
	[DisableDefaultCtor]
	interface TVILocalAudioTrack
	{
		// @property (readonly, nonatomic, strong) TVIAudioOptions * _Nullable options;
		[NullAllowed, Export("options", ArgumentSemantic.Strong)]
		TVIAudioOptions Options { get; }

		// @property (getter = isEnabled, assign, nonatomic) BOOL enabled;
		[Export("enabled")]
		bool Enabled { [Bind("isEnabled")] get; set; }

		// +(instancetype _Null_unspecified)track;
		[Static]
		[Export("track")]
		TVILocalAudioTrack Track();

		// +(instancetype _Null_unspecified)trackWithOptions:(TVIAudioOptions * _Nullable)options enabled:(BOOL)enabled;
		[Static]
		[Export("trackWithOptions:enabled:")]
		TVILocalAudioTrack TrackWithOptions([NullAllowed] TVIAudioOptions options, bool enabled);
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
		//unsafe CVImageBuffer ImageBuffer { get; }
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
		CMVideoDimensions Dimensions { get; set; }

		// @property (assign, nonatomic) NSUInteger frameRate;
		[Export("frameRate")]
		nuint FrameRate { get; set; }

		// @property (assign, nonatomic) TVIPixelFormat pixelFormat;
		[Export("pixelFormat", ArgumentSemantic.Assign)]
		/*TVIPixelFormat* TODO*/
		ulong PixelFormat { get; set; }
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

	interface ITVIVideoCapturer { }

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
		CMVideoDimensions TVIVideoConstraintsSize352x288 { get; }

		// extern const CMVideoDimensions TVIVideoConstraintsSize480x360;
		[Field("TVIVideoConstraintsSize480x360", "__Internal")]
		CMVideoDimensions TVIVideoConstraintsSize480x360 { get; }

		// extern const CMVideoDimensions TVIVideoConstraintsSize640x480;
		[Field("TVIVideoConstraintsSize640x480", "__Internal")]
		CMVideoDimensions TVIVideoConstraintsSize640x480 { get; }

		// extern const CMVideoDimensions TVIVideoConstraintsSize960x540;
		[Field("TVIVideoConstraintsSize960x540", "__Internal")]
		CMVideoDimensions TVIVideoConstraintsSize960x540 { get; }

		// extern const CMVideoDimensions TVIVideoConstraintsSize1280x720;
		[Field("TVIVideoConstraintsSize1280x720", "__Internal")]
		CMVideoDimensions TVIVideoConstraintsSize1280x720 { get; }

		// extern const CMVideoDimensions TVIVideoConstraintsSize1280x960;
		[Field("TVIVideoConstraintsSize1280x960", "__Internal")]
		CMVideoDimensions TVIVideoConstraintsSize1280x960 { get; }

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
		// @optional -(void)cameraCapturer:(TVICameraCapturer * _Nonnull)capturer didStartWithSource:(TVICameraCaptureSource)source;
		[Export("cameraCapturer:didStartWithSource:")]
		void CameraCapturer(TVICameraCapturer capturer, TVICameraCaptureSource source);

		// @optional -(void)cameraCapturerWasInterrupted:(TVICameraCapturer * _Nonnull)capturer reason:(TVICameraCapturerInterruptionReason)reason;
		[Export("cameraCapturerWasInterrupted:reason:")]
		void CameraCapturerWasInterrupted(TVICameraCapturer capturer, TVICameraCapturerInterruptionReason reason);

		// @optional -(void)cameraCapturer:(TVICameraCapturer * _Nonnull)capturer didFailWithError:(NSError * _Nonnull)error;
		[Export("cameraCapturer:didFailWithError:")]
		void CameraCapturer(TVICameraCapturer capturer, NSError error);
	}

	// @interface TVICameraCapturer : NSObject <TVIVideoCapturer>
	[BaseType(typeof(NSObject))]
	interface TVICameraCapturer : TVIVideoCapturer
	{
		// @property (readonly, assign, nonatomic) TVICameraCaptureSource source;
		[Export("source", ArgumentSemantic.Assign)]
		TVICameraCaptureSource Source { get; }

		// @property (readonly, getter = isCapturing, assign, atomic) BOOL capturing;
		[Export("capturing")]
		bool Capturing { [Bind("isCapturing")] get; }

		[Wrap("WeakDelegate")]
		[NullAllowed]
		TVICameraCapturerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<TVICameraCapturerDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly, nonatomic, strong) TVICameraPreviewView * _Nonnull previewView;
		[Export("previewView", ArgumentSemantic.Strong)]
		TVICameraPreviewView PreviewView { get; }

		// @property (readonly, getter = isInterrupted, assign, nonatomic) BOOL interrupted;
		[Export("interrupted")]
		bool Interrupted { [Bind("isInterrupted")] get; }

		// -(instancetype _Nullable)initWithSource:(TVICameraCaptureSource)source;
		[Export("initWithSource:")]
		IntPtr Constructor(TVICameraCaptureSource source);

		// -(instancetype _Nullable)initWithSource:(TVICameraCaptureSource)source delegate:(id<TVICameraCapturerDelegate> _Nullable)delegate;
		[Export("initWithSource:delegate:")]
		IntPtr Constructor(TVICameraCaptureSource source, [NullAllowed] TVICameraCapturerDelegate @delegate);

		// -(instancetype _Nullable)initWithSource:(TVICameraCaptureSource)source delegate:(id<TVICameraCapturerDelegate> _Nullable)delegate enablePreview:(BOOL)enablePreview;
		[Export("initWithSource:delegate:enablePreview:")]
		IntPtr Constructor(TVICameraCaptureSource source, [NullAllowed] TVICameraCapturerDelegate @delegate, bool enablePreview);

		// -(BOOL)selectSource:(TVICameraCaptureSource)source;
		[Export("selectSource:")]
		bool SelectSource(TVICameraCaptureSource source);

		// +(BOOL)isSourceAvailable:(TVICameraCaptureSource)source;
		[Static]
		[Export("isSourceAvailable:")]
		bool IsSourceAvailable(TVICameraCaptureSource source);

		// +(NSArray<NSNumber *> * _Nonnull)availableSources;
		[Static]
		[Export("availableSources")]
		//TODO [Verify (MethodToProperty)]
		NSNumber[] AvailableSources();
	}

	// @interface TVICameraPreviewView : UIView
	[BaseType(typeof(UIView))]
	interface TVICameraPreviewView
	{
		// @property (readonly, assign, nonatomic) UIInterfaceOrientation orientation;
		[Export("orientation", ArgumentSemantic.Assign)]
		UIInterfaceOrientation Orientation { get; }

		// @property (readonly, assign, nonatomic) CMVideoDimensions videoDimensions;
		[Export("videoDimensions", ArgumentSemantic.Assign)]
		CMVideoDimensions VideoDimensions { get; }
	}

	// @interface TVIConnectOptionsBuilder : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface TVIConnectOptionsBuilder
	{
		// @property (copy, nonatomic) NSArray<TVILocalAudioTrack *> * _Nonnull audioTracks;
		[Export("audioTracks", ArgumentSemantic.Copy)]
		TVILocalAudioTrack[] AudioTracks { get; set; }

		// @property (nonatomic, strong) dispatch_queue_t _Nullable delegateQueue;
		[NullAllowed, Export("delegateQueue", ArgumentSemantic.Strong)]
		DispatchQueue DelegateQueue { get; set; }

		// @property (nonatomic, strong) TVIIceOptions * _Nullable iceOptions;
		[NullAllowed, Export("iceOptions", ArgumentSemantic.Strong)]
		TVIIceOptions IceOptions { get; set; }

		// @property (getter = areInsightsEnabled, assign, nonatomic) BOOL insightsEnabled;
		[Export("insightsEnabled")]
		bool InsightsEnabled { [Bind("areInsightsEnabled")] get; set; }

		// @property (getter = shouldReconnectAfterReturningToForeground, assign, nonatomic) BOOL reconnectAfterReturningToForeground;
		[Export("reconnectAfterReturningToForeground")]
		bool ReconnectAfterReturningToForeground { [Bind("shouldReconnectAfterReturningToForeground")] get; set; }

		// @property (copy, nonatomic) NSString * _Nullable roomName;
		[NullAllowed, Export("roomName")]
		string RoomName { get; set; }

		// @property (copy, nonatomic) NSArray<TVILocalVideoTrack *> * _Nonnull videoTracks;
		[Export("videoTracks", ArgumentSemantic.Copy)]
		TVILocalVideoTrack[] VideoTracks { get; set; }
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

		// @property (readonly, copy, nonatomic) NSArray<TVILocalAudioTrack *> * _Nonnull audioTracks;
		[Export("audioTracks", ArgumentSemantic.Copy)]
		TVILocalAudioTrack[] AudioTracks { get; }

		// @property (readonly, nonatomic, strong) dispatch_queue_t _Nullable delegateQueue;
		[NullAllowed, Export("delegateQueue", ArgumentSemantic.Strong)]
		DispatchQueue DelegateQueue { get; }

		// @property (readonly, nonatomic, strong) TVIIceOptions * _Nullable iceOptions;
		[NullAllowed, Export("iceOptions", ArgumentSemantic.Strong)]
		TVIIceOptions IceOptions { get; }

		// @property (readonly, getter = areInsightsEnabled, assign, nonatomic) BOOL insightsEnabled;
		[Export("insightsEnabled")]
		bool InsightsEnabled { [Bind("areInsightsEnabled")] get; }

		// @property (readonly, getter = shouldReconnectAfterReturningToForeground, assign, nonatomic) BOOL reconnectAfterReturningToForeground;
		[Export("reconnectAfterReturningToForeground")]
		bool ReconnectAfterReturningToForeground { [Bind("shouldReconnectAfterReturningToForeground")] get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable roomName;
		[NullAllowed, Export("roomName")]
		string RoomName { get; }

		// @property (readonly, copy, nonatomic) NSArray<TVILocalVideoTrack *> * _Nonnull videoTracks;
		[Export("videoTracks", ArgumentSemantic.Copy)]
		TVILocalVideoTrack[] VideoTracks { get; }

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

	//TODO [Static]
	partial interface Constants
	{
		// extern NSString *const _Nonnull kTVIErrorDomain;
		[Field("kTVIErrorDomain", "__Internal")]
		NSString kTVIErrorDomain { get; }
	}

	// @interface TVIIceCandidateStats : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface TVIIceCandidateStats
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable candidateType;
		[NullAllowed, Export("candidateType")]
		string CandidateType { get; }

		// @property (readonly, getter = isDeleted, assign, nonatomic) BOOL deleted;
		[Export("deleted")]
		bool Deleted { [Bind("isDeleted")] get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable ip;
		[NullAllowed, Export("ip")]
		string Ip { get; }

		// @property (readonly, getter = isRemote, assign, nonatomic) BOOL remote;
		[Export("remote")]
		bool Remote { [Bind("isRemote")] get; }

		// @property (readonly, assign, nonatomic) long port;
		[Export("port")]
		nint Port { get; }

		// @property (readonly, assign, nonatomic) long priority;
		[Export("priority")]
		nint Priority { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable protocol;
		[NullAllowed, Export("protocol")]
		string Protocol { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable url;
		[NullAllowed, Export("url")]
		string Url { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable transportId;
		[NullAllowed, Export("transportId")]
		string TransportId { get; }
	}

	// @interface TVIIceCandidatePairStats : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface TVIIceCandidatePairStats
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable transportId;
		[NullAllowed, Export("transportId")]
		string TransportId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable localCandidateId;
		[NullAllowed, Export("localCandidateId")]
		string LocalCandidateId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable remoteCandidateId;
		[NullAllowed, Export("remoteCandidateId")]
		string RemoteCandidateId { get; }

		// @property (readonly, assign, nonatomic) TVIIceCandidatePairState state;
		[Export("state", ArgumentSemantic.Assign)]
		TVIIceCandidatePairState State { get; }

		// @property (readonly, assign, nonatomic) uint64_t priority;
		[Export("priority")]
		ulong Priority { get; }

		// @property (readonly, getter = isNominated, assign, nonatomic) BOOL nominated;
		[Export("nominated")]
		bool Nominated { [Bind("isNominated")] get; }

		// @property (readonly, getter = isWritable, assign, nonatomic) BOOL writable;
		[Export("writable")]
		bool Writable { [Bind("isWritable")] get; }

		// @property (readonly, getter = isReadable, assign, nonatomic) BOOL readable;
		[Export("readable")]
		bool Readable { [Bind("isReadable")] get; }

		// @property (readonly, assign, nonatomic) uint64_t bytesSent;
		[Export("bytesSent")]
		ulong BytesSent { get; }

		// @property (readonly, assign, nonatomic) uint64_t bytesReceived;
		[Export("bytesReceived")]
		ulong BytesReceived { get; }

		// @property (readonly, assign, nonatomic) CFTimeInterval totalRoundTripTime;
		[Export("totalRoundTripTime")]
		double TotalRoundTripTime { get; }

		// @property (readonly, assign, nonatomic) CFTimeInterval currentRoundTripTime;
		[Export("currentRoundTripTime")]
		double CurrentRoundTripTime { get; }

		// @property (readonly, assign, nonatomic) double availableOutgoingBitrate;
		[Export("availableOutgoingBitrate")]
		double AvailableOutgoingBitrate { get; }

		// @property (readonly, assign, nonatomic) double availableIncomingBitrate;
		[Export("availableIncomingBitrate")]
		double AvailableIncomingBitrate { get; }

		// @property (readonly, assign, nonatomic) uint64_t requestsReceived;
		[Export("requestsReceived")]
		ulong RequestsReceived { get; }

		// @property (readonly, assign, nonatomic) uint64_t requestsSent;
		[Export("requestsSent")]
		ulong RequestsSent { get; }

		// @property (readonly, assign, nonatomic) uint64_t responsesReceived;
		[Export("responsesReceived")]
		ulong ResponsesReceived { get; }

		// @property (readonly, assign, nonatomic) uint64_t responsesSent;
		[Export("responsesSent")]
		ulong ResponsesSent { get; }

		// @property (readonly, assign, nonatomic) uint64_t retransmissionsReceived;
		[Export("retransmissionsReceived")]
		ulong RetransmissionsReceived { get; }

		// @property (readonly, assign, nonatomic) uint64_t retransmissionsSent;
		[Export("retransmissionsSent")]
		ulong RetransmissionsSent { get; }

		// @property (readonly, assign, nonatomic) uint64_t consentRequestsReceived;
		[Export("consentRequestsReceived")]
		ulong ConsentRequestsReceived { get; }

		// @property (readonly, assign, nonatomic) uint64_t consentRequestsSent;
		[Export("consentRequestsSent")]
		ulong ConsentRequestsSent { get; }

		// @property (readonly, assign, nonatomic) uint64_t consentResponsesReceived;
		[Export("consentResponsesReceived")]
		ulong ConsentResponsesReceived { get; }

		// @property (readonly, assign, nonatomic) uint64_t consentResponsesSent;
		[Export("consentResponsesSent")]
		ulong ConsentResponsesSent { get; }
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

		// @property (readonly, copy, nonatomic) NSString * _Nonnull sid;
		[Export("sid")]
		string Sid { get; }

		// @property (readonly, copy, nonatomic) NSArray<TVILocalAudioTrack *> * _Nonnull audioTracks;
		[Export("audioTracks", ArgumentSemantic.Copy)]
		TVILocalAudioTrack[] AudioTracks { get; }

		// @property (readonly, copy, nonatomic) NSArray<TVILocalVideoTrack *> * _Nonnull videoTracks;
		[Export("videoTracks", ArgumentSemantic.Copy)]
		TVILocalVideoTrack[] VideoTracks { get; }

		// -(BOOL)addAudioTrack:(TVILocalAudioTrack * _Nonnull)track;
		[Export("addAudioTrack:")]
		bool AddAudioTrack(TVILocalAudioTrack track);

		// -(BOOL)addVideoTrack:(TVILocalVideoTrack * _Nonnull)track;
		[Export("addVideoTrack:")]
		bool AddVideoTrack(TVILocalVideoTrack track);

		// -(BOOL)removeAudioTrack:(TVILocalAudioTrack * _Nonnull)track;
		[Export("removeAudioTrack:")]
		bool RemoveAudioTrack(TVILocalAudioTrack track);

		// -(BOOL)removeVideoTrack:(TVILocalVideoTrack * _Nonnull)track;
		[Export("removeVideoTrack:")]
		bool RemoveVideoTrack(TVILocalVideoTrack track);
	}

	// @interface TVILocalVideoTrackStats : TVILocalTrackStats
	[BaseType(typeof(TVILocalTrackStats))]
	[DisableDefaultCtor]
	interface TVILocalVideoTrackStats
	{
		// @property (readonly, assign, nonatomic) CMVideoDimensions captureDimensions;
		[Export("captureDimensions", ArgumentSemantic.Assign)]
		CMVideoDimensions CaptureDimensions { get; }

		// @property (readonly, assign, nonatomic) NSUInteger captureFrameRate;
		[Export("captureFrameRate")]
		nuint CaptureFrameRate { get; }

		// @property (readonly, assign, nonatomic) CMVideoDimensions dimensions;
		[Export("dimensions", ArgumentSemantic.Assign)]
		CMVideoDimensions Dimensions { get; }

		// @property (readonly, assign, nonatomic) NSUInteger frameRate;
		[Export("frameRate")]
		nuint FrameRate { get; }
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

		// @property (readonly, copy, nonatomic) NSString * _Nullable sid;
		[NullAllowed, Export("sid")]
		string Sid { get; }

		// @property (readonly, copy, nonatomic) NSArray<TVIAudioTrack *> * _Nonnull audioTracks;
		[Export("audioTracks", ArgumentSemantic.Copy)]
		TVIAudioTrack[] AudioTracks { get; }

		// @property (readonly, copy, nonatomic) NSArray<TVIVideoTrack *> * _Nonnull videoTracks;
		[Export("videoTracks", ArgumentSemantic.Copy)]
		TVIVideoTrack[] VideoTracks { get; }
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

		// @property (readonly, nonatomic, strong) NSArray<TVIIceCandidateStats *> * _Nonnull iceCandidateStats;
		[Export("iceCandidateStats", ArgumentSemantic.Strong)]
		TVIIceCandidateStats[] IceCandidateStats { get; }

		// @property (readonly, nonatomic, strong) NSArray<TVIIceCandidatePairStats *> * _Nonnull iceCandidatePairStats;
		[Export("iceCandidatePairStats", ArgumentSemantic.Strong)]
		TVIIceCandidatePairStats[] IceCandidatePairStats { get; }
	}

	//TODO [Static]
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
		CMVideoDimensions TVIVideoConstraintsSizeNone { get; }
		//TODO IntPtr TVIVideoConstraintsSizeNone { get; }

		// extern const NSUInteger TVIVideoConstraintsFrameRateNone;
		[Field("TVIVideoConstraintsFrameRateNone", "__Internal")]
		nuint TVIVideoConstraintsFrameRateNone { get; }

		// extern const TVIAspectRatio TVIVideoConstraintsAspectRatioNone;
		[Field("TVIVideoConstraintsAspectRatioNone", "__Internal")]
		TVIAspectRatio TVIVideoConstraintsAspectRatioNone { get; }

		// extern const TVIAspectRatio TVIAspectRatio11x9;
		[Field("TVIAspectRatio11x9", "__Internal")]
		TVIAspectRatio TVIAspectRatio11x9 { get; }

		// extern const TVIAspectRatio TVIAspectRatio4x3;
		[Field("TVIAspectRatio4x3", "__Internal")]
		TVIAspectRatio TVIAspectRatio4x3 { get; }

		// extern const TVIAspectRatio TVIAspectRatio16x9;
		[Field("TVIAspectRatio16x9", "__Internal")]
		TVIAspectRatio TVIAspectRatio16x9 { get; }
	}

	// @interface TVIVideoConstraintsBuilder : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface TVIVideoConstraintsBuilder
	{
		// @property (assign, nonatomic) CMVideoDimensions maxSize;
		[Export("maxSize", ArgumentSemantic.Assign)]
		CMVideoDimensions MaxSize { get; set; }

		// @property (assign, nonatomic) CMVideoDimensions minSize;
		[Export("minSize", ArgumentSemantic.Assign)]
		CMVideoDimensions MinSize { get; set; }

		// @property (assign, nonatomic) NSUInteger maxFrameRate;
		[Export("maxFrameRate")]
		nuint MaxFrameRate { get; set; }

		// @property (assign, nonatomic) NSUInteger minFrameRate;
		[Export("minFrameRate")]
		nuint MinFrameRate { get; set; }

		// @property (assign, nonatomic) TVIAspectRatio aspectRatio;
		[Export("aspectRatio", ArgumentSemantic.Assign)]
		TVIAspectRatio AspectRatio { get; set; }
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
		CMVideoDimensions MaxSize { get; }

		// @property (readonly, assign, nonatomic) CMVideoDimensions minSize;
		[Export("minSize", ArgumentSemantic.Assign)]
		CMVideoDimensions MinSize { get; }

		// @property (readonly, assign, nonatomic) NSUInteger maxFrameRate;
		[Export("maxFrameRate")]
		nuint MaxFrameRate { get; }

		// @property (readonly, assign, nonatomic) NSUInteger minFrameRate;
		[Export("minFrameRate")]
		nuint MinFrameRate { get; }

		// @property (readonly, assign, nonatomic) TVIAspectRatio aspectRatio;
		[Export("aspectRatio", ArgumentSemantic.Assign)]
		TVIAspectRatio AspectRatio { get; }
	}

	interface ITVIVideoRenderer { }

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

	// @interface TVIVideoTrack : TVITrack
	[BaseType(typeof(TVITrack))]
	[DisableDefaultCtor]
	interface TVIVideoTrack
	{
		// @property (readonly, nonatomic, strong) NSArray<id<TVIVideoRenderer>> * _Nonnull renderers;
		[Export("renderers", ArgumentSemantic.Strong)]
		TVIVideoRenderer[] Renderers { get; }

		// -(void)addRenderer:(id<TVIVideoRenderer> _Nonnull)renderer;
		[Export("addRenderer:")]
		void AddRenderer(ITVIVideoRenderer renderer);

		// -(void)removeRenderer:(id<TVIVideoRenderer> _Nonnull)renderer;
		[Export("removeRenderer:")]
		void RemoveRenderer(ITVIVideoRenderer renderer);
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

		// +(instancetype _Nullable)trackWithCapturer:(id<TVIVideoCapturer> _Nonnull)capturer;
		[Static]
		[Export("trackWithCapturer:")]
		[return: NullAllowed]
		TVILocalVideoTrack TrackWithCapturer(ITVIVideoCapturer capturer);

		// +(instancetype _Nullable)trackWithCapturer:(id<TVIVideoCapturer> _Nonnull)capturer enabled:(BOOL)enabled constraints:(TVIVideoConstraints * _Nullable)constraints;
		[Static]
		[Export("trackWithCapturer:enabled:constraints:")]
		[return: NullAllowed]
		TVILocalVideoTrack TrackWithCapturer(ITVIVideoCapturer capturer, bool enabled, [NullAllowed] TVIVideoConstraints constraints);
	}

	// @interface TVIVideoTrackStats : TVITrackStats
	[BaseType(typeof(TVITrackStats))]
	[DisableDefaultCtor]
	interface TVIVideoTrackStats
	{
		// @property (readonly, assign, nonatomic) CMVideoDimensions dimensions;
		[Export("dimensions", ArgumentSemantic.Assign)]
		CMVideoDimensions Dimensions { get; }

		// @property (readonly, assign, nonatomic) NSUInteger frameRate;
		[Export("frameRate")]
		nuint FrameRate { get; }
	}

	// @protocol TVIVideoViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface TVIVideoViewDelegate
	{
		// @optional -(void)videoViewDidReceiveData:(TVIVideoView * _Nonnull)view;
		[Export("videoViewDidReceiveData:")]
		void VideoViewDidReceiveData(TVIVideoView view);

		// @optional -(void)videoView:(TVIVideoView * _Nonnull)view videoDimensionsDidChange:(CMVideoDimensions)dimensions;
		[Export("videoView:videoDimensionsDidChange:")]
		void VideoView(TVIVideoView view, CMVideoDimensions dimensions);

		// @optional -(void)videoView:(TVIVideoView * _Nonnull)view videoOrientationDidChange:(TVIVideoOrientation)orientation;
		[Export("videoView:videoOrientationDidChange:")]
		void VideoView(TVIVideoView view, TVIVideoOrientation orientation);
	}

	// @interface TVIVideoView : UIView <TVIVideoRenderer>
	[BaseType(typeof(UIView))]
	interface TVIVideoView : TVIVideoRenderer
	{
		// -(instancetype _Null_unspecified)initWithFrame:(CGRect)frame delegate:(id<TVIVideoViewDelegate> _Nullable)delegate;
		[Export("initWithFrame:delegate:")]
		IntPtr Constructor(CGRect frame, [NullAllowed] TVIVideoViewDelegate @delegate);

		// -(instancetype _Null_unspecified)initWithFrame:(CGRect)frame delegate:(id<TVIVideoViewDelegate> _Nullable)delegate renderingType:(TVIVideoRenderingType)renderingType;
		[Export("initWithFrame:delegate:renderingType:")]
		IntPtr Constructor(CGRect frame, [NullAllowed] TVIVideoViewDelegate @delegate, TVIVideoRenderingType renderingType);

		[Wrap("WeakDelegate")]
		[NullAllowed]
		TVIVideoViewDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<TVIVideoViewDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) BOOL viewShouldRotateContent;
		[Export("viewShouldRotateContent")]
		bool ViewShouldRotateContent { get; set; }

		// @property (readonly, assign, nonatomic) CMVideoDimensions videoDimensions;
		[Export("videoDimensions", ArgumentSemantic.Assign)]
		CMVideoDimensions VideoDimensions { get; }

		// @property (readonly, assign, nonatomic) TVIVideoOrientation videoOrientation;
		[Export("videoOrientation", ArgumentSemantic.Assign)]
		TVIVideoOrientation VideoOrientation { get; }

		// @property (readonly, assign, atomic) BOOL hasVideoData;
		[Export("hasVideoData")]
		bool HasVideoData { get; }

		// @property (getter = shouldMirror, assign, nonatomic) BOOL mirror;
		[Export("mirror")]
		bool Mirror { [Bind("shouldMirror")] get; set; }
	}

	// @interface TwilioVideo : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface TwilioVideo
	{
		// +(TVIRoom * _Nonnull)connectWithOptions:(TVIConnectOptions * _Nonnull)options delegate:(id<TVIRoomDelegate> _Nullable)delegate;
		[Static]
		[Export("connectWithOptions:delegate:")]
		TVIRoom ConnectWithOptions(TVIConnectOptions options, [NullAllowed] TVIRoomDelegate @delegate);

		// +(NSString * _Nonnull)version;
		[Static]
		[Export("version")]
		//TODO [Verify(MethodToProperty)]
		string Version { get; }

		// +(TVILogLevel)logLevel;
		// +(void)setLogLevel:(TVILogLevel)logLevel;
		[Static]
		[Export("logLevel")]
		//TODO [Verify(MethodToProperty)]
		TVILogLevel LogLevel { get; set; }

		// +(void)setLogLevel:(TVILogLevel)logLevel module:(TVILogModule)module;
		[Static]
		[Export("setLogLevel:module:")]
		void SetLogLevel(TVILogLevel logLevel, TVILogModule module);
	}
}
