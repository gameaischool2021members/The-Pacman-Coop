using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PacmanAgent : Agent
{
    //private Pacman API = null;
    private List<GameObject> pellets;
    private List<GameObject> largePellets;
    private List<GameObject> ghosts;

    private Rigidbody2D pacmanBody;
    private Vector3 initialPosition;

    private float rewardWinning = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        //API = gameObject.GetComponent<Pacman>();
        pacmanBody = GetComponent<Rigidbody2D>();
        initialPosition = pacmanBody.transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        //reset pacman position
        //pacmanBody.transform.localPosition = initialPosition;
        //reset enemies position
        //reset pellets position
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Enemies position
        foreach (GameObject p in ghosts)
            sensor.AddObservation(p.transform.localPosition);
        //Pacman position
        sensor.AddObservation(this.transform.localPosition);
        //Pellets position
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);

        //Pacman finished game - ate all the dots
        /*if (pellets.Count == 0 && largePellets.Count == 0)
        {
            SetReward(rewardWinning);
            EndEpisode();
        }
        else
        {
            //Pacman collided with ghost
            //can eat ghost
            //cannot eat ghost

            //Pacman collected pellets - small
            //Pacman collected pellets - big

        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //pellets = API.GetPellets();
        //largePellets = API.GetLargePellets();
        //ghosts = API.GetGhosts();
        
    }
}
