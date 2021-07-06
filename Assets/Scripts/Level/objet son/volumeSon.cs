using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSon : MonoBehaviour {
	public Slider sliderVal;
	public void Start(){
		
		sliderVal.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
		}

	void ValueChangeCheck (){
		if (GameObject.Find ("Son") == null) {
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().volume = sliderVal.value;
		} else {
			GameObject.Find ("Son").GetComponent<AudioSource> ().volume = sliderVal.value;
		}
	}
}
