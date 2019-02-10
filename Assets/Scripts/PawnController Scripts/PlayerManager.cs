using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance;
   

    public Renderer newrend;
    
   

    private void Awake()
    {
        Instance = this;
        
      //  newrend = transform.GetChild(1).GetComponent<Renderer>();
    }

    public CellProperties PlayerCell;

	// Use this for initialization
	void Start () {
        //GridManager.Instance.InitializeGrid();
        SetTransform();

  
    }

    void SetTransform()
    {
        
        int j = Random.Range(1, 4);
        PlayerCell = GridManager.Instance.Cells[0][j];
        transform.position = PlayerCell.transform.position;

        HighlightNeigbhours();

    }

    void HighlightNeigbhours()
    {
        foreach (CellProperties ncell in PlayerCell.Neighbours)
        {
            ncell.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    
	
}
