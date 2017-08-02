using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			animator.SetInteger ("Pushup State", 0);
		} else if (Input.GetKeyDown (KeyCode.W)) {
			animator.SetInteger ("Pushup State", 1);
		} else if (Input.GetKeyDown (KeyCode.E)) {
			animator.SetInteger ("Pushup State", 2);
		} else if (Input.GetKeyDown (KeyCode.R)) {
			animator.SetInteger ("Pushup State", 3);
		}
	}
}
