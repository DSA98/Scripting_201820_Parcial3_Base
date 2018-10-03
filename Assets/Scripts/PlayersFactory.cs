using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Spawner
{
    public static Vector3 GetPointInVolume(this Collider collider)
    {
        Vector3 randPoint = Vector3.zero;
        randPoint = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), collider.transform.position.y, Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        return randPoint;
    }
}

[RequireComponent(typeof(Collider))]
public class PlayersFactory : MonoBehaviour {

    private static PlayersFactory factoryInstance = null;
    public static PlayersFactory FactoryInstance
    {
        get
        {
            return factoryInstance;
        }
    }

    Collider mCollider;

	// Use this for initialization
	void Awake () {
        if (factoryInstance == null)
        {
            factoryInstance = this;
        }
        else
        {
            Destroy(this);
        }
	}

    private void Start()
    {
        mCollider = GetComponent<Collider>();
    }

    public ActorController ManufacturePlayerAtRandPoint(ActorController actorneeded)
    {
        ActorController actorClone = Instantiate(actorneeded);
        actorClone.transform.position = mCollider.GetPointInVolume();
        actorClone.transform.rotation = Quaternion.identity;
        return actorClone;
    }
}
