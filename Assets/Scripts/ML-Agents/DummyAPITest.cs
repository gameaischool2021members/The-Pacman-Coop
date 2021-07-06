using UnityEngine;
using System.Collections.Generic;

public class DummyAPITest : MonoBehaviour
{
    Pacman pacmanGame;

    void Start() 
    {
        pacmanGame = gameObject.GetComponent<Pacman>();
    }

    void Update()
    {
        List<GameObject> ghosts = pacmanGame.GetGhosts();
        Debug.Log("Ghosts: " + ghosts.Count);

        List<GameObject> bigpellets = pacmanGame.GetLargePellets();
        Debug.Log("Big Pellets: " + bigpellets.Count);

        List<Transform> walls = pacmanGame.GetWalls();
        Debug.Log("Walls: " + walls.Count);

        List<GameObject> pellets = pacmanGame.GetPellets();
        Debug.Log("Pellets: " + pellets.Count);

        List<GameObject> players = pacmanGame.GetPacmen();
        Debug.Log("Players: " + players.Count);
    }
}