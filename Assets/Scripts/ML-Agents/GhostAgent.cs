using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.SceneManagement;


public class GhostAgent : Agent
{
    private Pacman API = null;
    
    public string CollidedActionAgent { private get; set; } = "";

    private List<Vector2> initialPositionGhosts = null;

    private Vector2 initialPositionPacman;

    private float rewardWinning = 500.0f;
    private float penaltyGhost = -200.0f;
    private float penaltyDefault = -10.0f;

    private Vector2 tampon;
    private TileMatrix M = null;

    [SerializeField]
    private Rigidbody2D ghostBody = null;

    void Awake()
    {

        if (API == null)
        {
            API = GameObject.Find("ML-Agents").GetComponent<Pacman>();
        }

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

        //for (int i = 0; i < ghosts.Count; i++)
        //{
        //    ghosts[i].transform.localPosition = initialPositionGhosts[i];
        //}

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

    public override void OnActionReceived(ActionBuffers actions)
    {
        float speed = 4.0f;

        int directionX = 0;
        int directionY = 0;

        //Get the action index for movement
        int movement = actions.DiscreteActions[0];
        // Look up the index in the movement action list:
        if (movement == 1) { directionY = 1; }
        else if (movement == 2) { directionY = -1; }
        else if (movement == 3) { directionX = -1; }
        else if (movement == 4) { directionX = 1; }

        if (M == null)
        {
            M = GameObject.Find("LevelsGenerator").GetComponent<LevelsGenerator>().M;
        }

        VerifyTeleportationMap();

        // Apply the action results to move the Agent
        ghostBody.velocity = new Vector2((speed * directionX) * 0.24f, (speed * directionY) * 0.24f);

        if (CollidedActionAgent == "ghostDead")
        {
            SetReward(penaltyGhost);
            CollidedActionAgent = "";
        }

        else if (CollidedActionAgent == "ghostAlive")
        {
            SetReward(rewardWinning);
            CollidedActionAgent = "";
        }
        else
        {
            SetReward(penaltyDefault); //state of game is changing
        }
    }

    void VerifyTeleportationMap()
    {


        ///  permet la téléportation sur l'axe des x
        /// </summary>
        if (transform.position.x > (M.largeur - 1) * 0.24f)
        {
            transform.position = new Vector2(0, transform.position.y);
            tampon = transform.position;
        }
        if (transform.position.x < 0)
        {
            transform.position = new Vector2((M.largeur - 1) * 0.24f, transform.position.y);
            tampon = transform.position;

        }
        /// <summary>
        ///  permet la téléportation sur l'axe des y
        /// </summary>
        if (transform.position.y < -(M.hauteur - 1) * 0.24f)
        {
            transform.position = new Vector2(transform.position.x, 0);
            tampon = transform.position;

        }
        if (transform.position.y > 0)
        {
            transform.position = new Vector2(transform.position.x, -(M.hauteur - 1) * 0.24f);
            tampon = transform.position;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (API == null)
        {
            API = GameObject.Find("ML-Agents").GetComponent<Pacman>();
        }

        AddObservations(sensor, API.GetPacmen());
        AddObservations(sensor, API.GetGhosts());

        sensor.AddObservation(this.transform.localPosition);

    }

    void AddObservations(VectorSensor sensor, List<GameObject> lst)
    {
        if (lst != null)
        {
            foreach (GameObject p in lst)
                sensor.AddObservation(p.transform.localPosition);
        }

  

       

    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        

    }
}