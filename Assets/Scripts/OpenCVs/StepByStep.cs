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

    Mat[] tempStepImage;

    TrackBar trackbar;

	public StepByStep () {
        _srcBinaryImage = new Mat();
        _middleImage = new Mat();
        tempStepImage = new Mat[2];

        openCVImage = OpenCVImage.Instance();
		size = new Size (5, 5);
        point = new Point(3, 3);
        StructuringElement = Cv2.GetStructuringElement(MorphShapes.Ellipse, size, point);

        //trackbar = new TrackBar();
	}
    bool stepOneImageShow = false, stepTwoImageShow = false;
	public void Process (Mat _srcImage, Mat _dstImage) {
		Cv2.CvtColor (_srcImage, _srcBinaryImage, ColorConversionCodes.BGR2GRAY);

        // 배경 제거
        Cv2.Subtract(openCVImage.backgroundImage, _srcBinaryImage, _middleImage);

        // 이진화 + 모폴로지 연산(Dilate(팽창) + 메디안 블러) trackbar.thr
        Cv2.Threshold (_middleImage, _dstImage, 38.0d, 256.0d, ThresholdTypes.Binary);
        Cv2.MorphologyEx(_dstImage, _dstImage, MorphTypes.DILATE, StructuringElement);
        Cv2.MedianBlur(_dstImage, _dstImage, 1);

        Cv2.ImShow ("Step By Step", _dstImage);

        if (OpenCVImage.Instance().stepOne)
        {
            OpenCVImage.Instance().stepOne = false;
            tempStepImage[0] = _dstImage.Clone();
            stepOneImageShow = true;
        }
        else if (OpenCVImage.Instance().stepTwo)
        {
            OpenCVImage.Instance().stepTwo = false;
            tempStepImage[1] = _dstImage.Clone();
            stepTwoImageShow = true;
        }

        if (stepOneImageShow)
        {
            Cv2.ImShow("Step 1", tempStepImage[0]);
        }
        if (stepTwoImageShow)
        {
            Cv2.ImShow("Step 2", tempStepImage[1]);
        }
    }

    public Mat[] getImages()
    {
        return tempStepImage;
    }
}
