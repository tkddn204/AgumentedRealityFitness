using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	private GameObject Beta;

	void Start () {
		Beta = GameObject.Find ("/Characters/Beta").gameObject;
	}

	public void SetActiveCharacter(bool active) {
		Beta.SetActive (active);
	}

	public bool isActiveCharacter() {
		return Beta.activeInHierarchy;
	}

}
