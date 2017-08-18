using System;

using UnityEngine;
using OpenCvSharp;

public class CaptureBackground : WebCamProcess {

    Mat backgroundImage;

    public CaptureBackground()
    {
        backgroundImage = new Mat();
    }

	public void Process (Mat _srcImage) {
		Cv2.CvtColor (_srcImage, backgroundImage, ColorConversionCodes.BGR2GRAY);
        Cv2.ImShow("BackgroundImage", backgroundImage);
	}
    public Mat[] getImages()
    {
        Mat[] resultBackgroundImage = new Mat[1];
        resultBackgroundImage[0] = backgroundImage;
        return resultBackgroundImage;
    }
}
