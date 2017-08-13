using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopExerciseText : MonoBehaviour {

	Text text;

	GameObject ContinueExercise;
	float time;
	int count;
	int patnerCount;

	void Start () {
		text = GetComponent<Text> ();
		ContinueExercise = GameObject.Find ("/UI/Canvas List/ContinueExerciseCanvas/Text");
	}

	void Update () {
		time = ContinueExercise.GetComponent<ContinueExerciseText> ().time;
		count = ContinueExercise.GetComponent<ContinueExerciseText> ().count;
		patnerCount = ContinueExercise.GetComponent<ContinueExerciseText> ().patnerCount;
		text.text = "훌륭합니다!\n" + (int)time + "초 동안 " + count + "회 하셨네요!\n" +
			"파트너는 " + patnerCount + "회 했습니다!" + "좀 더 횟수를 늘려볼까요?";
	}
}
