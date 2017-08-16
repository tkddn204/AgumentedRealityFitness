using System;
using OpenCvSharp;
using OpenCvSharp.UserInterface;


class TrackBar {

    public int thr = 100;
	public static readonly string WINDOW_NAME = "TrackBar";
    CvTrackbar trackbar;

    public TrackBar() {
        CreateTrackBar();
	}
	public void CreateTrackBar() {
		Cv2.NamedWindow (WINDOW_NAME);
		trackbar = new CvTrackbar ("thr", WINDOW_NAME, thr, 255, new CvTrackbarCallback2(callback));
	}

    void callback(int pos, object userData)
    {
        pos = 0;
        
    }
}