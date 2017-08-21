using CoreMedia;
using Foundation;
using System;

namespace Twilio.Video.Sample.iOS
{
    public class TwilioVideoViewDelegate :
            TVIVideoViewDelegate
    {
        #region Fields

        static TwilioVideoViewDelegate _instance;

        #endregion

        #region Properties

        public static TwilioVideoViewDelegate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TwilioVideoViewDelegate();
                }

                return _instance;
            }
        }

        #endregion

        #region Events

        public class DimensionsDidChangeEventArgs : EventArgs
        {
            public TVIVideoView View { get; private set; }
            public CMVideoDimensions Dimensions { get; private set; }
            public DimensionsDidChangeEventArgs(TVIVideoView view, CMVideoDimensions dimensions)
            {
                View = view;
                Dimensions = dimensions;
            }
        }

        public event EventHandler<DimensionsDidChangeEventArgs> DimensionsDidChange;
        void OnDimensionsDidChange(DimensionsDidChangeEventArgs args)
        {
            DimensionsDidChange?.Invoke(this, args);
        }

        public class OrientationDidChangeEventArgs : EventArgs
        {
            public TVIVideoView View { get; private set; }
            public TVIVideoOrientation Orientation { get; private set; }
            public OrientationDidChangeEventArgs(TVIVideoView view, TVIVideoOrientation orientation)
            {
                View = view;
                Orientation = orientation;
            }
        }

        public event EventHandler<OrientationDidChangeEventArgs> OrientationDidChange;
        void OnOrientationDidChange(OrientationDidChangeEventArgs args)
        {
            OrientationDidChange?.Invoke(this, args);
        }

        public class DidReceiveDataEventArgs : EventArgs
        {
            public TVIVideoView View { get; private set; }
            public DidReceiveDataEventArgs(TVIVideoView view)
            {
                View = view;
            }
        }

        public event EventHandler<DidReceiveDataEventArgs> DidReceiveData;
        void OnDidReceiveData(DidReceiveDataEventArgs args)
        {
            DidReceiveData?.Invoke(this, args);
        }

        #endregion

        #region Methods

        public void Finish()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        [Export("videoView:videoDimensionsDidChange:")]
        public override void VideoView(TVIVideoView view, CMVideoDimensions dimensions)
        {
            OnDimensionsDidChange(new DimensionsDidChangeEventArgs(view, dimensions));
        }

        [Export("videoView:videoOrientationDidChange:")]
        public override void VideoView(TVIVideoView view, TVIVideoOrientation orientation)
        {
            OnOrientationDidChange(new OrientationDidChangeEventArgs(view, orientation));
        }

        [Export("videoViewDidReceiveData:")]
        public override void VideoViewDidReceiveData(TVIVideoView view)
        {
            OnDidReceiveData(new DidReceiveDataEventArgs(view));
        }

        #endregion
    }
}
