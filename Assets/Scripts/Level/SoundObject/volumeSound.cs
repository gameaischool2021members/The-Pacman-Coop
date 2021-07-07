using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSound : MonoBehaviour
{
    public Slider sliderVal;
    public void Start()
    {

        sliderVal.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    void ValueChangeCheck()
    {
        if (GameObject.Find("Sound") == null)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = sliderVal.value;
        }
        else
        {
            GameObject.Find("Sound").GetComponent<AudioSource>().volume = sliderVal.value;
        }
    }
}
