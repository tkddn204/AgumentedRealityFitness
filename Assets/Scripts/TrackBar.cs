using System;
using OpenCvSharp;
using OpenCvSharp.UserInterface;


class TrackBar {

	public static readonly string WINDOW_NAME = "TrackBar";

	public TrackBar() {
	}
	public void CreateTrackBar() {
		Cv2.NamedWindow (WINDOW_NAME);
		CvTrackbar trackbar = new CvTrackbar ("asdf", WINDOW_NAME, 120, 255,
			new CvTrackbarCallback2 (delegate(int pos, object userdata) {
				pos = 0;
			}));
	}
}