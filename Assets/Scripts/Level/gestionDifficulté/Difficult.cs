using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficult : MonoBehaviour {

	public float diff ;
	void start(){
		Debug.Log ("yo");
		if (PlayerPrefs.GetInt ("speedP") == 0) {
			diff = 1;
		} else {
			if (PlayerPrefs.GetInt ("speedP") == 4) {
				diff = 1;
			}
			if (PlayerPrefs.GetInt ("speedP") == 5) {
				diff = 2;
			}

			if (PlayerPrefs.GetInt ("speedP") == 7) {
				diff = 3;
			}
		}
	}
	public void Adjustdiff(float newdiff)
    {
		diff = newdiff;
		if (diff == 1) {
			PlayerPrefs.SetInt ("speedP", 4);
			PlayerPrefs.SetInt ("speedF", 3);
		}
		if (diff == 2) {
			PlayerPrefs.SetInt ("speedP", 5);
			PlayerPrefs.SetInt ("speedF", 4);
		}
		if (diff == 3) {
			PlayerPrefs.SetInt ("speedP", 7);
			PlayerPrefs.SetInt ("speedF", 6);
		}
    }
}
