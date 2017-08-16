using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCvSharp;
using OpenCV;

public class StepByStep : WebCamProcess {

	OpenCVImage openCVImage;
	Size size;
    Point point;
    Mat _srcBinaryImage, _middleImage;
	Mat StructuringElement;

    TrackBar trackbar;

	public StepByStep () {
        _srcBinaryImage = new Mat();
        _middleImage = new Mat();

        openCVImage = OpenCVImage.Instance();
		size = new Size (5, 5);
        point = new Point(3, 3);
        StructuringElement = Cv2.GetStructuringElement(MorphShapes.Ellipse, size, point);

        //trackbar = new TrackBar();
	}
	public void Process (Mat _srcImage, Mat _dstImage) {
		Cv2.CvtColor (_srcImage, _srcBinaryImage, ColorConversionCodes.BGR2GRAY);

        // 배경 제거
        Cv2.Subtract(openCVImage.backgroundImage, _srcBinaryImage, _middleImage);
        //Cv2.BitwiseAnd(_diffImage, _srcBinaryImage, _middleImage);

        // 이진화 + 모폴로지 연산(Dilate(팽창) + 메디안 블러) trackbar.thr
        Cv2.Threshold (_middleImage, _dstImage, 38.0d, 256.0d, ThresholdTypes.Binary);
        Cv2.MorphologyEx(_dstImage, _dstImage, MorphTypes.DILATE, StructuringElement);
        Cv2.MedianBlur(_dstImage, _dstImage, 1);
        


		Cv2.ImShow ("Step By Step", _dstImage);
	}
}
