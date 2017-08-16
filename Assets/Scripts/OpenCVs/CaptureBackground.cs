using System;

using UnityEngine;
using OpenCvSharp;

public class CaptureBackground : WebCamProcess {

    Mat backgroundImage;

    public CaptureBackground()
    {
        backgroundImage = new Mat();
    }

	public void Process (Mat _srcImage, Mat _dstImage) {
		Cv2.CvtColor (_srcImage, _dstImage, ColorConversionCodes.BGR2GRAY);
        Cv2.ImShow("BackgroundImage", _srcImage);

        backgroundImage = _dstImage.Clone();
	}
    public Mat[] getImages()
    {
        Mat[] resultBackgroundImage = new Mat[1];
        resultBackgroundImage[0] = backgroundImage;
        return resultBackgroundImage;
    }
}
