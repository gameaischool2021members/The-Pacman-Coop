using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verificationSound : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        Debug.Log("yo");
        if (GameObject.Find("Sound") != null)
        {

            if (GameObject.Find("Sound").GetComponent<AudioSource>().clip == GetComponent<AudioSource>().clip)
            {
                Destroy(GetComponent<AudioSource>());
                Destroy(GetComponent<AudioListener>());
            }
            else
            {
                Destroy(GameObject.Find("Sound"));
            }
        }
    }

}
