using Foundation;
using System;

namespace Twilio.Video.Sample.iOS
{
    public class TwilioRoomDelegate
            : TVIRoomDelegate

    {
        #region Fields

        private static TwilioRoomDelegate _instance;

        #endregion

        #region Properties

        public static TwilioRoomDelegate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TwilioRoomDelegate();
                }

                return _instance;
            }
        }

        public TVIRoom Room { get; private set; }
        public static bool InProgress { get; private set; }

        #endregion

        #region Events

        public event EventHandler DidConnectToRoomEvent;
        void OnDidConnectToRoom()
        {
            DidConnectToRoomEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<NSError> RoomDidFailToConnectWithErrorEvent;
        void OnRoomDidFailToConnectWithError(NSError error)
        {
            RoomDidFailToConnectWithErrorEvent?.Invoke(this, error);
        }

        public event EventHandler<NSError> RoomDidDisconnectWithErrorEvent;
        void OnRoomDidDisconnectWithError(NSError error)
        {
            RoomDidDisconnectWithErrorEvent?.Invoke(this, error);
        }

        public event EventHandler RoomParticipantDidConnectEvent;
        void OnRoomParticipantDidConnect()
        {
            RoomParticipantDidConnectEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomParticipantDidDisconnectEvent;
        void OnRoomParticipantDidDisconnect()
        {
            RoomParticipantDidDisconnectEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomDidStartRecordingEvent;
        void OnRoomDidStartRecording()
        {
            RoomDidStartRecordingEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomDidStopRecordingEvent;
        void OnRoomDidStopRecording()
        {
            RoomDidStopRecordingEvent?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Methods

        public void ConnectToRoom(string token, TVILocalAudioTrack localAudioTrack, TVILocalVideoTrack localVideoTrack, string roomName)
        {
            InProgress = true;

            var connectOptions = TVIConnectOptions.OptionsWithToken(token, builder =>
            {
                builder.AudioTracks = new[] { localAudioTrack };
                builder.VideoTracks = new[] { localVideoTrack };
                builder.RoomName = roomName;
            });

            Room = TwilioVideo.ConnectWithOptions(connectOptions, Instance);
        }

        [Export("didConnectToRoom:")]
        public override void DidConnectToRoom(TVIRoom room)
        {
            if (room.Participants.Length > 0)
            {
                TwilioParticipantDelegate.Instance.SetParticipant(room.Participants[0]);
            }

            OnDidConnectToRoom();
        }

        [Export("room:didDisconnectWithError:")]
        public override void RoomDidDisconnectWithError(TVIRoom room, NSError error)
        {
            OnRoomDidDisconnectWithError(error);
        }

        [Export("room:didFailToConnectWithError:")]
        public override void RoomDidFailToConnectWithError(TVIRoom room, NSError error)
        {
            OnRoomDidFailToConnectWithError(error);
        }

        [Export("roomDidStartRecording:")]
        public override void RoomDidStartRecording(TVIRoom room)
        {
            OnRoomDidStartRecording();
        }

        [Export("roomDidStopRecording:")]
        public override void RoomDidStopRecording(TVIRoom room)
        {
            OnRoomDidStopRecording();
        }

        [Export("room:participantDidConnect:")]
        public override void RoomParticipantDidConnect(TVIRoom room, TVIParticipant participant)
        {
            if (TwilioParticipantDelegate.Instance.Participant == null)
            {
                TwilioParticipantDelegate.Instance.SetParticipant(participant);
            }

            OnRoomParticipantDidConnect();
        }

        [Export("room:participantDidDisconnect:")]
        public override void RoomParticipantDidDisconnect(TVIRoom room, TVIParticipant participant)
        {
            if (TwilioParticipantDelegate.Instance.Participant == participant)
            {
                OnRoomParticipantDidDisconnect();
            }
        }

        public void Finish()
        {
            InProgress = false;

            if (Room != null)
            {
                Room.Disconnect();
            }

            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        #endregion
    }
}
