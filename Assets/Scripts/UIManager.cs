using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager uiInstance = null;
    public static UIManager UiInstance
    {
        get
        {
            return uiInstance;
        }
    }

    [SerializeField]
    private Text timerText, winnerText;

    private int timeToInt;

    [SerializeField]
    GameController gameController;

    private void Awake()
    {
        if (uiInstance == null)
        {
            uiInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    private void Start()
    {
        //timerText = GetComponentInChildren<Text>();
        if (timerText == null)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameController.CurrentGameTime > 0)
        {
            timerText.text = gameController.CurrentGameTime.ToString("00");
        }
        else
        {
            timerText.text = "00";
        }
        //TODO: Set text from GameController
    }

    public void GraphWinner(string popUpMessage)
    {
        winnerText.text = popUpMessage;
    }
}