using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constants;

public class CanvasManager : MonoBehaviour {

	public int currentCanvasIndex = 0;
	public GameObject currentCanvas;

	private Transform canvasListTransform;  // To grab "Canvas List" context
	private string[] canvasNames = Enum.GetNames (typeof(Constants.CanvasEnum));

	void Start () {
		canvasListTransform = GameObject.Find ("/UI/Canvas List").transform;
		canvasListDeactive ();
		changeCanvas (currentCanvasIndex);
	}

	void canvasListDeactive() {
		foreach (string canvas in canvasNames) {
			canvasListTransform.Find(canvas).gameObject.SetActive (false);
		}
	}

	public void changeCanvas(int canvasIndex) {
		currentCanvas = canvasListTransform.Find (canvasNames [canvasIndex]).gameObject;
		currentCanvas.SetActive (true);
	}

	public void nextCanvas() {
		if (currentCanvasIndex < 0 || currentCanvasIndex > canvasNames.Length) {
			throw new IndexOutOfRangeException (
				"currentCanvasIndex가 범위에서 벗어났습니다 : " + currentCanvasIndex);
		}
		currentCanvas.SetActive (false);
		changeCanvas (++currentCanvasIndex);
	}
}
