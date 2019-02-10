using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    public Text TurnText;
    public Text TimerDis;
    public int Timer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Timer = 5;
        if (MainMenu.Instance.AIturn == true)
        {
            InvokeRepeating("TimerDisplay", 1, 1);
        }
    }

    // Update is called once per frame
    void Update () {

        TurnTextChange();
        
	}

    void TurnTextChange()
    {
       
            if (GridManager.Instance.turn > 0)
            {
                TurnText.text = "Your Turn";
            }
            else if (GridManager.Instance.turn <= 0)
            {
                TurnText.text = "Opponent's Turn";
            }
       
    }

    void TimerDisplay()
    {
        

        TimerDis.text = "Timer:  " + Timer.ToString();
        Timer--;

        if (Timer < 0)
        {
            TimerReset();
        }
    }
    void TimerReset()
    {
        if (MainMenu.Instance.AIturn)
        {
            if (GridManager.Instance.turn > 0)
            {
                GridManager.Instance.turn--;
                Timer = 5;
                return;
            }
            if (GridManager.Instance.turn <= 0)
            {
                GridManager.Instance.turn++;
                Timer = 5;
                return;
            }
        }
        
        
    }
    public void Exit()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
