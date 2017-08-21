using System;

using Foundation;

namespace Twilio.Video.Sample.iOS
{
    public class TwilioParticipantDelegate :
            TVIParticipantDelegate
    {
        #region Fields

        static TwilioParticipantDelegate _instance;

        #endregion

        #region Properties

        public static TwilioParticipantDelegate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TwilioParticipantDelegate();
                }

                return _instance;
            }
        }

        public TVIParticipant Participant { get; private set; }

        #endregion

        #region Events

        public event EventHandler<TVIVideoTrack> AddedVideoTrackEvent;
        void OnAddedVideoTrack(TVIVideoTrack videoTrack)
        {
            AddedVideoTrackEvent?.Invoke(this, videoTrack);
        }

        public event EventHandler<TVIVideoTrack> RemovedVideoTrackEvent;
        void OnRemovedVideoTrack(TVIVideoTrack videoTrack)
        {
            RemovedVideoTrackEvent?.Invoke(this, videoTrack);
        }

        event EventHandler AddedAudioTrackEvent;
        void OnAddedAudioTrack()
        {
            AddedAudioTrackEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RemovedAudioTrackEvent;
        void OnRemovedAudioTrack()
        {
            RemovedAudioTrackEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler EnabledTrackEvent;
        void OntEnabledTrack()
        {
            EnabledTrackEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler DisabledTrackEvent;
        void OnDisabledTrack()
        {
            DisabledTrackEvent?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Methods

        public void SetParticipant(TVIParticipant participant)
        {
            if (Participant != null)
            {
                Participant.Delegate = null;
            }

            Participant = participant;
            Participant.Delegate = this;
        }

        public void Finish()
        {
            if (Participant != null)
            {
                Participant.Delegate = null;
            }

            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        [Export("participant:addedVideoTrack:")]
        public override void AddedVideoTrack(TVIParticipant participant, TVIVideoTrack videoTrack)
        {
            if (Participant == participant)
            {
                OnAddedVideoTrack(videoTrack);
            }
        }

        [Export("participant:removedVideoTrack:")]
        public override void RemovedVideoTrack(TVIParticipant participant, TVIVideoTrack videoTrack)
        {
            if (Participant == participant)
            {
                OnRemovedVideoTrack(videoTrack);
            }
        }

        [Export("participant:addedAudioTrack:")]
        public override void AddedAudioTrack(TVIParticipant participant, TVIAudioTrack audioTrack)
        {
            OnAddedAudioTrack();
        }

        [Export("participant:removedAudioTrack:")]
        public override void RemovedAudioTrack(TVIParticipant participant, TVIAudioTrack audioTrack)
        {
            OnRemovedAudioTrack();
        }

        [Export("participant:enabledTrack:")]
        public override void EnabledTrack(TVIParticipant participant, TVITrack track)
        {
            OntEnabledTrack();
        }

        [Export("participant:disabledTrack:")]
        public override void DisabledTrack(TVIParticipant participant, TVITrack track)
        {
            OnDisabledTrack();
        }

        #endregion
    }
}