using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {

    public static CharacterSelect Instance;

    public bool IsWolf;
    

    private void Awake()
    {
        Instance = this;
       
    }

    public void Wolf()
    {
        SceneManager.LoadScene("Multiplayer_Local");
        IsWolf = true ;
    }

    public void Caveman()
    {
        SceneManager.LoadScene("Multiplayer_Local");
        IsWolf = false ;
    }
	
	
}
