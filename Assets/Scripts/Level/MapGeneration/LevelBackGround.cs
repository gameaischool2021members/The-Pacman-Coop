using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackGround : MonoBehaviour {

    protected TileMatrix target;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        target = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().tileMatrix;
        this.transform.position = new Vector3(target.position.x + (((target.width-1) * 0.24f) / 2), target.position.y - (((target.height - 1) * 0.24f) / 2));
        this.transform.localScale = new Vector2(target.width, target.height);
    }
}
