using System;
using ObjCRuntime;

namespace Twilio.Voice.iOS
{
    [Native]
	public enum TVOCallState : ulong
    {
        Connecting = 0,
        Connected,
        Disconnected
    }

    [Native]
	public enum TVOCallInviteState : ulong
    {
        Pending = 0,
        Accepted,
        Rejected,
        Canceled
    }

    [Native]
	public enum TVOError : ulong
    {
        AccessTokenInvalid = 20101,
        AccessTokenHeaderInvalid = 20102,
        AccessTokenIssuerInvalid = 20103,
        AccessTokenExpired = 20104,
        AccessTokenNotYetValid = 20105,
        AccessTokenGrantsInvalid = 20106,
        AccessTokenSignatureInvalid = 20107,
        ExpirationTimeExceedsMaxTimeAllowed = 20157,
        AccessForbidden = 20403,
        ApplicationNotFound = 21218,
        ConnectionTimeout = 31003,
        InitializationError = 31004,
        ConnectionError = 31005,
        MalformedRequest = 31100,
        InvalidData = 31106,
        AuthorizationError = 31201,
        InvalidJWTToken = 31204,
        MicrophoneAccessDenial = 31208,
        RegistrationError = 31301
    }

    [Native]
	public enum TVOLogLevel : ulong
    {
        Off = 0,
        Error,
        Warn,
        Info,
        Debug,
        Verbose
    }

    [Native]
	public enum TVOLogModule : ulong
    {
        TVOLogModulePJSIP = 0
    }
}
