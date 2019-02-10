using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneUI : MonoBehaviour {

    public Text GameOverText;



    private void Update()
    {
        UpdateGameOver();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }


    void UpdateGameOver()

    {
        if(MainMenu.Instance.AIturn == true)
        {
            GameOverText.text = " Game Over ";
        }
        else
        {
            GameOverText.text = "Game Over \n \n \n " + CellProperties.Instance.Winner + "  Won";
        }
    }

  

}
