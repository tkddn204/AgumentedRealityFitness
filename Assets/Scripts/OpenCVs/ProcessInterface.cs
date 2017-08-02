using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public interface ProcessInterface {
	void Process (Mat _image);
}
