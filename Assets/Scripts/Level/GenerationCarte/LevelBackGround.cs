using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackGround : MonoBehaviour {

    protected TileMatrix cible;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        cible = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().tileMatrix;
        this.transform.position = new Vector3(cible.position.x + (((cible.largeur-1) * 0.24f) / 2), cible.position.y - (((cible.hauteur - 1) * 0.24f) / 2));
        this.transform.localScale = new Vector2(cible.largeur, cible.hauteur);
    }
}
