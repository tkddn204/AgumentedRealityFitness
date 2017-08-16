using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

// Parallel computation support
using Uk.Org.Adcock.Parallel;
using System.Runtime.InteropServices;

using Constants;

namespace OpenCV
{
    public class OpenCVImage
    {
        private Mat sourceImage;
        private Vec3b[] sourceImageData;
        private Mat transformImage;

        public Mat backgroundImage;

        public bool stepOne = false, stepTwo = false;
        public Mat[] stepImage;

        private int imageHeight;
        private int imageWidth;

        private WebCamProcess webCamProcess = null;

        private OpenCVImage(int height, int width)
        {
            this.imageHeight = height;
            this.imageWidth = width;

            sourceImage = new Mat(height, width, MatType.CV_8UC3);
            sourceImageData = new Vec3b[height * width];
            transformImage = new Mat(height, width, MatType.CV_8UC3);
        }

        // Color32 배열로 변환된 텍스쳐에서 Mat으로 변환
        public void TextureToMat(Color32[] color)
        {
            Parallel.For(0, imageHeight, i =>
            {
                int index;
                Vec3b vec3b = new Vec3b(0, 0, 0);
                for (var j = 0; j < imageWidth; j++)
                {
                    index = j + i * imageWidth;
                    vec3b.Item0 = color[index].b;
                    vec3b.Item1 = color[index].g;
                    vec3b.Item2 = color[index].r;

                    sourceImageData[index] = vec3b;
                }
            });

            sourceImage.SetArray(0, 0, sourceImageData);
            Cv2.Flip(sourceImage, sourceImage, FlipMode.Y);
        }

        public void SetWebCamProcessFromCanvas(int currentCanvasIndex)
        {
            switch (currentCanvasIndex)
            {
                case (int)CanvasEnum.CaptureBackgroundCanvas:
                    webCamProcess = new CaptureBackground();
                    break;
                case (int)CanvasEnum.SettingPatnerPositionCanvas:
                    backgroundImage = webCamProcess.getImages()[0];
                    webCamProcess = null;
                    break;
                case (int)CanvasEnum.StepByStepCanvas:
                    webCamProcess = new StepByStep();
                    break;
                case (int)CanvasEnum.ReadyExerciseCanvas:
                    stepImage = webCamProcess.getImages();
                    webCamProcess = null;
                    break;
                case (int)CanvasEnum.ContinueExerciseCanvas:
                    webCamProcess = new ContinueExercise();
                    break;
                default:
                    webCamProcess = null;
                    break;
            }
        }

        public void ProcessTransformImage()
        {
            if (webCamProcess != null)
            {
                Cv2.Flip(sourceImage, sourceImage, FlipMode.Y);
                webCamProcess.Process(sourceImage, transformImage);
            }
        }

        // Singletone
        private static volatile OpenCVImage _instance;
        private static object _syncRoot = new object();

        public static OpenCVImage Instance()
        {
            return _instance;
        }

        public static OpenCVImage Instance(int height, int width)
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new OpenCVImage(height, width);
                }
            }
            return _instance;
        }

        public void RemoveAllWindows()
        {
            Cv2.DestroyAllWindows();
        }
    }
}
