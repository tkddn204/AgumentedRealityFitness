using System;

using UnityEngine;
using OpenCvSharp;

public class CaptureBackground : WebCamProcess {

	Mat backgroundImage;
	int currentCanvasIndex;

	public CaptureBackground () {
		backgroundImage = new Mat ();
		currentCanvasIndex = GameObject.Find ("/UI/Canvas Manager")
			.GetComponent<CanvasManager> ().currentCanvasIndex;
	}

	public void Process (Mat _srcImage, Mat _dstImage) {
		if (currentCanvasIndex == 1) {
			_srcImage.CopyTo(backgroundImage);
			Cv2.ImShow ("BackgroundImage", backgroundImage);
		}
	}
}
