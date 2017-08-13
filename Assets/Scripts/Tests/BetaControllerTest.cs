using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaControllerTest : MonoBehaviour {

	public string exercise;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			//animator.SetInteger ("Pushup", 0);
			animator.SetInteger ("Situp", 0);
		} else if (Input.GetKeyDown (KeyCode.W)) {
			//animator.SetInteger ("Pushup", 1);
			animator.SetInteger ("Situp", 1);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			//animator.SetInteger ("Pushup", 2);
			animator.SetInteger ("Situp", 2);
		} else if (Input.GetKeyDown (KeyCode.R)) {
			//animator.SetInteger ("Pushup", 3);
			animator.SetInteger ("Situp", 3);
		}
	}
}
