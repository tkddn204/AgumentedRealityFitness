using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constants;

public class CanvasManager : MonoBehaviour {

	public int currentCanvasIndex = 0;

	private Transform canvasListTransform;
	private string[] canvasNames = Enum.GetNames (typeof(Constants.CanvasEnum));
	private GameObject currentCanvas;

	void Start () {
		canvasListTransform = GameObject.Find ("/UI/Canvas List").transform;
		foreach (string canvas in canvasNames) {
			canvasListTransform.Find(canvas).gameObject.SetActive (false);
		}
		currentCanvas = canvasListTransform
			.Find (canvasNames [currentCanvasIndex]).gameObject;
		currentCanvas.SetActive (true);
		StartCoroutine ("UpdateCanvas");
	}

	void Update () {
	}

	float seconds = 3.0f;
	IEnumerator UpdateCanvas() {
		while (true) {
			yield return new WaitForSeconds (seconds);
			ChangeCanvas ();
		}
	}

	void ChangeCanvas() {
		currentCanvas.SetActive (false);
		if (currentCanvasIndex != 7) {
			currentCanvas = canvasListTransform
				.Find (canvasNames [++currentCanvasIndex]).gameObject;
		} else {
			currentCanvasIndex = 0;
			currentCanvas = canvasListTransform
				.Find (canvasNames [currentCanvasIndex]).gameObject;
		}
		currentCanvas.SetActive (true);
	}
}
