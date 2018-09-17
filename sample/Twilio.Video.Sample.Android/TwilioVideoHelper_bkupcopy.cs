using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Android.Content;
using Android.Media;
using Android.Runtime;
using TwilioVideo;
using AudioTrack = TwilioVideo.AudioTrack;
using Android.Hardware;

public class TwilioVideoHelper : Java.Lang.Object, Room.IListener, Participant.IListener
{
    public class Cameras
    {
        public static int Count()
        {
            return Camera.NumberOfCameras;
        }

        public static bool HasFrontCamera()
        {
            int numCameras = Camera.NumberOfCameras;
            var cameraInfo = new Camera.CameraInfo();
            for (int i = 0; i < numCameras; i++)
            {
                Camera.GetCameraInfo(i, cameraInfo);
                if (cameraInfo.Facing == CameraFacing.Front)
                    return true;
            }
            return false;
        }

        public static int GetCameraRotation()
        {
            int numCameras = Camera.NumberOfCameras;
            var cameraInfo = new Camera.CameraInfo();
            for (int i = numCameras - 1; i >= 0; i--)
            {
                Camera.GetCameraInfo(i, cameraInfo);
                if (cameraInfo.Facing == CameraFacing.Front)
                    break;
            }
            return cameraInfo.Orientation;
        }
    }

    public interface IListener
    {
        void SetLocalVideoTrack(LocalVideoTrack track);
        void SetRemoteVideoTrack(VideoTrack track);
        void RemoveLocalVideoTrack(LocalVideoTrack track);
        void RemoveRemoteVideoTrack(VideoTrack track);
        void OnRoomConnected(string roomId);
        void OnRoomDisconnected(StopReason reason);
        void OnParticipantConnected(string participantId);
        void OnParticipantDisconnected(string participantId);
        void SetCallTime(int seconds);
    }

    public enum StopReason
    {
        Error,
        VideoTrackRemoved,
        ParticipantDisconnected,
        RoomDisconnected
    }

    public static TwilioVideoHelper Instance { get; private set; }

    static volatile bool _callInProgress;
    public static bool CallInProgress
    {
        get { return _callInProgress; }
        set { _callInProgress = value; }
    }

    protected LocalVideoTrack CurrentVideoTrack { get; private set; }
    protected LocalAudioTrack CurrentAudioTrack { get; private set; }
    protected VideoTrack RemoteVideoTrack { get; private set; }
    protected AudioTrack RemoteAudioTrack { get; private set; }
    protected CameraCapturer VideoCapturer { get; private set; }
    protected Participant Participant { get; private set; }
    protected Room CurrentRoom { get; private set; }

    protected Stopwatch Timer { get; private set; } = new Stopwatch();

    public bool ClientIsReady { get { return AccessToken != null; } }

    string AccessToken;

    IListener _listener;

    AudioManager _audioManager;
    Mode _previousAudioMode;
    bool _previousSpeakerphoneOn;

    public TwilioVideoHelper() : base()
    {
    }

    public TwilioVideoHelper(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public static TwilioVideoHelper GetOrCreate(Context context)
    {
        if (Instance == null)
        {
            Instance = new TwilioVideoHelper();
        }
        if (Instance.CurrentVideoTrack == null)
            Instance.CreateLocalMedia(context);
        return Instance;
    }

    void CreateLocalMedia(Context context)
    {
        _audioManager = (AudioManager)context.GetSystemService(Context.AudioService);
        var cameraSource = Cameras.HasFrontCamera()
            ? CameraCapturer.CameraSource.FrontCamera
            : CameraCapturer.CameraSource.BackCamera;
        VideoCapturer = new CameraCapturer(context, cameraSource);
        CurrentVideoTrack = LocalVideoTrack.Create(context, true, VideoCapturer);
        CurrentAudioTrack = LocalAudioTrack.Create(context, true);
    }

    public void Bind(IListener listener)
    {
        //LogHelper.Call(GetType(), listener.ToString());
        _listener = listener;
        if (CurrentRoom != null)
            _listener.OnRoomConnected(CurrentRoom.Sid);
        if (Participant != null)
            _listener.OnParticipantConnected(Participant.Identity);
        if (CurrentVideoTrack != null)
            _listener.SetLocalVideoTrack(CurrentVideoTrack);
        if (RemoteVideoTrack != null)
            _listener.SetRemoteVideoTrack(RemoteVideoTrack);
        if (Timer.IsRunning)
            _listener.SetCallTime((int)Timer.Elapsed.TotalSeconds);
    }

    void DropRenderers(VideoTrack track)
    {
        if (track?.Renderers?.Any() == true)
            foreach (var r in track.Renderers.ToArray())
                track.RemoveRenderer(r);
    }

    public void Unbind(IListener listener)
    {
        //LogHelper.Call(GetType(), listener + " vs " + _listener);
        _listener = null;
        RemoveTracksRenderers();
    }

    public void UpdateToken(string capabilityToken)
    {
        //LogHelper.Call(GetType(), capabilityToken);
        if (AccessToken == capabilityToken)
            return;
        AccessToken = capabilityToken;
        if (CurrentRoom == null)
            return;
        CurrentRoom.Disconnect();
        CurrentRoom = null;
    }

    public void FlipCamera()
    {
        VideoCapturer?.SwitchCamera();
    }

    public void Mute(bool muted)
    {
        CurrentAudioTrack?.Enable(!muted);
    }

    public void JoinRoom(Context context, string roomname)
    {
        if (CurrentRoom != null)
            return;
        //LogHelper.Call(GetType(), roomname);
        IList<LocalVideoTrack> videoTracks = new List<LocalVideoTrack>() { CurrentVideoTrack };
        IList<LocalAudioTrack> audioTracks = new List<LocalAudioTrack>() { CurrentAudioTrack };
        var options = new ConnectOptions.Builder(AccessToken)
            .VideoTracks(videoTracks)
            .AudioTracks(audioTracks)
            .RoomName(roomname)
            .Build();
        CurrentRoom = Video.Connect(context, options, this);
        CallInProgress = true;
    }

    void RemoveTracksRenderers()
    {
        DropRenderers(RemoteVideoTrack);
        DropRenderers(CurrentVideoTrack);
    }

    void ReleaseRoom()
    {
        try
        {
            CurrentRoom?.Disconnect();
        }
        finally
        {
            CurrentRoom = null;
        }
    }

    public void ReleaseMedia()
    {
        if (VideoCapturer != null)
        {
            VideoCapturer.StopCapture();
            VideoCapturer = null;
        }
        if (CurrentVideoTrack != null)
        {
            var videoTrack = CurrentVideoTrack;
            CurrentVideoTrack = null;
            CurrentRoom?.LocalParticipant.RemoveVideoTrack(videoTrack);
            DropRenderers(videoTrack);
            videoTrack.Release();
        }
        if (CurrentAudioTrack != null)
        {
            var audioTrack = CurrentAudioTrack;
            CurrentAudioTrack = null;
            CurrentRoom?.LocalParticipant.RemoveAudioTrack(audioTrack);
            audioTrack.Enable(false);
            audioTrack.Release();
        }
    }

    void SetAudioFocus(bool focused)
    {
        if (focused)
        {
            _previousAudioMode = _audioManager.Mode;
            _previousSpeakerphoneOn = _audioManager.SpeakerphoneOn;
            _audioManager.RequestAudioFocus(null, Stream.VoiceCall, AudioFocus.GainTransient);
            _audioManager.SpeakerphoneOn = true;
            _audioManager.Mode = Mode.InCommunication;
        }
        else
        {
            _audioManager.Mode = _previousAudioMode;
            _audioManager.SpeakerphoneOn = _previousSpeakerphoneOn;
            _audioManager.AbandonAudioFocus(null);
        }
    }

    #region Media.Listener

    public void OnAudioTrackAdded(Participant participant, AudioTrack audioTrack)
    {
        //LogHelper.Call(GetType(), audioTrack.TrackId);
        RemoteAudioTrack = audioTrack;
    }

    public void OnAudioTrackDisabled(Participant participant, AudioTrack audioTrack)
    {
        //LogHelper.Call(GetType(), audioTrack.TrackId);
    }

    public void OnAudioTrackEnabled(Participant participant, AudioTrack audioTrack)
    {
        //LogHelper.Call(GetType(), audioTrack.TrackId);
    }

    public void OnAudioTrackRemoved(Participant participant, AudioTrack audioTrack)
    {
        //LogHelper.Call(GetType(), audioTrack.TrackId);
        if (RemoteAudioTrack.TrackId == audioTrack.TrackId)
            RemoteAudioTrack = null;
    }

    public void OnVideoTrackAdded(Participant participant, VideoTrack videoTrack)
    {
        //LogHelper.Call(GetType(), videoTrack.TrackId);
        RemoteVideoTrack = videoTrack;
        _listener?.SetRemoteVideoTrack(RemoteVideoTrack);
    }

    public void OnVideoTrackDisabled(Participant participant, VideoTrack videoTrack)
    {
        //LogHelper.Call(GetType(), videoTrack.TrackId);
    }

    public void OnVideoTrackEnabled(Participant participant, VideoTrack videoTrack)
    {
        //LogHelper.Call(GetType(), videoTrack.TrackId);
    }

    public void OnVideoTrackRemoved(Participant participant, VideoTrack videoTrack)
    {
        //LogHelper.Call(GetType(), videoTrack.TrackId);
        if (RemoteVideoTrack.TrackId != videoTrack.TrackId)
            return;
        _listener?.RemoveRemoteVideoTrack(RemoteVideoTrack);
        RemoteVideoTrack = null;
    }

    #endregion

    #region Room.Listener

    public void OnConnectFailure(Room room, TwilioException e)
    {
        //LogHelper.Call(GetType(), room.Name + " error=" + e);
        OnFinishConversation(StopReason.Error);
    }

    public void OnConnected(Room room)
    {
        //LogHelper.Call(GetType(), room.Name);
        CurrentRoom = room;
        _listener?.OnRoomConnected(room.Name);
        var participant = room.Participants.FirstOrDefault(p => p.Identity != room.LocalParticipant.Identity);
        if (participant != null)
            OnParticipantConnected(room, participant);
        if (_audioManager != null)
            SetAudioFocus(true);
    }

    public void OnDisconnected(Room room, TwilioException e)
    {
        //LogHelper.Call(GetType(), room.Name + " error=" + e);
        room.Dispose();
        Timer.Stop();
        if (e != null)
            OnFinishConversation(StopReason.Error);
        else
            OnFinishConversation(StopReason.RoomDisconnected);
    }

    public void OnParticipantConnected(Room room, Participant participant)
    {
        //LogHelper.Call(GetType(), room.Name + " participant=" + participant.Identity);
        Participant = participant;
        Participant.SetListener(this);
        Timer.Restart();
        _listener?.OnParticipantConnected(participant.Identity);
        var videoTrack = Participant.VideoTracks.FirstOrDefault();
        if (videoTrack != null)
            OnVideoTrackAdded(Participant, videoTrack);
    }

    public void OnParticipantDisconnected(Room room, Participant participant)
    {
        //LogHelper.Call(GetType(), room.Name + " participant=" + participant.Identity);
        if (Participant?.Identity != participant.Identity)
            return;
        Participant.SetListener(null);
        Participant = null;
        _listener?.OnParticipantDisconnected(participant.Identity);
        OnFinishConversation(StopReason.ParticipantDisconnected);
    }

    public void OnRecordingStarted(Room room = null)
    {
        //LogHelper.Call(GetType(), room?.Name);
    }

    public void OnRecordingStopped(Room room = null)
    {
        //LogHelper.Call(GetType(), room?.Name);
    }

    #endregion

    void OnFinishConversation(StopReason reason)
    {
        if (!CallInProgress)
            return;
        CallInProgress = false;
        //LogHelper.Call(GetType(), "send stop_reason: " + reason);
        RemoveTracksRenderers();
        ReleaseRoom();
        _listener?.OnRoomDisconnected(reason);
    }

    public void FinishCall()
    {
        //LogHelper.Call(GetType());

        _listener = null;

        ReleaseRoom();

        Participant = null;
        RemoteVideoTrack = null;
        RemoteAudioTrack = null;

        ReleaseMedia();

        if (_audioManager != null)
        {
            SetAudioFocus(false);
            _audioManager = null;
        }

        Timer.Stop();
    }

    protected override void Dispose(bool disposing)
    {
        //LogHelper.Call(GetType(), disposing.ToString());

        FinishCall();

        base.Dispose(disposing);
    }
}