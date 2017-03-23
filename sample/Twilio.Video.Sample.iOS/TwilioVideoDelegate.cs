using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using Twilio.Video;

namespace Twilio.Video.Sample.iOS
{
    public class TwilioVideoDelegate :
         NSObject,
         ITVIRoomDelegate
    {
        #region Fields

        static TwilioVideoDelegate _instance;

        #endregion

        #region Properties

        public static TwilioVideoDelegate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TwilioVideoDelegate();
                }

                return _instance;
            }
        }

        public TVIRoom Room { get; private set; }

        #endregion

        #region Events

        public event EventHandler DidConnectToRoomEvent;
        void OnDidConnectToRoom()
        {
            DidConnectToRoomEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomDidFailToConnectWithErrorEvent;
        void OnRoomDidFailToConnectWithError()
        {
            RoomDidFailToConnectWithErrorEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomDidDisconnectWithErrorEvent;
        void OnRoomDidDisconnectWithError()
        {
            RoomDidDisconnectWithErrorEvent.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomParticipantDidConnectEvent;
        void OnRoomParticipantDidConnect()
        {
            RoomParticipantDidConnectEvent.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomParticipantDidDisconnectEvent;
        void OnRoomParticipantDidDisconnect()
        {
            RoomParticipantDidDisconnectEvent.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomDidStartRecordingEvent;
        void OnRoomDidStartRecording()
        {
            RoomDidStartRecordingEvent.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler RoomDidStopRecordingEvent;
        void OnRoomDidStopRecording()
        {
            RoomDidStopRecordingEvent.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Methods

        public void ConnectToRoom(string token, TVILocalMedia localMedia, string roomName)
        {
            //LogHelper.Call(GetType());

            var connectOptions = TVIConnectOptions.OptionsWithToken(token, builder =>
            {
                builder.LocalMedia = localMedia;
                builder.RoomName = roomName;
            });

            Room = TVIVideoClient.ConnectWithOptions(connectOptions, Instance);
        }

        #region ITVIRoomDelegate

        [Export("didConnectToRoom:")]
        public void DidConnectToRoom(TVIRoom room)
        {
            //LogHelper.Call(GetType());

            if (room.Participants.Length > 0)
            {
                TwilioParticipantDelegate.Instance.Participant = room.Participants[0];
            }

            OnDidConnectToRoom();
        }

        [Export("room:didFailToConnectWithError:")]
        public void RoomDidFailToConnectWithError(TVIRoom room, NSError error)
        {
            //LogHelper.Call(GetType());
            OnRoomDidFailToConnectWithError();
        }

        [Export("room:didDisconnectWithError:")]
        public void RoomDidDisconnectWithError(TVIRoom room, NSError error)
        {
            //LogHelper.Call(GetType());
            OnRoomDidDisconnectWithError();
        }

        [Export("room:participantDidConnect:")]
        public void RoomParticipantDidConnect(TVIRoom room, TVIParticipant participant)
        {
            //LogHelper.Call(GetType());

            if (TwilioParticipantDelegate.Instance.Participant == null)
            {
                TwilioParticipantDelegate.Instance.Participant = participant;
            }

            OnRoomParticipantDidConnect();
        }

        [Export("room:participantDidDisconnect:")]
        public void RoomParticipantDidDisconnect(TVIRoom room, TVIParticipant participant)
        {
            //LogHelper.Call(GetType());

            if (TwilioParticipantDelegate.Instance.Participant == participant)
            {
                OnRoomParticipantDidDisconnect();
            }
        }

        [Export("roomDidStartRecording:")]
        public void RoomDidStartRecording(TVIRoom room)
        {
            //LogHelper.Call(GetType());
            OnRoomDidStartRecording();
        }

        [Export("roomDidStopRecording:")]
        public void RoomDidStopRecording(TVIRoom room)
        {
            //LogHelper.Call(GetType());
            OnRoomDidStopRecording();
        }

        #endregion

        public void Finish()
        {
            //LogHelper.Call(GetType());

            if (Room != null)
            {
                Room.Disconnect();
                Room.Dispose();
                Room = null;
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
