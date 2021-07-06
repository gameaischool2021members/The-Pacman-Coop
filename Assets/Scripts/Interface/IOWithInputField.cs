using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IOWithInputField : MonoBehaviour {

    public InputField Field;
    protected bool saving = false;
    protected bool charging = false;

    public void Saving()
    {
        Field.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        Field.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.85f);
        Field.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.85f);
        Field.interactable = true;
        Field.placeholder.GetComponent<Text>().text = "Nom de la carte à sauver...";
        saving = true;
        charging = false;
    }

    public void Charging()
    {
        Field.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        Field.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.85f);
        Field.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.85f);
        Field.interactable = true;
        Field.placeholder.GetComponent<Text>().text = "Nom de la carte à charger...";
        charging = true;
        saving = false;
    }

    public void SaveOrLoadLvlWithField()
    {
        if (Field.text != "")
        {
            if (saving)
            {
                GetComponent<TileEditor>().saveLvl(Field.text);
                saving = false;
                print("Sauvegarde reussi !");
            }
            else if (charging)
            {
                GetComponent<TileEditor>().loadLvl(Field.text);
                charging = false;
                print("Chargement reussi !");
            }
        }
        else
        {
            if (saving)
            {
                saving = false;
                print("Sauvegarde annulé");
            }
            else if (charging)
            {
                charging = false;
                print("Chargement annulé !");
            }
        }
        Field.placeholder.GetComponent<Text>().text = "";
    }

    public void Activate()
    {
        Field.interactable = true;
    }

    public void Desactivate()
    {
        Field.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, -0.2f);
        Field.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, -0.2f);
        Field.interactable = false;
        Field.text = "";
        saving = false;
        charging = false;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
