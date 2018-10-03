using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController controllerInstance = null;
    public static GameController ControllerInstance
    {
        get
        {
            return controllerInstance;
        }
    }

    public delegate void TimeIsUp();
    public event TimeIsUp OnTimeIsUp;

    [SerializeField]
    private int playersQuantity;
    private ActorController[] players;
    public ActorController lastTagged { get; set; }

    [SerializeField]
    private PlayerController playerControlled;
    [SerializeField]
    private AIController ai;

    [SerializeField]
    private float gameTime;

    public float CurrentGameTime { get; private set; }

    private bool timeIsUp = false;

    private void Awake()
    {
        if (controllerInstance == null)
        {
            controllerInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    private IEnumerator Start()
    {
        players = new ActorController[playersQuantity];
        CurrentGameTime = gameTime;

        players[0] = PlayersFactory.FactoryInstance.ManufacturePlayerAtRandPoint(playerControlled);
        int i = 1;
        // Sets the first random tagged player
        while (i < players.Length)
        {
            players[i] = PlayersFactory.FactoryInstance.ManufacturePlayerAtRandPoint(ai);
            players[i].gameObject.name = "Bot " + i;
            i++;
        }
        players = FindObjectsOfType<ActorController>();

        yield return new WaitForSeconds(0.5F);

        players[Random.Range(0, players.Length)].onActorTagged(true);
        StartCoroutine(GameTimer());
    }

    //private void Update()
    //{
    //    CurrentGameTime -= Time.deltaTime;

    //    if (CurrentGameTime <= 0F)
    //    {

    //        //Time.timeScale = 0f;
    //        if (OnTimeIsUp != null)
    //        {
    //            print("Time is up");
    //            OnTimeIsUp();
    //        }
    //        //TODO: Send GameOver event.
    //    }
    //}

    private IEnumerator GameTimer()
    {
        while (!timeIsUp)
        {
            CurrentGameTime -= Time.deltaTime;
            if (CurrentGameTime <= 0F)
            {
                timeIsUp = true;
                //Time.timeScale = 0f;
                if (OnTimeIsUp != null)
                {
                    print("Time is up");
                    OnTimeIsUp();
                    CheckWinner();
                }
                //TODO: Send GameOver event.
            }
            yield return null;
        }
    }

    private void CheckWinner()
    {
        ActorController actor = null, winner = null;
        foreach(ActorController player in players)
        {
            actor = player;
            if (winner == null)
            {
                winner = actor;
            }
            if (actor.TagsCount < winner.TagsCount)
            {
                print("Overwritten");
                winner = actor;
            }
        }
        //print(string.Format("Winner is {0}, with {1} tags", actor.gameObject.name, actor.TagsCount));
        UIManager.UiInstance.GraphWinner("Winner is "+ winner.gameObject.name + ", with " + winner.TagsCount+ " tags");
    }
}