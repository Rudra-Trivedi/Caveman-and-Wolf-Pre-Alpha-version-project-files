using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public static MainMenu Instance;
    public bool AIturn;

    private void Awake()
    {
        Instance = this;
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer_Local");
        AIturn = true;
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("CharacterSelect");
        AIturn = false;
    }

    

}
