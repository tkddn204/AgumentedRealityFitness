using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;
using OpenCV;

public class WebCamManager : MonoBehaviour {

    private Texture2D webCamImage;

    // Webcam size
    private const int webCamWidth = 640;
    private const int webCamHeight = 480;

    // OpenCV Image Setting Object
    private OpenCVImage openCVImage;
    
    void Awake() {
        openCVImage = OpenCVImage.Instance(webCamHeight, webCamWidth);
        webCamImage = new Texture2D(webCamHeight, webCamWidth);
    }
	
	// Update is called once per frame
	void Update () {
        if (CameraDevice.Instance.SetFrameFormat(Image.PIXEL_FORMAT.RGBA8888, true)) {
            CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.RGBA8888).CopyToTexture(webCamImage);
            openCVImage.TextureToMat(webCamImage.GetPixels32());
        }
    }
}
