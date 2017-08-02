﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

// Parallel computation support
using Uk.Org.Adcock.Parallel;
using System.Runtime.InteropServices;

namespace OpenCV
{
	public class OpenCVImage
	{
		private Mat sourceImage;
		private Vec3b[] sourceImageData;
		private Mat transformImage;

		private int imageHeight;
		private int imageWidth;

		public OpenCVImage (int height, int width)
		{
			this.imageHeight = height;
			this.imageWidth = width;

			sourceImage = new Mat(height, width, MatType.CV_8UC3);
			sourceImageData = new Vec3b[height * width];
			transformImage = new Mat(height, width, MatType.CV_8UC1);
		}

		// Color32 배열로 변환된 텍스쳐에서 Mat으로 변환
		public void TextureToMat(Color32[] color) {
			Parallel.For(0, imageHeight, i => {
				int index;
				Vec3b vec3b = new Vec3b(0,0,0);
				for (var j = 0; j < imageWidth; j++) {
					index = j + i * imageWidth;
					vec3b.Item0 = color[index].b;
					vec3b.Item1 = color[index].g;
					vec3b.Item2 = color[index].r;

					sourceImageData[index] = vec3b;
				}
			});

			sourceImage.SetArray(0, 0, sourceImageData);
			Cv2.Flip(sourceImage, sourceImage, FlipMode.X);
		}

		void ShowTransformImage() {
			Cv2.ImShow ("변환 이미지", transformImage);
		}

		~OpenCVImage() {
			Cv2.DestroyAllWindows();
		}
	}
}