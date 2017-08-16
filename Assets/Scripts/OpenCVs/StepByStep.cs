using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCvSharp;
using OpenCV;
using System;

public class StepByStep : WebCamProcess {

	OpenCVImage openCVImage;
	Size size;
    Point point;
    Mat _srcBinaryImage, _middleImage;
	Mat StructuringElement;

    Mat[] stepImageBuffer;
    
	public StepByStep () {
        _srcBinaryImage = new Mat();
        _middleImage = new Mat();
        stepImageBuffer = new Mat[2];

        openCVImage = OpenCVImage.Instance();
		size = new Size (5, 5);
        point = new Point(3, 3);
        StructuringElement = Cv2.GetStructuringElement(MorphShapes.Ellipse, size, point);

        _dstImageBuffer = new Mat();
        stepImageBufferOfBuffer = new Mat();
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

        ChangeStep(_dstImage);

    }

    private void ChangeStep(Mat _dstImage)
    {
        if (OpenCVImage.Instance().stepOne)
        {
            OpenCVImage.Instance().stepOne = false;
            stepImageBuffer[0] = _dstImage.Clone();
            CompareHistogram(_dstImage, 0);
            stepOneImageShow = true;
        }
        else if (OpenCVImage.Instance().stepTwo)
        {
            OpenCVImage.Instance().stepTwo = false;
            stepImageBuffer[1] = _dstImage.Clone();
            CompareHistogram(_dstImage, 1);
            stepTwoImageShow = true;
        }

        if (stepOneImageShow)
        {
            Debug.Log("스텝원히스트 : " + OpenCVImage.Instance().stepOneHist);
            Cv2.ImShow("Step 1", stepImageBuffer[0]);
        }
        if (stepTwoImageShow)
        {
            Debug.Log("스텝투히스트 : " + OpenCVImage.Instance().stepTwoHist);
            Cv2.ImShow("Step 2", stepImageBuffer[1]);
        }
    }
    Mat _dstImageBuffer, stepImageBufferOfBuffer;
    private void CompareHistogram(Mat _dstImage, int step)
    {
        _dstImageBuffer = _dstImage.Clone();
        stepImageBufferOfBuffer = stepImageBuffer[step].Clone();
        _dstImageBuffer.ConvertTo(_dstImageBuffer, MatType.CV_32F);
        stepImageBufferOfBuffer.ConvertTo(stepImageBufferOfBuffer, MatType.CV_32F);
        Cv2.Normalize(_dstImageBuffer, stepImageBufferOfBuffer); //, 1.0, 0.0, NormTypes.L1
        if (step == 0)
        {
            OpenCVImage.Instance().stepOneHist = Cv2.CompareHist(stepImageBufferOfBuffer,
                _dstImageBuffer, HistCompMethods.Chisqr);
        } else
        {
            OpenCVImage.Instance().stepTwoHist = Cv2.CompareHist(stepImageBufferOfBuffer,
                _dstImageBuffer, HistCompMethods.Chisqr);
        }
    }

    public Mat[] getImages()
    {
        Cv2.DestroyWindow("Step By Step");
        return stepImageBuffer;
    }
}
