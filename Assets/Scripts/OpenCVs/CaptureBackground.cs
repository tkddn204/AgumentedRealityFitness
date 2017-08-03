using System;

using UnityEngine;
using OpenCvSharp;

public class CaptureBackground : WebCamProcess {
	//BackgroundSubtractor mog2;

	public CaptureBackground() {
		//mog2 = BackgroundSubtractorMOG2.Create(10, 16, false);
	}

	public void Process (Mat _srcImage, Mat _dstImage) {
		//mog2.Apply (_srcImage, _dstImage, 0.1d);
		//Debug.Log("들어옴!");
	}
}
