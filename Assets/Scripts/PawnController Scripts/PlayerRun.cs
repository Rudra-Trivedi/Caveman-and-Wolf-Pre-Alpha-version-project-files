using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour {


    public static PlayerRun Instance;
    public CellProperties temp;
    int i, j;
    public CellProperties RunCell;
    int playerx;
    int playery;
    int Difference, max;
    List<int> DiffList;
    Renderer newrend;
    public Renderer wolfrend;
    Animator AIWolfAnim;

    


    // AI is Wolf


    private void Awake()
    {
        Instance = this;
       
    }
    void Start()

    {
        max = 0;

        SetTransform();
        AIWolfAnim = GetComponent<Animator>();


    }

    private void Update()
    {
        
    }



    void SetTransform()
    {
        Debug.Log("Entered settrans");
       
        int j = Random.Range(1, 4);
        RunCell = GridManager.Instance.Cells[3][j];
        transform.position = RunCell.transform.position;

        

        foreach (CellProperties ncell in RunCell.Neighbours)
        {
            newrend = ncell.GetComponent<Renderer>();
            newrend.material.color = Color.white;
        }
        Debug.Log("Start position " + this.transform.position);
        Debug.Log("Start Cell " + RunCell);
    }

    public void EvadePlayer()

    {
        

        Debug.Log("----------------------------------------");

        playerx = PlayerManager.Instance.PlayerCell.row;
        playery = PlayerManager.Instance.PlayerCell.column;
        DiffList = new List<int>();
        max = 0;
        Debug.Log("Entered Evade Player");
        foreach (CellProperties ncell in RunCell.Neighbours)
        {
            Debug.Log("Entered the first loop in evade player");
            Difference = (Mathf.Abs(playerx - ncell.row)) + (Mathf.Abs(playery - ncell.column));
            Debug.Log("Difference: " + Difference);
            DiffList.Add(Difference);
            MaxList();
            Debug.Log("Max difference: " + max);
            if( Difference == max)
            {
                Debug.Log("Max when equal to diff: " + max);
                if(ncell.row == PlayerManager.Instance.PlayerCell.row || ncell.column == PlayerManager.Instance.PlayerCell.column)
                {
                    continue;
                }
                RunCell = ncell;
            }
        }

        AIWolfAnim.SetTrigger("Walk");

        iTween.LookTo(this.gameObject, RunCell.transform.position, 0.1f);
        iTween.MoveTo(this.gameObject, RunCell.transform.position, 5f);
     
        foreach (CellProperties ncell in RunCell.Neighbours)
        {
            
            newrend = ncell.GetComponent<Renderer>();
            newrend.material.color = Color.white;
        }
        
        Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");


    }

    void MaxList()
    {
        
        foreach(int i in DiffList)
        {
            max = Mathf.Max(max, i);
        }

        
    }

}
