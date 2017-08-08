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

	bool _changeCanvasLock = false;

	void Wait(float seconds, Action action) {
		_changeCanvasLock = true;
		StartCoroutine(_wait(seconds, action));
	}

	IEnumerator _wait(float time, Action callback) {
		yield return new WaitForSeconds (time);
		callback ();
		_changeCanvasLock = false;
	}

	void Update() {
		if (!_changeCanvasLock) {
			switch (currentCanvasIndex) {
			case (int)CanvasEnum.SettingPatnerPositionCanvas:
				if (foundBeta) {
					Wait (1.0f, nextCanvas);
				} 
				break;
			case (int)CanvasEnum.CompleteBeforeSettingCanvas:
				Wait (2.0f, nextCanvas);
				break;
			case (int)CanvasEnum.ReadyExerciseCanvas:
				Wait (2.0f, nextCanvas);
				break;
			default:
				break;
			}
		}
	}

	public void SelectExercise(string exercise) {
		GameObject.Find("/ImageTarget/Beta")
			.GetComponent<BetaController>().exercise = exercise;
		nextCanvas();
	}

	bool foundBeta {
		get {
			return GameObject.Find ("/ImageTarget")
				.GetComponent<BetaTrackableEventHandler>().foundBeta;
		}
	}

	void canvasListDeactive() {
		foreach (string canvas in canvasNames) {
			canvasListTransform.Find(canvas).gameObject.SetActive (false);
		}
	}

	public void changeCanvas(int canvasIndex) {
		if (currentCanvas != null) {
			currentCanvas.SetActive (false);
		}
		currentCanvasIndex = canvasIndex;
		GameObject.Find("/Managers/OpenCV Manager")
			.GetComponent<OpenCVManager>().WebCamProcessFromCanvas (canvasIndex);
		currentCanvas = canvasListTransform.Find (canvasNames [canvasIndex]).gameObject;
		currentCanvas.SetActive (true);
	}

	public void nextCanvas() {
		if (currentCanvasIndex < 0 || currentCanvasIndex > canvasNames.Length) {
			throw new IndexOutOfRangeException (
				"currentCanvasIndex가 범위에서 벗어났습니다 : " + currentCanvasIndex);
		}
		changeCanvas (++currentCanvasIndex);
	}
}
