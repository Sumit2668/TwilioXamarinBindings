using Android.App;
using Android.Widget;
using Android.OS;
using TwilioVideo;
using Android.Support.Design.Widget;
using Android.Media;
using Android.Support.V7.App;
using Org.Webrtc;
using Android.Views;
using System;
using Android.Runtime;
using Android.Content.PM;
using System.Linq;
using Android.Support.V4.App;
using Android;
using Android.Support.V7.Widget;
using Android.Support.V4.Content;
using static TwilioVideoHelper;

namespace Twilio.Video.Sample.Android
{

    [Activity(Label = "Twilio.Video.Sample.Android", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity, TwilioVideoHelper.IListener
    {
        private const int CAMERA_MIC_PERMISSION_REQUEST_CODE = 1;
        private const string TAG = "VideoActivity";

        /*
         * You must provide a Twilio Access Token to connect to the Video service
         */
        // OPTION 1- Generate an access token from the getting started portal
        // https://www.twilio.com/console/video/dev-tools/testing-tools
        private const string TWILIO_ACCESS_TOKEN = "YOUR_TOKEN";
        private const string RoomName = "TestRoom";

        const int PermissionsRequestCode = 1;

        public TwilioVideoHelper TwilioVideo { get; private set; }

        string _localVideoTrackId;
        string _remoteVideoTrackId;

        AppCompatImageView _btnEndCall;
        AppCompatCheckBox _cbMute;
        AppCompatCheckBox _btnFlip;
        TwilioVideo.VideoView _primaryVideo;
        TwilioVideo.VideoView _thumbnailVideo;
        ViewGroup _root;
        ViewGroup _thumbnailContainer;
        TextView _lblUserName;
        TextView _lblStatus;
        View _spinner;

        bool _dataUpdated;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VideoCall);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            _btnEndCall = FindViewById<AppCompatImageView>(Resource.VideoCall.btnEndCall);
            _cbMute = FindViewById<AppCompatCheckBox>(Resource.VideoCall.cbMute);
            _btnFlip = FindViewById<AppCompatCheckBox>(Resource.VideoCall.btnFlip);
            _lblUserName = FindViewById<TextView>(Resource.VideoCall.lblUserName);
            _lblStatus = FindViewById<TextView>(Resource.VideoCall.lblDuration);
            _primaryVideo = FindViewById<TwilioVideo.VideoView>(Resource.VideoCall.primaryVideo);
            _spinner = FindViewById<View>(Resource.VideoCall.pgLoading);
            _thumbnailVideo = FindViewById<TwilioVideo.VideoView>(Resource.VideoCall.thumbnailVideo);
            _root = FindViewById<ViewGroup>(Resource.VideoCall.root);
            _thumbnailContainer = FindViewById<ViewGroup>(Resource.VideoCall.thumbnailContainer);

            _primaryVideo.Visibility = ViewStates.Invisible;
            _thumbnailVideo.Visibility = ViewStates.Invisible;

            _btnFlip.Click += OnFlipButtonClick;
            _btnEndCall.Click += OnCancelButtonClick;

            bool twoOrMoreCameras = Cameras.Count() > 1;
            _btnFlip.Enabled = twoOrMoreCameras;
            _btnFlip.Visibility = twoOrMoreCameras ? ViewStates.Visible : ViewStates.Gone;

            VolumeControlStream = Stream.VoiceCall;

            bool granted = ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) == Permission.Granted
                && ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.RecordAudio) == Permission.Granted;
            CheckVideoCallPermissions(granted);

            TwilioVideo = TwilioVideoHelper.GetOrCreate(ApplicationContext);
            UpdateState();
            ConnectToRoom();

        }

        void ConnectToRoom()
        {
            TwilioVideo.UpdateToken(TWILIO_ACCESS_TOKEN);
            TwilioVideo.JoinRoom(ApplicationContext, RoomName);
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            TwilioVideo = TwilioVideoHelper.GetOrCreate(ApplicationContext);
            UpdateState();
        }

        public override bool OnSupportNavigateUp()
        {
            TryCancelCall();
            return true;
        }

        public override void OnBackPressed()
        {
            TryCancelCall();
        }

        protected override void OnStart()
        {
            base.OnStart();
            UpdateState();
        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateState();
            _cbMute.CheckedChange += OnMuteButtonCheckedChange;
        }

        protected override void OnPause()
        {
            _cbMute.CheckedChange -= OnMuteButtonCheckedChange;
            _dataUpdated = false;
            base.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            TwilioVideo.Unbind(this);
            TwilioVideo = null;
            base.OnDestroy();
        }

        void UpdateState()
        {
            if (_dataUpdated)
                return;
            _dataUpdated = true;
            TwilioVideo.Bind(this);
            UpdatingState();
        }

        protected virtual void UpdatingState()
        {
        }

        protected void TryCancelCall()
        {
            CloseScreen();
        }

        void OnCallCanceled()
        {
            CloseScreen();
        }

        protected void SoftCloseScreen()
        {
            CloseScreen();
        }

        protected void CloseScreen()
        {
            Finish();
        }

        protected virtual void FinishCall(bool hangup)
        {
            TwilioVideo.Unbind(this);
            TwilioVideo.FinishCall();
        }

        void RequestCameraAndMicrophonePermissions()
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera)
                || ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.RecordAudio))
                Toast.MakeText(this, "Need Camera and Microphone permissions to call", ToastLength.Long).Show();
            else
                ActivityCompat.RequestPermissions(this,
                    new string[] { Manifest.Permission.Camera, Manifest.Permission.RecordAudio }, PermissionsRequestCode);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == PermissionsRequestCode)
                CheckVideoCallPermissions(grantResults.Any(p => p == Permission.Denied));
        }

        void CheckVideoCallPermissions(bool granted)
        {
            if (!granted)
                RequestCameraAndMicrophonePermissions();
        }

        #region TwilioVideo.IListener

        public void SetLocalVideoTrack(LocalVideoTrack videoTrack)
        {
            var trackId = videoTrack?.TrackId;
            if (_localVideoTrackId == trackId)
                return;
            _localVideoTrackId = trackId;
            UpdateTrackRenderer(videoTrack, _thumbnailVideo);
        }

        public void SetRemoteVideoTrack(TwilioVideo.VideoTrack videoTrack)
        {
            var trackId = videoTrack?.TrackId;
            if (_remoteVideoTrackId == trackId)
                return;
            _remoteVideoTrackId = trackId;
            UpdateTrackRenderer(videoTrack, _primaryVideo);
            _spinner.Visibility = _remoteVideoTrackId == null ? ViewStates.Visible : ViewStates.Invisible;
        }

        public void RemoveLocalVideoTrack(LocalVideoTrack track)
        {
            SetLocalVideoTrack(null);
        }

        public void RemoveRemoteVideoTrack(TwilioVideo.VideoTrack track)
        {
            SetRemoteVideoTrack(null);
        }

        public virtual void OnRoomConnected(string roomId)
        {
        }

        public virtual void OnRoomDisconnected(TwilioVideoHelper.StopReason reason)
        {
        }

        public virtual void OnParticipantConnected(string participantId)
        {
        }

        public virtual void OnParticipantDisconnected(string participantId)
        {
        }

        void UpdateTrackRenderer(TwilioVideo.VideoTrack videoTrack, TwilioVideo.VideoView renderer)
        {
            if (videoTrack != null && !IsDestroyed && !IsFinishing)
                videoTrack.AddRenderer(renderer);
            renderer.Visibility = videoTrack == null ? ViewStates.Invisible : ViewStates.Visible;
        }

        #endregion

        void OnCancelButtonClick(object sender, EventArgs e)
        {
            TryCancelCall();
        }

        void OnMuteButtonCheckedChange(object sender, EventArgs e)
        {
            TwilioVideo.Mute(_cbMute.Checked);
        }

        void OnFlipButtonClick(object sender, EventArgs e)
        {
            TwilioVideo.FlipCamera();
        }

        public void SetCallTime(int seconds)
        {

        }
    }
}

