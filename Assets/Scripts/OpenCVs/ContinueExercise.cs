using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCvSharp;
using OpenCV;

public class ContinueExercise : WebCamProcess {

    OpenCVImage openCVImage;
    Size size;
    Point point;
    Mat _srcBinaryImage, _removeBgImage;
    Mat StructuringElement;
    
    private int currentStep = 0;

    public ContinueExercise()
    {
        _srcBinaryImage = new Mat();
        _removeBgImage = new Mat();

        openCVImage = OpenCVImage.Instance();
        size = new Size(5, 5);
        point = new Point(3, 3);
        StructuringElement = Cv2.GetStructuringElement(MorphShapes.Ellipse, size, point);
        
    }
    double compareValue = 0.0d;
    double thresh = 38.0d;
    public void Process(Mat _srcImage)
    {
        Cv2.CvtColor(_srcImage, _srcBinaryImage, ColorConversionCodes.BGR2GRAY);

        // 배경 제거
        Cv2.Subtract(openCVImage.backgroundImage, _srcBinaryImage, _removeBgImage);

        // 이진화
        Cv2.Threshold(_removeBgImage, _removeBgImage,
            thresh, 256.0d, ThresholdTypes.Binary);

        // 모폴로지 연산(Dilate(팽창) + 메디안 블러)
        Cv2.MorphologyEx(_removeBgImage, _removeBgImage, MorphTypes.DILATE, StructuringElement);
        Cv2.MedianBlur(_removeBgImage, _removeBgImage, 1);

        CompareHistogramWithStepImage(_removeBgImage);
        ChangeExercise();
        
        Cv2.ImShow("Continue Exercise(remove Background)", _removeBgImage);
    }

    private void CompareHistogramWithStepImage(Mat _removeBackgorundImage)
    {
        _removeBackgorundImage.ConvertTo(_removeBackgorundImage, MatType.CV_32F);
        Cv2.Normalize(_removeBackgorundImage,
            openCVImage.stepImage[currentStep]); // , 1.0, 0.0, NormTypes.L1

        compareValue = Cv2.CompareHist(openCVImage.stepImage[currentStep],
            _removeBackgorundImage, HistCompMethods.Chisqr);
    }
    
    private void ChangeExercise()
    {
        if (openCVImage.stepHist[0] <= openCVImage.stepHist[1])
        {
            ChangeStepAndCountUp(openCVImage.stepHist[0], openCVImage.stepHist[1]);
        } else
        {
            ChangeStepAndCountUp(openCVImage.stepHist[1], openCVImage.stepHist[0]);
        }
    }

    private void ChangeStepAndCountUp(double lowHist, double highHist)
    {
        if (currentStep == 0)
        {
            if ((compareValue >= lowHist) && (compareValue < highHist))
            {
                currentStep = 1;
            }
        }
        else
        {
            if ((compareValue >= highHist) && (currentStep == 1))
            {
                currentStep = 0;
                GameObject.Find("/UI/Canvas List/ContinueExerciseCanvas")
                    .GetComponent<ContinueExerciseCanvas>().count++;
            }
        }
    }

    public Mat[] getImages()
    {
        // nothing
        return new Mat[1];
    }
}
