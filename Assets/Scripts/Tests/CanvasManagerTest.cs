using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProcessCanvases {
	WelcomeCanvas,
	CaptureBackgroundCanvas,
	SettingPatnerPositionCanvas,
	CompleteBeforeSettingCanvas,
	StepByStepCanvas,
	ReadyExerciseCanvas,
	ContinueExerciseCanvas,
	StopExerciseCanvas
}

public class CanvasManagerTest : MonoBehaviour {

	public int currentCanvasIndex = 0;

	private Transform processCanvasTransform;
	private string[] processCanvasesValues = Enum.GetNames (typeof(ProcessCanvases));
	private GameObject currentCanvas;

	void Start () {
		processCanvasTransform = GameObject.Find ("ProcessCanvases").transform;
		foreach (string canvas in processCanvasesValues) {
			processCanvasTransform.Find(canvas).gameObject.SetActive (false);
		}
		currentCanvas = processCanvasTransform
			.Find (processCanvasesValues [currentCanvasIndex]).gameObject;
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
		// Debug.Log (processCanvasesValues [currentCanvasIndex]);
		currentCanvas.SetActive (!currentCanvas.activeSelf);
		if (currentCanvasIndex != 7) {
			currentCanvas = processCanvasTransform
				.Find (processCanvasesValues [++currentCanvasIndex]).gameObject;
		} else {
			currentCanvasIndex = 0;
			currentCanvas = processCanvasTransform
				.Find (processCanvasesValues [currentCanvasIndex]).gameObject;
		}
		currentCanvas.SetActive (true);
		Debug.Log (processCanvasesValues [currentCanvasIndex]);
	}
}
