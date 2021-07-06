using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text affichageScore;
    protected GameObject pacman;

	// Use this for initialization
	void Start () {
        pacman = GameObject.Find("pacman(Clone)");
        affichageScore.text = "Score: " + pacman.GetComponent<collisionPacman>().score;
    }
	
	// Update is called once per frame
	void Update () {
        affichageScore.text = "Score: " + pacman.GetComponent<collisionPacman>().score;
	}
}
