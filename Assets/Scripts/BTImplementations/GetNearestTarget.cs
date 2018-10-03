using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNearestTarget : Task {

    private ActorController[] players;
    public ActorController victim { get; set; }
    private float distanceFromPlayer;

    public override bool Execute()
    {
        if (victim == null)
        {
            GetActor();
        }
        return true;
    }

    public ActorController GetActor()
    {
        players = FindObjectsOfType<ActorController>();
        distanceFromPlayer = Mathf.Infinity;
        //ControlledAI.victim = players[0];
        foreach (ActorController player in players)
        {
            if (player != gameObject.GetComponent<ActorController>() && player != GameController.ControllerInstance.lastTagged)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < distanceFromPlayer)
                {
                    distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
                    victim = player;
                    //ControlledAI.victim = player;
                    print("Got Victim");
                }
            }
        }
        return victim;
    }
}
