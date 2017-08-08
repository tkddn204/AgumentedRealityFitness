using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;

public class StepByStepTextManager : MonoBehaviour {

	public Text stepText;
	public int currentStep = 1;

	private string currentExercise;

	void Start () {
		stepText = GetComponent<Text>();
	}

	void Update () {
		currentExercise = GetCurrentExercise ();
		stepText.text = currentExercise + " " + currentStep
		+ " / " + Exercise.GetStep(currentExercise) + " 단계";
	}

	string GetCurrentExercise() {
		return GameObject.Find ("/ImageTarget/Beta").gameObject
			.GetComponent<BetaController> ().exercise;
	}
}
