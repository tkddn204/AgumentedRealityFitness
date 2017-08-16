using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCvSharp;
using OpenCV;
using System;

public class ContinueExercise : WebCamProcess {

    OpenCVImage openCVImage;
    Size size;
    Point point;
    Mat _srcBinaryImage, _middleImage;
    Mat StructuringElement;

    Mat[] tempStepImage;

    TrackBar trackbar;

    public ContinueExercise()
    {
        _srcBinaryImage = new Mat();
        _middleImage = new Mat();
        tempStepImage = new Mat[2];

        openCVImage = OpenCVImage.Instance();
        size = new Size(5, 5);
        point = new Point(3, 3);
        StructuringElement = Cv2.GetStructuringElement(MorphShapes.Ellipse, size, point);

        //trackbar = new TrackBar();
    }
    double compareValue = 0.0;
    bool stepOneImageShow = false, stepTwoImageShow = false;
    public void Process(Mat _srcImage, Mat _dstImage)
    {
        Cv2.CvtColor(_srcImage, _srcBinaryImage, ColorConversionCodes.BGR2GRAY);

        // 배경 제거
        Cv2.Subtract(openCVImage.backgroundImage, _srcBinaryImage, _middleImage);
        // 이진화 + 모폴로지 연산(Dilate(팽창) + 메디안 블러) trackbar.thr
        Cv2.Threshold(_middleImage, _dstImage, 38.0d, 256.0d, ThresholdTypes.Binary);
        Cv2.MorphologyEx(_dstImage, _dstImage, MorphTypes.DILATE, StructuringElement);
        Cv2.MedianBlur(_dstImage, _dstImage, 1);

        CompareHistogram(_dstImage);
        ChangeExercise();
        
        Cv2.ImShow("Continue Exercise", _dstImage);
    }

    private void CompareHistogram(Mat _dstImage)
    {
        _dstImage.ConvertTo(_dstImage, MatType.CV_32F);
        //OpenCVImage.Instance().stepImage[currentStep].ConvertTo(OpenCVImage.Instance().stepImage[currentStep], MatType.CV_32F);
        Cv2.Normalize(_dstImage, OpenCVImage.Instance().stepImage[currentStep]); // , 1.0, 0.0, NormTypes.L1

        compareValue = Cv2.CompareHist(OpenCVImage.Instance().stepImage[currentStep],
            _dstImage, HistCompMethods.Chisqr);
    }

    int currentStep = 0;
    bool IsCounted = false;
    private void ChangeExercise()
    {
        if (currentStep == 0)
        {
            if ((compareValue >= OpenCVImage.Instance().stepOneHist) && (compareValue < OpenCVImage.Instance().stepTwoHist))
            {
                currentStep = 1;
                IsCounted = false;
            }
        } else
        {
            if ((compareValue >= OpenCVImage.Instance().stepTwoHist) && !IsCounted)
            {
                IsCounted = true;
                GameObject.Find("/UI/Canvas List/ContinueExerciseCanvas/Text").GetComponent<ContinueExerciseText>().count++;
                currentStep = 0;
            }
        }
    }

    public Mat[] getImages()
    {
        return tempStepImage;
    }
}
