using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCV;

public class OpenCVManager : MonoBehaviour {

	OpenCVImage openCVImage;

	void Start () {
		openCVImage = OpenCVImage.Instance();
	}

	public void changeCanvas(int canvasIndex) {
		openCVImage.SetWebCamProcessFromCanvas(canvasIndex);
	}

	void Update () {
		openCVImage.ProcessTransformImage ();
	}

	void OnDestroy() {
		openCVImage.RemoveAllWindows ();
	}
}
