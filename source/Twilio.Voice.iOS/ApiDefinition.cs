using System;
using Foundation;
using ObjCRuntime;

namespace Twilio.Voice.iOS
{
    // The first step to creating a binding is to add your native library ("libNativeLibrary.a")
    // to the project by right-clicking (or Control-clicking) the folder containing this source
    // file and clicking "Add files..." and then simply select the native library (or libraries)
    // that you want to bind.
    //
    // When you do that, you'll notice that MonoDevelop generates a code-behind file for each
    // native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
    // architectures that the native library supports and fills in that information for you,
    // however, it cannot auto-detect any Frameworks or other system libraries that the
    // native library may depend on, so you'll need to fill in that information yourself.
    //
    // Once you've done that, you're ready to move on to binding the API...
    //
    //
    // Here is where you'd define your API definition for the native Objective-C library.
    //
    // For example, to bind the following Objective-C class:
    //
    //     @interface Widget : NSObject {
    //     }
    //
    // The C# binding would look like this:
    //
    //     [BaseType (typeof (NSObject))]
    //     interface Widget {
    //     }
    //
    // To bind Objective-C properties, such as:
    //
    //     @property (nonatomic, readwrite, assign) CGPoint center;
    //
    // You would add a property definition in the C# interface like so:
    //
    //     [Export ("center")]
    //     CGPoint Center { get; set; }
    //
    // To bind an Objective-C method, such as:
    //
    //     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
    //
    // You would add a method definition to the C# interface like so:
    //
    //     [Export ("doSomething:atIndex:")]
    //     void DoSomething (NSObject object, int index);
    //
    // Objective-C "constructors" such as:
    //
    //     -(id)initWithElmo:(ElmoMuppet *)elmo;
    //
    // Can be bound as:
    //
    //     [Export ("initWithElmo:")]
    //     IntPtr Constructor (ElmoMuppet elmo);
    //
    // For more information, see http://developer.xamarin.com/guides/ios/advanced_topics/binding_objective-c/
    //

    // @interface TVOCall : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVOCall
    {
        [Wrap("WeakDelegate")]
        [NullAllowed]
        TVOCallDelegate Delegate { get; }

        // @property (readonly, nonatomic, weak) id<TVOCallDelegate> _Nullable delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nullable from;
        [NullAllowed, Export("from", ArgumentSemantic.Strong)]
        string From { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nullable to;
        [NullAllowed, Export("to", ArgumentSemantic.Strong)]
        string To { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull sid;
        [Export("sid", ArgumentSemantic.Strong)]
        string Sid { get; }

        // @property (getter = isMuted, assign, nonatomic) BOOL muted;
        [Export("muted")]
        bool Muted { [Bind("isMuted")] get; set; }

        // @property (readonly, assign, nonatomic) TVOCallState state;
        [Export("state", ArgumentSemantic.Assign)]
        TVOCallState State { get; }

        // @property (getter = isOnHold, nonatomic) BOOL onHold;
        [Export("onHold")]
        bool OnHold { [Bind("isOnHold")] get; set; }

        // -(void)disconnect;
        [Export("disconnect")]
        void Disconnect();

        // -(void)sendDigits:(NSString * _Nonnull)digits;
        [Export("sendDigits:")]
        void SendDigits(string digits);
    }

    // @interface CallKitIntegration (TVOCall)
    [Category]
    [BaseType(typeof(TVOCall))]
    interface TVOCall_CallKitIntegration
    {
        // @property (readonly, nonatomic, strong) NSUUID * _Nonnull uuid;
        [Export("uuid", ArgumentSemantic.Strong)]
        //NSUuid Uuid { get; }
        NSUuid GetUuid();
    }

    // @protocol TVOCallDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVOCallDelegate
    {
        // @required -(void)callDidConnect:(TVOCall * _Nonnull)call;
        [Abstract]
        [Export("callDidConnect:")]
        void CallDidConnect(TVOCall call);

        // @required -(void)call:(TVOCall * _Nonnull)call didFailToConnectWithError:(NSError * _Nonnull)error;
        [Abstract]
        [Export("call:didFailToConnectWithError:")]
        void CallDidFailToConnectWithError(TVOCall call, NSError error);

        // @required -(void)call:(TVOCall * _Nonnull)call didDisconnectWithError:(NSError * _Nullable)error;
        [Abstract]
        [Export("call:didDisconnectWithError:")]
        void CallDidDisconnectWithError(TVOCall call, [NullAllowed] NSError error);
    }

    // @interface TVOCallInvite : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TVOCallInvite
    {
        // @property (readonly, nonatomic, strong) NSString * _Nonnull from;
        [Export("from", ArgumentSemantic.Strong)]
        string From { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull to;
        [Export("to", ArgumentSemantic.Strong)]
        string To { get; }

        // @property (readonly, nonatomic, strong) NSString * _Nonnull callSid;
        [Export("callSid", ArgumentSemantic.Strong)]
        string CallSid { get; }

        // @property (readonly, assign, nonatomic) TVOCallInviteState state;
        [Export("state", ArgumentSemantic.Assign)]
        TVOCallInviteState State { get; }

        // -(TVOCall * _Nonnull)acceptWithDelegate:(id<TVOCallDelegate> _Nonnull)delegate;
        [Export("acceptWithDelegate:")]
        TVOCall AcceptWithDelegate(TVOCallDelegate @delegate);

        // -(void)reject;
        [Export("reject")]
        void Reject();
    }

    // @interface CallKitIntegration (TVOCallInvite)
    [Category]
    [BaseType(typeof(TVOCallInvite))]
    interface TVOCallInvite_CallKitIntegration
    {
        // @property (readonly, nonatomic, strong) NSUUID * _Nonnull uuid;
        [Export("uuid", ArgumentSemantic.Strong)]
        //NSUuid Uuid { get; }
        NSUuid GetUuid();
    }

    [Static]
    //[Verify(ConstantsInterfaceAssociation)]
    partial interface Constants
    {
        // extern NSString *const _Nonnull kTVOErrorDomain;
        [Field("kTVOErrorDomain", "__Internal")]
        NSString kTVOErrorDomain { get; }
    }

    // @protocol TVONotificationDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface TVONotificationDelegate
    {
        // @required -(void)callInviteReceived:(TVOCallInvite * _Nonnull)callInvite;
        [Abstract]
        [Export("callInviteReceived:")]
        void CallInviteReceived(TVOCallInvite callInvite);

        // @required -(void)notificationError:(NSError * _Nonnull)error;
        [Abstract]
        [Export("notificationError:")]
        void NotificationError(NSError error);
    }

    // @interface TwilioVoice : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface TwilioVoice
    {
        // @property (assign, nonatomic, class) TVOLogLevel logLevel;
        [Static]
        [Export("logLevel", ArgumentSemantic.Assign)]
        TVOLogLevel LogLevel { get; set; }

        // @property (getter = isAudioEnabled, assign, nonatomic, class) BOOL audioEnabled;
        [Static]
        [Export("audioEnabled")]
        bool AudioEnabled { [Bind("isAudioEnabled")] get; set; }

        // @property (copy, nonatomic, class) NSString * _Nonnull region;
        [Static]
        [Export("region")]
        string Region { get; set; }

        // +(NSString * _Nonnull)version;
        [Static]
        [Export("version")]
        //[Verify(MethodToProperty)]
        string Version { get; }

        // +(void)setModule:(TVOLogModule)module logLevel:(TVOLogLevel)level;
        [Static]
        [Export("setModule:logLevel:")]
        void SetModule(TVOLogModule module, TVOLogLevel level);

        // +(void)registerWithAccessToken:(NSString * _Nonnull)accessToken deviceToken:(NSString * _Nonnull)deviceToken completion:(void (^ _Nullable)(NSError * _Nullable))completion;
        [Static]
        [Export("registerWithAccessToken:deviceToken:completion:")]
        void RegisterWithAccessToken(string accessToken, string deviceToken, [NullAllowed] Action<NSError> completion);

        // +(void)unregisterWithAccessToken:(NSString * _Nonnull)accessToken deviceToken:(NSString * _Nonnull)deviceToken completion:(void (^ _Nullable)(NSError * _Nullable))completion;
        [Static]
        [Export("unregisterWithAccessToken:deviceToken:completion:")]
        void UnregisterWithAccessToken(string accessToken, string deviceToken, [NullAllowed] Action<NSError> completion);

        // +(void)handleNotification:(NSDictionary * _Nonnull)payload delegate:(id<TVONotificationDelegate> _Nonnull)delegate;
        [Static]
        [Export("handleNotification:delegate:")]
        void HandleNotification(NSDictionary payload, TVONotificationDelegate @delegate);

        // +(TVOCall * _Nonnull)call:(NSString * _Nonnull)accessToken params:(NSDictionary<NSString *,NSString *> * _Nullable)twiMLParams delegate:(id<TVOCallDelegate> _Nonnull)delegate;
        [Static]
        [Export("call:params:delegate:")]
        TVOCall Call(string accessToken, [NullAllowed] NSDictionary<NSString, NSString> twiMLParams, TVOCallDelegate @delegate);
    }

    // @interface CallKitIntegration (TwilioVoice)
    [Category]
    [BaseType(typeof(TwilioVoice))]
    interface TwilioVoice_CallKitIntegration
    {
        // +(TVOCall * _Nonnull)call:(NSString * _Nonnull)accessToken params:(NSDictionary<NSString *,NSString *> * _Nullable)twiMLParams uuid:(NSUUID * _Nonnull)uuid delegate:(id<TVOCallDelegate> _Nonnull)delegate;
        [Static]
        [Export("call:params:uuid:delegate:")]
        TVOCall Call(string accessToken, [NullAllowed] NSDictionary<NSString, NSString> twiMLParams, NSUuid uuid, TVOCallDelegate @delegate);

        // +(void)configureAudioSession;
        [Static]
        [Export("configureAudioSession")]
        void ConfigureAudioSession();
    }
}
