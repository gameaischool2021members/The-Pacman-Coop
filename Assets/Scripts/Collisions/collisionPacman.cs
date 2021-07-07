using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This script allows to manage collisions on pacman.
/// </summary>
public class collisionPacman : MonoBehaviour
{

    /// <summary>
    ///  initialization of unity objects
    /// </summary>

    public List<GameObject> ghosts = new List<GameObject>();
    /// <summary>
    ///  different sprite used
    /// </summary>
    public Sprite hunt;
    public Sprite dead;
    /// <summary>
    ///  counter to count the number of pacgums and superpacgums eaten
    /// </summary>
    public int counter2 { get; set; }
    /// <summary>
    ///  Pacman score during a game.
    /// </summary>
    public int score { get; set; }



    void OnCollisionEnter2D(Collision2D coll)
    {
        /// <summary>
        ///  collision test with pacGums then it destroys it and increments the eaten pacGums counter + score.
        /// </summary>
        if (coll.gameObject.tag == "pacgum")
        {
            Destroy(coll.gameObject);
            counter2++;
            score += 10;
        }
        /// <summary>
        ///  collision test with the cherry then it destroys it and increments the counter of eaten pacgum + score.
        /// </summary>
        if (coll.gameObject.tag == "cherry")
        {
            Destroy(coll.gameObject);
            counter2++;
            score += 150;
        }
        /// <summary>
        /// collision test with superPacGums then it destroys it and increments the eaten pacgums counter + score.
        /// and if the ghosts are not dead (images with just the eyes) then put them in hunt (blue images).
        /// and puts the pacman object in hunter state.

        /// </summary>
        if (coll.gameObject.tag == "superPacGums")
        {
            Destroy(coll.gameObject);
            ghosts = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().ghosts;
            foreach (GameObject ghost in ghosts)
            {
                if (ghost.GetComponent<SpriteRenderer>().sprite != dead)
                {
                    ghost.gameObject.GetComponent<SpriteRenderer>().sprite = hunt;
                }
            }
            score += 100;
            counter2++;
        }

    }


}