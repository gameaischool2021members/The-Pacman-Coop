using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorScript : MonoBehaviour {

    public Color normal = Color.white;
    public Color pressed = new Color32(69, 52, 195, 255);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor()
    {
        Color col = GetComponent<Image>().color;
        if (col == normal)
        {
            GetComponent<Image>().color = pressed;
        }
        else
        {
            GetComponent<Image>().color = normal;
        }
    }

    public void ChangeColorIfNormal()
    {
        Color col = GetComponent<Image>().color;
        if (col == normal)
        {
            GetComponent<Image>().color = pressed;
        }
    }

    public void ChangeColorIfDiferrent()
    {
        Color col = GetComponent<Image>().color;
        if (col != normal)
        {
            GetComponent<Image>().color = normal;
        }
    }
}
