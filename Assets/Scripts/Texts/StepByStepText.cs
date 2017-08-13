using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;

public class StepByStepText : MonoBehaviour {

	Text text;

	private string currentExercise;
	private int currentStep;
	private int maxStep;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		currentExercise = GameObject.Find ("/ImageTarget/Beta")
			.GetComponent<BetaController> ().exercise;
		Init ();
		maxStep = Exercise.GetStep (currentExercise);
	}

	void Init() {
		currentStep = 1;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = currentExercise + " " + currentStep + " / " + maxStep;
	}
}
