using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Constants;

public class ChangeCanvasManager : MonoBehaviour {

	void Start() {
		
	}

	void Update() {
		StartCoroutine ("ChangeCoroutine");
	}

	bool marker = true;
	IEnumerator ChangeCoroutine() {
		switch (GetCanvasIndex ()) {
		case (int)CanvasEnum.SettingPatnerPositionCanvas:
			if (marker) { // TODO: 마커 인식
				marker = false;
				yield return new WaitForSecondsRealtime (3.0f);
				ChangeCanvas ((int)CanvasEnum.CompleteBeforeSettingCanvas);
				NextCanvas ();
			}
			break;
		case (int)CanvasEnum.CompleteBeforeSettingCanvas:
			if (!marker) {
				marker = true;
				yield return new WaitForSecondsRealtime (3.0f);
				ChangeCanvas ((int)CanvasEnum.StepByStepCanvas);
				NextCanvas ();
			}
			break;
		case (int)CanvasEnum.StepByStepCanvas:
			break;
		default:
			break;
		}
	}

	public void SelectExercise(string exercise) {
		GameObject.Find("/ImageTarget/Beta")
			.GetComponent<BetaController>().exercise = exercise;
		ChangeCanvas ((int)CanvasEnum.CaptureBackgroundCanvas);
	}

	public void CaptureBackground() {
		ChangeCanvas ((int)CanvasEnum.SettingPatnerPositionCanvas);
	}

	void ChangeCanvas(int canvasIndex) {
		GameObject.Find ("/Managers/OpenCV Manager")
			.GetComponent<OpenCVManager> ().changeCanvas (canvasIndex);		
	}

	void NextCanvas() {
		GameObject.Find ("/Managers/Canvas Manager")
			.GetComponent<CanvasManager> ().nextCanvas();
	}

	int GetCanvasIndex() {
		return GameObject.Find ("/Managers/Canvas Manager")
			.GetComponent<CanvasManager> ().currentCanvasIndex;
	}
}
