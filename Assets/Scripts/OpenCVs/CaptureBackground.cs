using System;

using UnityEngine;
using OpenCvSharp;

public class CaptureBackground : WebCamProcess {
	TrackBar trackbar;
	public void Process (Mat _srcImage, Mat _dstImage) {
		Cv2.CvtColor (_srcImage, _dstImage, ColorConversionCodes.BGR2GRAY);
		Cv2.ImShow ("BackgroundImage", _dstImage);

		//trackbar = new TrackBar();
		//trackbar.CreateTrackBar ();
	}
}
