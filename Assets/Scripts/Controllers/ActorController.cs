using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class ActorController : MonoBehaviour
{
    protected NavMeshAgent agent;

    [SerializeField]
    protected Color baseColor = Color.blue;
    protected Color taggedColor = Color.red;
    protected MeshRenderer renderer;
    public delegate void OnActorTagged(bool val);
    public OnActorTagged onActorTagged;
    public bool IsTagged { get; protected set; }
    private bool canTag = false;
    public int TagsCount { get; protected set; }
    //public ActorController victim { get; set; }
    public NavMeshAgent Agent
    {
        get
        {
            return agent;
        }
        set
        {
            agent = value;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<MeshRenderer>();

        SetTagged(false);

        onActorTagged += SetTagged;
        GameController.ControllerInstance.OnTimeIsUp += GameIsOver;
    }

    protected abstract Vector3 GetTargetLocation();

    protected void MoveActor()
    {
        agent.SetDestination(GetTargetLocation());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            print("collided!");
            if (IsTagged && canTag)
            {
                canTag = false;
                otherActor.onActorTagged(true);
                otherActor.TagsCount += 1;
                GameController.ControllerInstance.lastTagged = this;
                onActorTagged(false);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        agent = null;
        renderer = null;
        onActorTagged -= SetTagged;
    }

    private void SetTagged(bool val)
    {
        IsTagged = val;
        if (IsTagged)
        {
            StartCoroutine(CanTagCoolDown());
        }
        if (!IsTagged)
        {
            agent.ResetPath();
        }

        if (renderer)
        {
            print(string.Format("Changing color to {0}", gameObject.name));
            renderer.material.color = val ? taggedColor : baseColor;
        }
    }

    private IEnumerator CanTagCoolDown()
    {
        yield return new WaitForSeconds(1f);
        //print("Can tag again");
        canTag = true;
    }

    private void GameIsOver()
    {
        agent.speed = 0;
    }
}