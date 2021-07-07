using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// test the collisions of the ghosts.
/// </summary>
public class collisionGhost : MonoBehaviour
{

    /// <summary>
    /// image needed
    /// </summary>
    public Sprite alive;
    public Sprite hunt;
    public Sprite dead;
    /// <summary>
    /// If an object enters the collication area
    /// </summary>
    void OnTriggerEnter2D(Collider2D coll)
    {
        /// <summary>
        /// collision test with the pacman or if the ghost is alive then the pacman is eaten and the game is lost.
        /// </summary>
        if (coll.gameObject.tag == "pacman" && GetComponent<SpriteRenderer>().sprite == alive)
        {
            Destroy(coll.gameObject);
            SceneManager.LoadScene("Defeat");
        }
        /// <summary>
        /// collision test with pacman if the ghost is chased then it dies.
        /// </summary>
        if (coll.gameObject.tag == "pacman" && GetComponent<SpriteRenderer>().sprite == hunt)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = dead;
            coll.gameObject.GetComponent<collisionPacman>().score += 200;
        }
    }
}
