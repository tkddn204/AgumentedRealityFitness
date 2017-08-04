using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constants;

public class ChangeCanvasManager : MonoBehaviour {
	
	public void SelectExercise(string exercise) {
		GameObject.Find("/Characters/Beta")
			.GetComponent<BetaController>().exercise = exercise;
		GameObject.Find ("/OpenCV Manager")
			.GetComponent<OpenCVManager> ().changeCanvas (
				(int)CanvasEnum.CaptureBackgroundCanvas);
	}

	public void captureBackground() {
		GameObject.Find ("/OpenCV Manager")
			.GetComponent<OpenCVManager> ().changeCanvas (
				(int)CanvasEnum.SettingPatnerPositionCanvas);
	}
}
