using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCvSharp;
using OpenCV;

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

        _dstImage.ConvertTo(_dstImage, MatType.CV_32F);
        OpenCVImage.Instance().stepImage[0].ConvertTo(OpenCVImage.Instance().stepImage[0], MatType.CV_32F);
        Cv2.Normalize(_dstImage, OpenCVImage.Instance().stepImage[0], 1.0, 0.0, NormTypes.L1);

        compareValue = Cv2.CompareHist(_dstImage, OpenCVImage.Instance().stepImage[0], HistCompMethods.Chisqr);
        if(compareValue >= 11000000.0)
        {
            Debug.Log(compareValue);
            GameObject.Find("/UI/Canvas List/ContinueExerciseCanvas/Text").GetComponent<ContinueExerciseText>().count++;
        }

        Cv2.ImShow("Continue Exercise", _dstImage);
    }

    public Mat[] getImages()
    {
        return tempStepImage;
    }
}
