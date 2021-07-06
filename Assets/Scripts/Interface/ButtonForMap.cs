using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonForMap : MonoBehaviour {


    private static List<string> MapsList;
    private static List<GameObject> ButtonsList;

	// Use this for initialization
	void Start () {
        MapsList = new List<string>();
        ButtonsList = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void addMapToPlay()
    {
        GameObject TileEditor = GameObject.Find("TileEditor");
        TileEditor.GetComponent<TileEditor>().addMap(this.GetComponentInChildren<Text>().text);
    }

    public void LoadMap()
    {
        GameObject TileEditor = GameObject.Find("TileEditor");
        GameObject canvas = GameObject.Find("CanvasLoadMapMenu");
        GameObject panel = GameObject.Find("PanelLoadMapMenu");

        TileEditor.GetComponent<TileEditor>().loadLvl(this.GetComponentInChildren<Text>().text);
        panel.GetComponent<MapMenu>().DestroyButtons();
        canvas.GetComponent<MenuPause>().ChangeState();
    }

    public void SelectMap()
    {
        string mapName = this.GetComponentInChildren<Text>().text;

        if (!MapsList.Contains(mapName))
        {
            MapsList.Add(mapName);
        }
        else
        {
            MapsList.Remove(mapName);
        }
    }

    public void PlaySelectedMaps()
    {
        GameObject TileEditor = GameObject.Find("TileEditor");
        TileEditor.GetComponent<TileEditor>().playLvls(MapsList);
    }

    public void DeleteSelectedMaps()
    {
        while(MapsList.Count > 0)
        {
            File.Delete("Maps/" + MapsList[0] + ".map");
            MapsList.RemoveAt(0);
        }
    }
}
