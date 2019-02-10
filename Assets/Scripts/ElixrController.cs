using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ElixrController : MonoBehaviour
{

    public static ElixrController Instance;
    public Text Elixr1;
    public Text Elixr2;
    public int Elixrvalue1;
    public int Elixrvalue2;

    private void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start()
    {
        Elixrvalue1 = Elixrvalue2 = 0;
        InvokeRepeating("ElixrFill", 1.5f, 1);
    }



    void ElixrFill()

    {
        Elixrvalue1++;
        Elixrvalue2++;

        UpdateText();
    }

    void UpdateText()
    {
        Elixr1.text ="Elixr:  " + Elixrvalue1.ToString();
        Elixr2.text ="Elixr:  " + Elixrvalue2.ToString();
    }
}
