using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCvSharp;
using OpenCV;

public class StepByStep : WebCamProcess {

	OpenCVImage openCVImage;

	Size size;
	Mat blurElement;

	public StepByStep () {
		openCVImage = OpenCVImage.Instance();
		size = new Size (7, 7);
		blurElement = Cv2.GetStructuringElement(MorphShapes.Ellipse, size, new Point(3, 3));
	}
	public void Process (Mat _srcImage, Mat _dstImage) {
		Cv2.CvtColor (_srcImage, _dstImage, ColorConversionCodes.BGR2GRAY);

		Cv2.Absdiff (_dstImage, openCVImage.backgroundImage, _dstImage);

		Cv2.Threshold (_dstImage, _dstImage, 50.0d, 256.0d, ThresholdTypes.Binary);

		Cv2.GaussianBlur (_dstImage, _dstImage, size, 5);
		Cv2.Dilate (_dstImage, _dstImage, blurElement);

		//Cv2.CalcOpticalFlowPyrLK(

		Cv2.ImShow ("Step By Step", _dstImage);
	}
}
