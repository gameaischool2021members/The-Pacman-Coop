using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verificationSon : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Debug.Log ("yo");
		if (GameObject.Find ("Son") != null) {

			if (GameObject.Find ("Son").GetComponent<AudioSource>().clip == GetComponent<AudioSource>().clip) {
				Destroy (GetComponent<AudioSource> ());
				Destroy (GetComponent<AudioListener> ());
			} else {
				Destroy (GameObject.Find ("Son"));
			}
		}
	}

}
