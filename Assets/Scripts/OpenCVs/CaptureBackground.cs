using System;

using UnityEngine;
using OpenCvSharp;

public class CaptureBackground : WebCamProcess {

	public void Process (Mat _srcImage, Mat _dstImage) {
		_srcImage.CopyTo(_dstImage);
		Cv2.ImShow ("BackgroundImage", _dstImage);
	}
}
