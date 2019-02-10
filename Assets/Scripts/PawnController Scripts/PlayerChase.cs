using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChase : MonoBehaviour {

    public static PlayerChase Instance;
    public CellProperties temp;
    int i, j;
    public CellProperties ChaseCell;
    int playerx,chasex;
    int playery,chasey;
    Renderer newrend;
    public Renderer cavemanrend;
    Animator AICavemanAnim;
   
   
    // AI is caveman

    private void Awake()
    {
        Instance = this;
    }
    void Start()

    {
        
        SetTransform();
        AICavemanAnim = GetComponent<Animator>();
        //ChasePlayer();
        
    }

   

    void SetTransform()
    {
        Debug.Log("Entered settrans");
       
        int j = Random.Range(1, 4);
        ChaseCell = GridManager.Instance.Cells[3][j];
        transform.position = ChaseCell.transform.position;

       

        foreach (CellProperties ncell in ChaseCell.Neighbours)
        {
            newrend = ncell.GetComponent<Renderer>();
            newrend.material.color = Color.white;
        }
        Debug.Log("Start position " + this.transform.position);
        Debug.Log("Start Cell " + ChaseCell);
    }

    public void ChasePlayer()

    {
        
            playerx = AIManager.Instance.AICell.row;
            playery = AIManager.Instance.AICell.column;
            chasex = ChaseCell.row;
            chasey = ChaseCell.column;
            
            if(playerx == chasex && playery == chasey)
            {
                Debug.Log("Caught Player");
            }
            
            if (playerx > chasex && playery > chasey )
            {
                for(i=playerx; i >=chasex && i>=0 ; i--)
                {
                    for(j = playery; j>=chasey && j>=0 ; j--)
                    {
                        temp = GridManager.Instance.Cells[i][j];
                        if(ChaseCell.Neighbours.Contains(temp))
                        {
                            ChaseCell = temp;

                            AICavemanAnim.SetTrigger("Walk");

                            iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                            iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                            
                            Debug.Log("entered first condition");
                            Debug.Log(this.transform.position);
                            Debug.Log(ChaseCell);
                            ncolor();
                            return;
                        }
                      
                    }
                    
                }
            }
            if (playerx < chasex && playery < chasey)
            {
                for (i = playerx; i <= chasex && i<4 ; i++)
                {
                    for (j = playery; j <= chasey && j<4 ; j++)
                    {
                        temp = GridManager.Instance.Cells[i][j];

                        if (ChaseCell.Neighbours.Contains(temp))
                        {

                            ChaseCell = temp;
                            AICavemanAnim.SetTrigger("Walk");

                            iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                            iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                            Debug.Log("entered second condition");
                            Debug.Log(this.transform.position);
                            Debug.Log(ChaseCell);
                            ncolor();
                            return;
                        }
                     
                    }

                }
            }
            if (playerx > chasex && playery < chasey)
            {
                for (i = playerx; i >= chasex && i>=0 ; i--)
                {
                    for (j = playery; j <= chasey && j<4; j++)
                    {
                        temp = GridManager.Instance.Cells[i][j];
                        if (ChaseCell.Neighbours.Contains(temp))
                        {
                            ChaseCell = temp;
                            AICavemanAnim.SetTrigger("Walk");

                            iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                            iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                            Debug.Log("entered third condition");
                            Debug.Log(this.transform.position);
                            Debug.Log(ChaseCell);
                            ncolor();
                            return;
                        }
                        
                    
                    }

                }
            }
            if (playerx < chasex && playery > chasey)
            {
                for (i = playerx; i <= chasex && i<4 ; i++)
                {
                    for (j = playery; j >= chasey && j >=0 ; j--)
                    {
                        temp = GridManager.Instance.Cells[i][j];
                        if (ChaseCell.Neighbours.Contains(temp))
                        {
                            ChaseCell = temp;
                            AICavemanAnim.SetTrigger("Walk");

                            iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                            iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                            Debug.Log("entered fourth condition");
                            Debug.Log(this.transform.position);
                            Debug.Log(ChaseCell);
                            ncolor();
                            return;
                        }
                      
                    }

                }
            }

            if(playerx == chasex && playery > chasey)
            {
                for (j = playery; j >= chasey && j >= 0; j--)
                {
                    temp = GridManager.Instance.Cells[i][j];
                    if (ChaseCell.Neighbours.Contains(temp))
                    {
                        ChaseCell = temp;
                        AICavemanAnim.SetTrigger("Walk");

                        iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                        iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                        Debug.Log("entered fifth condition");
                        Debug.Log(this.transform.position);
                        Debug.Log(ChaseCell);
                        ncolor();

                        return;
                    }

                }
            }
            if (playerx == chasex && playery < chasey)
            {
                for (j = playery; j <= chasey && j < 4; j++)
                {
                    temp = GridManager.Instance.Cells[i][j];
                    if (ChaseCell.Neighbours.Contains(temp))
                    {
                        ChaseCell = temp;
                        AICavemanAnim.SetTrigger("Walk");

                        iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                        iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                        Debug.Log("entered sixth condition");
                        Debug.Log(this.transform.position);
                        Debug.Log(ChaseCell);
                        ncolor();

                        return;
                    }


                }
            }
            if (playerx > chasex && playery == chasey)
            {
                for (i = playerx; i >= chasex && i >= 0; i--)
                {
                    temp = GridManager.Instance.Cells[i][j];
                    if (ChaseCell.Neighbours.Contains(temp))
                    {
                        ChaseCell = temp;
                        AICavemanAnim.SetTrigger("Walk");

                        iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                        iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                        Debug.Log("entered seventh condition");
                        Debug.Log(this.transform.position);
                        Debug.Log(ChaseCell);
                        ncolor();

                        return;
                    }
                }
            }
            if (playerx < chasex && playery == chasey)
            {
                 for (i = playerx; i <= chasex && i < 4; i++)
                 {
                    temp = GridManager.Instance.Cells[i][j];
                    if (ChaseCell.Neighbours.Contains(temp))
                    {
                        ChaseCell = temp;
                        AICavemanAnim.SetTrigger("Walk");

                        iTween.LookTo(this.gameObject, ChaseCell.transform.position, 0.1f);
                        iTween.MoveTo(this.gameObject, ChaseCell.transform.position, 5f);
                        Debug.Log("entered seventh condition");
                        Debug.Log(this.transform.position);
                        Debug.Log(ChaseCell);
                        ncolor();

                        return;
                    }
                 }
            }





    }

    void ncolor()
    {

        foreach (CellProperties ncell in ChaseCell.Neighbours)
        {
            newrend = ncell.GetComponent<Renderer>();
            newrend.material.color = Color.white;
        }
    }
}
