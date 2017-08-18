using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCvSharp;
using OpenCV;

public class StepByStep : WebCamProcess {

	OpenCVImage openCVImage;

    Mat _srcBinaryImage, _middleImage;
	Mat StructuringElement;

    Mat _dstImageBuffer, stepImageBufferOfBuffer;
    Mat[] stepImageBuffer;

    bool stepOneImageShow = false, stepTwoImageShow = false;

    public StepByStep ()
    {
        openCVImage = OpenCVImage.Instance();

        _srcBinaryImage = new Mat();
        _middleImage = new Mat();
        StructuringElement = Cv2.GetStructuringElement(
            MorphShapes.Ellipse, new Size(5, 5), new Point(3, 3));

        _dstImageBuffer = new Mat();
        stepImageBufferOfBuffer = new Mat();
        stepImageBuffer = new Mat[2];
    }
	public void Process (Mat _srcImage) {
		Cv2.CvtColor (_srcImage, _srcBinaryImage, ColorConversionCodes.BGR2GRAY);

        // 배경 제거
        Cv2.Subtract(openCVImage.backgroundImage, _srcBinaryImage, _middleImage);

        // 이진화 + 모폴로지 연산(Dilate(팽창) + 메디안 블러) trackbar.thr
        Cv2.Threshold (_middleImage, _middleImage, 38.0d, 256.0d, ThresholdTypes.Binary);
        Cv2.MorphologyEx(_middleImage, _middleImage, MorphTypes.DILATE, StructuringElement);
        Cv2.MedianBlur(_middleImage, _middleImage, 1);

        Cv2.ImShow ("Step By Step", _middleImage);

        ChangeStep(_middleImage);
        StepImageShow();
    }

    private void ChangeStep(Mat _dstImage)
    {
        if (openCVImage.stepOne)
        {
            openCVImage.stepOne = false;
            stepImageBuffer[0] = _dstImage.Clone();
            CompareHistogram(_dstImage, 0);
            stepOneImageShow = true;
        }
        else if (openCVImage.stepTwo)
        {
            openCVImage.stepTwo = false;
            stepImageBuffer[1] = _dstImage.Clone();
            CompareHistogram(_dstImage, 1);
            stepTwoImageShow = true;
        }
    }

    private void StepImageShow()
    {
        if (stepOneImageShow)
        {
            Debug.Log("스텝원히스트 : " + openCVImage.stepHist[0]);
            Cv2.ImShow("Step 1", stepImageBuffer[0]);
        }
        if (stepTwoImageShow)
        {
            Debug.Log("스텝투히스트 : " + openCVImage.stepHist[1]);
            Cv2.ImShow("Step 2", stepImageBuffer[1]);
        }
    }

    private void CompareHistogram(Mat _dstImage, int step)
    {
        _dstImageBuffer = _dstImage.Clone();
        stepImageBufferOfBuffer = stepImageBuffer[step].Clone();

        _dstImageBuffer.ConvertTo(_dstImageBuffer, MatType.CV_32F);
        stepImageBufferOfBuffer.ConvertTo(stepImageBufferOfBuffer, MatType.CV_32F);

        Cv2.Normalize(_dstImageBuffer, stepImageBufferOfBuffer);

        openCVImage.stepHist[step] = Cv2.CompareHist(stepImageBufferOfBuffer,
            _dstImageBuffer, HistCompMethods.Chisqr);
    }

    public Mat[] getImages()
    {
        Cv2.DestroyWindow("Step By Step");
        return stepImageBuffer;
    }
}
