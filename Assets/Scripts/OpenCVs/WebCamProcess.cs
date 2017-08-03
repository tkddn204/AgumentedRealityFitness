using System;
using OpenCvSharp;

public interface WebCamProcess {
	void Process (Mat _srcImage, Mat _dstImage);
}
