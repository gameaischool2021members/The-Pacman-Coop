using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
///  This script is used to manage party objects.
/// </summary>
public class gameManagement : MonoBehaviour
{
    /// <summary>
    ///  total number of pacgum.
    /// </summary>
    public int totalpacg;
    /// <summary>
    ///  number of pacgums in the game.
    /// </summary>
    public int nbrpacg;
    /// <summary>
    ///  the time spent
    /// </summary>
    private float time = 0;
    /// <summary>
    ///  number of maps played.
    /// </summary>
    private int num;

    private int i = 0;
    private int j = 0;
    /// <summary>
    ///  list of walls that will be destroyed.
    /// </summary>
    private List<GameObject> listWallTemp = new List<GameObject>();
    /// <summary>
    ///  The unity pacman.
    /// </summary>
    private GameObject pacman;

    public List<GameObject> ghosts = new List<GameObject>();


    public Sprite aliveR;
    public Sprite aliveP;
    public Sprite aliveJ;
    public Sprite aliveB;
    public Sprite hunt;
    public Sprite dead;

    private bool hunting;

    private int huntTime;

    void Start()
    {
        /// <summary>
        ///   search for the different values.
        /// </summary>
        hunting = false;
        totalpacg = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().counter;
        num = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().num;
        listWallTemp = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().liWallTemp;
        pacman = GameObject.Find("pacman(Clone)");

        ghosts = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().ghosts;
    }

    void Update()
    {
        nbrpacg = totalpacg - (pacman.GetComponent<collisionPacman>().counter2);
        if (nbrpacg == 0)
        {
            Destroy(GameObject.Find("Main Camera").GetComponent<FlareLayer>());
            // Destroy(GameObject.Find ("Main Camera").GetComponent<GUILayer>());
            Destroy(GameObject.Find("Main Camera").GetComponent<Animator>());
            Destroy(GameObject.Find("Main Camera").GetComponent<Camera>());
            GameObject.Find("Main Camera").name = "Sound";
            DontDestroyOnLoad(GameObject.Find("Sound"));
            PlayerPrefs.SetInt("num", num + 1);
            SceneManager.LoadScene("Play");
        }
        /// <summary>
        ///  time management
        /// </summary>
        i++;
        if (i == 60)
        {
            time++;
            i = 0;
        }
        if (hunting == false)
        {
            foreach (GameObject ghost in ghosts)
            {
                if (ghost.GetComponent<SpriteRenderer>().sprite == hunt)
                {
                    hunting = true;
                }
            }
        }
        if (hunting == true)
        {
            j++;
            if (j == 60)
            {
                huntTime++;
                j = 0;
            }
        }
        if (huntTime == 8)
        {
            huntTime = 0;
            hunting = false;

            foreach (GameObject ghost in ghosts)
            {
                if (ghost.GetComponent<SpriteRenderer>().sprite != dead)
                {
                    if (ghost.tag == "ghostR")
                    {
                        ghost.GetComponent<SpriteRenderer>().sprite = aliveR;
                    }
                    if (ghost.tag == "ghostP")
                    {
                        ghost.GetComponent<SpriteRenderer>().sprite = aliveP;
                    }
                    if (ghost.tag == "ghostB")
                    {
                        ghost.GetComponent<SpriteRenderer>().sprite = aliveB;
                    }
                    if (ghost.tag == "ghostJ")
                    {
                        ghost.GetComponent<SpriteRenderer>().sprite = aliveJ;
                    }
                }
            }
        }
        /// <summary>
        ///  If 5 seconds have elapsed destroy the temporary walls
        /// </summary>
        if (time == 3)
        {
            foreach (GameObject murTemp in listWallTemp)
            {
                Destroy(murTemp);
            }
        }
    }
}
