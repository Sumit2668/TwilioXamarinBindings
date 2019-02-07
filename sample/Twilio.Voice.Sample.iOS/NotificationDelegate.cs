using System;
using Foundation;
using LSP.Mobile.Infrastructure.Common.Log;
using Twilio.Voice.iOS;

namespace LSP.Mobile.iOS.ViewController.Delegates.Voice
{
    public class NotificationDelegate : TVONotificationDelegate
    {
        #region Fields

        private static NotificationDelegate _instance;

        #endregion

        #region Properties

        public static NotificationDelegate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationDelegate();
                }

                return _instance;
            }
        }

        #endregion

        #region Events

        public event EventHandler<TVOCallInvite> CallInviteReceivedEvent;
        public event EventHandler<NSError> NotificationErrorEvent;

        #endregion

        #region Methods

        [Export("callInviteReceived:")]
        public override void CallInviteReceived(TVOCallInvite callInvite)
        {
            LogHelper.Call(nameof(NotificationDelegate), nameof(CallInviteReceived));
            CallInviteReceivedEvent?.Invoke(this, callInvite);
        }

        [Export("notificationError:")]
        public override void NotificationError(NSError error)
        {
            LogHelper.Call(nameof(NotificationDelegate), nameof(NotificationError));
            NotificationErrorEvent?.Invoke(this, error);
        }

        #endregion
    }
}
