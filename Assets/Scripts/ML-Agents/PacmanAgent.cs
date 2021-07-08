using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PacmanAgent : Agent
{
    private Pacman API = null;

    [SerializeField]
    private Rigidbody2D pacmanBody = null;
    private float speed = 4.0f;

    private Vector2 initialPosition;

    private float rewardWinning = 500.0f;
    private float rewardPellet = 10.0f;
    private float rewardLargePellet = 100.0f;
    private float rewardGhost = 200.0f;
    private float penaltyGhost = -200.0f;
    private float penaltyDefault = -10.0f;

    private float penaltyWall = -10.0f; //TODO: is it worth it? already penalty for changing state
    private float rewardFruit = 150.0f; //TODO: cherries?

    public string CollidedActionAgent { private get; set; } = "";

    // Start is called before the first frame update
    void Start()
    {
        API = GameObject.Find("ML-Agents").GetComponent<Pacman>();
        initialPosition = pacmanBody.transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        //TODO: update positions of pacman and ghosts, restore pellets
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (API != null) {
            //ghost
            AddObservations(sensor, API.GetGhosts());
            //pellets
            AddObservations(sensor, API.GetPellets());
            AddObservations(sensor, API.GetLargePellets());
        }
        //player
        sensor.AddObservation(this.transform.localPosition);

        //TODO: update size observations in gameobject?
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

        //TODO: pacman teleportation verify
        if (API != null)
        {
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
