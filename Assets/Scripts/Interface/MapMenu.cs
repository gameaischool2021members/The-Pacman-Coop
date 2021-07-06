using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMenu : MonoBehaviour {

    // Use this for initialization
    public string cheminMap;
    public GameObject button;
    private List<string> filesNames;

	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void loadMapButtons()
    {
        DirectoryInfo dir = new DirectoryInfo(cheminMap);
        FileInfo[] info = dir.GetFiles();

        filesNames = new List<string>();

        for (int i = 0; i < info.Length; i++)
        {
            int index = info[i].Name.IndexOf(".");
            string mapName = info[i].Name.Substring(0, index);

            if (!filesNames.Contains(mapName))
            {
                filesNames.Add(mapName);
                addButton(mapName);
            }
        }
    }

    public void DestroyButtons()
    {
        Transform parent = this.transform;

        foreach(Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    public void addButton(string fileName)
    {
        GameObject B =  Instantiate(button) as GameObject;
        B.transform.SetParent(this.transform, false);
        B.GetComponentInChildren<Text>().text = fileName;
    }
}
