using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCV;

public class OpenCVManager : MonoBehaviour {

	OpenCVImage openCVImage;

	void Start () {
		openCVImage = OpenCVImage.Instance ();
	}

	void Update () {
		openCVImage.ProcessTransformImage ();
	}

	public void WebCamProcessFromCanvas(int canvasIndex) {
		openCVImage.SetWebCamProcessFromCanvas(canvasIndex);
	}
		
	void OnDestroy() {
		openCVImage.RemoveAllWindows ();
	}
}
