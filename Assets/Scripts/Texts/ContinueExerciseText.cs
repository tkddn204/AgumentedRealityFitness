using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;

public class ContinueExerciseText : MonoBehaviour {

	private Text text;

	public float time;
	public int count;

	public int patnerCount;

	void Update () {
		time += Time.deltaTime;
		text.text = "시간 : " + (int)time + "초" +
			"     " + "횟수 : " + count + "\n"
			+ "파트너의 운동 횟수 : " + patnerCount;
		//PatnerCountUp ();
	}

	void Start () {
		text = GetComponent<Text>();
		Init ();
	}

	public void Init() {
		time = 0.0f;
		count = 0;
		patnerCount = 0;
	}
}
