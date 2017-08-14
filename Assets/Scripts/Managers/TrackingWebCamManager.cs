using UnityEngine;
using System.Collections;

using OpenCV;

public class TrackingWebCamManager: MonoBehaviour
{
	public Camera vuforiaCamera;

	private RenderTexture renderTexture;
	private Texture2D screenShot;
	private Rect screenShotRect;

	// Webcam size
	private const int webCamWidth = 640;
	private const int webCamHeight = 360;

	// OpenCV Image Setting Object
	private OpenCVImage openCVImage;

	void Awake() {
		openCVImage = OpenCVImage.Instance(webCamHeight, webCamWidth);

		renderTexture = new RenderTexture (webCamWidth, webCamHeight, 16);
		screenShot = new Texture2D (webCamWidth, webCamHeight);
		screenShotRect = new Rect (0, 0, webCamWidth, webCamHeight);
	}

	void start() {
	}

	void Update() {
		try {
			vuforiaCamera.targetTexture = renderTexture;
			vuforiaCamera.Render ();

			RenderTexture.active = renderTexture;

			screenShot.ReadPixels (screenShotRect, 0, 0);
			screenShot.Apply();

			openCVImage.TextureToMat (screenShot.GetPixels32());
		} finally {
			vuforiaCamera.targetTexture = null;
		}
	}

	void onDestory() {
		RenderTexture.active = null;
		Destroy (renderTexture);
	}
}
