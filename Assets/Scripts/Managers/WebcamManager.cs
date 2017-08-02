using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using OpenCV;


// class for video display and processed video display
// current process is canny edge detection
public class WebcamManager : MonoBehaviour
{

	// Video parameters
	public MeshRenderer WebCamTextureRenderer;
	public int deviceNumber;
	private WebCamTexture _webcamTexture;

	// Webcam size
	public const int webcamWidth = 640;
	public const int webcamHeight = 360;

	// OpenCV Setting Object
	private OpenCVImage openCVImage;

	void Start() {
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length < 0) {
			Debug.Log ("카메라가 없습니다.");
		} else {

			_webcamTexture = new WebCamTexture (devices [deviceNumber].name, webcamWidth, webcamHeight);
			WebCamTextureRenderer.material.mainTexture = _webcamTexture;
			_webcamTexture.Play ();

			openCVImage = new OpenCVImage (webcamHeight, webcamWidth);
		}
	}

	void Update() {
		if (_webcamTexture.isPlaying && _webcamTexture.didUpdateThisFrame) {
			updateWebcamTexture ();
		}
	}

	void updateWebcamTexture() {
		openCVImage.TextureToMat (_webcamTexture.GetPixels32());
		openCVImage.ShowImage ();
	}

	public void OnDestroy() {
		openCVImage.DestroyImage ();
	}
}