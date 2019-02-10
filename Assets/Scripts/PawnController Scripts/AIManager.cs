using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    public static AIManager Instance;
    public Renderer newrend;

                                


    private void Awake()
    {
        Instance = this;
        
    }

    public CellProperties AICell;

    // Use this for initialization
    void Start()
    {
        // GridManager.Instance.InitializeGrid();
        SetTransform();
    }

    public void SetTransform()
    {
        if (MainMenu.Instance.AIturn)
        {
            int j = Random.Range(1, 4);
            AICell = GridManager.Instance.Cells[3][j];
            transform.position = AICell.transform.position;
        }
        else if(MainMenu.Instance.AIturn == false && CharacterSelect.Instance.IsWolf == true)
        {

            int j = Random.Range(1, 4);
            AICell = GridManager.Instance.Cells[0][j];
            transform.position = AICell.transform.position;

        }

       

        HighlightNeighbours();

    }

    void HighlightNeighbours()
    {
        foreach (CellProperties ncell in AICell.Neighbours)
        {
            ncell.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

}


