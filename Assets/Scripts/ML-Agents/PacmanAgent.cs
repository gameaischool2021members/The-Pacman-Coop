using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;


public class PacmanAgent : Agent
{
    private Pacman API = null;
    private TileMatrix M = null;

    [SerializeField]
    private GameObject pellet = null;
    [SerializeField]
    private GameObject largePellet = null;

    [SerializeField]
    private Rigidbody2D pacmanBody = null;
    private float speed = 4.0f;

    private float rewardWinning = 500.0f;
    private float rewardPellet = 10.0f;
    private float rewardLargePellet = 100.0f;
    private float rewardGhost = 200.0f;
    private float penaltyGhost = -200.0f;
    private float penaltyDefault = -10.0f;

    private float penaltyWall = -10.0f; //TODO: is it worth it? already penalty for changing state
    private float rewardFruit = 150.0f; //TODO: cherries? maybe its okay not to consider them!

    public string CollidedActionAgent { private get; set; } = "";

    private List<Vector2> initialPositionGhosts = null;

    private Vector2 initialPositionPacman;

    void Awake()
    {
        
        if (API == null)
        {
            API = GameObject.Find("ML-Agents").GetComponent<Pacman>();
        }
        initialPositionPacman = this.transform.localPosition;
        
        initialPositionGhosts = new List<Vector2>();
        List<GameObject> ghosts = API.GetGhosts();
        for (int i = 0; i < ghosts.Count; i++)
        {
            initialPositionGhosts.Add(ghosts[i].transform.localPosition);
        }

    }

    public override void OnEpisodeBegin()
    {
         if (API == null)
         {
             API = GameObject.Find("ML-Agents").GetComponent<Pacman>();
         }

         //update pacman pos
         this.transform.localPosition = initialPositionPacman;
         //update ghosts pos
         List<GameObject> ghosts = API.GetGhosts();

         for (int i = 0; i < ghosts.Count; i++)
         {
             ghosts[i].transform.localPosition = initialPositionGhosts[i];
         }

        //restore pellets
        List<GameObject> pellets = API.GetPellets();

        for (int i = 0; i < pellets.Count; i++)
         {
            pellets[i].SetActive(true);
         }

        //restore large pellets
        List<GameObject> largePellets = API.GetLargePellets();

        for (int i = 0; i < largePellets.Count; i++)
        {
            largePellets[i].SetActive(true);
        }

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (API == null)
        {
            API = GameObject.Find("ML-Agents").GetComponent<Pacman>();
        }

        //ghost + (2*4ghosts default)
        AddObservations(sensor, API.GetGhosts());
        //pellets
        //AddObservations(sensor, API.GetPellets());
        //AddObservations(sensor, API.GetLargePellets());
        
        //player +2
        sensor.AddObservation(this.transform.localPosition);
        //TODO: observations cant change
    }

    void AddObservations(VectorSensor sensor, List<GameObject> lst)
    {
        if (lst != null)
        {
            foreach (GameObject p in lst)
                sensor.AddObservation(p.transform.localPosition);
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        int directionX = 0;
        int directionY = 0;

        //Get the action index for movement
        int movement = actions.DiscreteActions[0];
        // Look up the index in the movement action list:
        if (movement == 1) { directionY = 1; }
        else if (movement == 2) { directionY = -1; }
        else if (movement == 3) { directionX = -1; }
        else if (movement == 4) { directionX = 1; }

        //Animation
        GetComponent<Animator>().SetFloat("DirX", directionX);
        GetComponent<Animator>().SetFloat("DirY", directionY);
        // Apply the action results to move the Agent
        pacmanBody.velocity = new Vector2((speed * directionX) * 0.24f, (speed * directionY) * 0.24f);

        if (M == null)
        {
            M = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().M;
        }

        VerifyTeleportationMap();

        if (API == null)
        {
            API = GameObject.Find("ML-Agents").GetComponent<Pacman>();
        }
        List<GameObject> pellets = API.GetPellets();
        List<GameObject> largePellets = API.GetLargePellets();

        //Pacman finished game - ate all the dots
        if (pellets.Count == 0 && largePellets.Count == 0)
        {
            SetReward(rewardWinning);
            EndEpisode();
        }
        else
        {
            if (CollidedActionAgent == "pellet")
            {
                SetReward(rewardPellet); //consumed pellet
                CollidedActionAgent = "";
            }
            else if (CollidedActionAgent == "largePellet")
            {
                SetReward(rewardLargePellet); //consumed large pellet
                CollidedActionAgent = "";
            }
            else if (CollidedActionAgent == "ghostDead")
            {
                SetReward(rewardGhost); //captured ghost
                CollidedActionAgent = "";
            }
            else if (CollidedActionAgent == "ghostAlive")
            {
                SetReward(penaltyGhost); //captured by ghost
                CollidedActionAgent = "";
                EndEpisode();
            }
            else
            {
                SetReward(penaltyDefault); //state of game is changing
            }

        }
    }

    void VerifyTeleportationMap()
    {
        //Taken from deplacementPacman.cs
        ///  permet la téléportation sur l'axe des x
        if (transform.position.x > (M.largeur - 1) * 0.24f)
        {
            transform.position = new Vector2(0, transform.position.y);
        }
        if (transform.position.x < 0)
        {
            transform.position = new Vector2((M.largeur - 1) * 0.24f, transform.position.y);
        }
        ///  permet la téléportation sur l'axe des y
        if (transform.position.y < -(M.hauteur - 1) * 0.24f)
        {
            transform.position = new Vector2(transform.position.x, 0);
        }
        if (transform.position.y > 0)
        {
            transform.position = new Vector2(transform.position.x, -(M.hauteur - 1) * 0.24f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        if (Input.GetKey("up")) {
            discreteActionsOut[0] = 1; 
        }
        else if (Input.GetKey("down"))
        {
            discreteActionsOut[0] = 2;
        }
        else if (Input.GetKey("left"))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey("right"))
        {
            discreteActionsOut[0] = 4;
        }
    }
}
