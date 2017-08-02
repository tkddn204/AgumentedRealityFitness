using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Constants;

public class ButtonManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectExercise(string exercise) {
		GameObject.Find("/Characters/Beta").GetComponent<BetaController>().exercise = exercise;
	}
}
