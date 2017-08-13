using System;
using OpenCvSharp;
using OpenCvSharp.UserInterface;


class TrackBar {

	TrackBar trackbar;

	public TrackBar() {
		trackbar = new TrackBar ();
		trackbar.CreateTrackBar ();
	}

	void CreateTrackBar() {
		Cv2.NamedWindow ("TrackBar");
	}

}