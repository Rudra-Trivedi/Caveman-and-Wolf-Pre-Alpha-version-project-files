using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerScript : MonoBehaviour {

    public static SecondPlayerScript Instance;
    public Renderer newrend;
    public int turn;
    private readonly int turndet;
    
    int Lifetime;



    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        InvokeRepeating("LifeSpan", 1, 1);
        Lifetime = 20;
    }

    public CellProperties SecondPlayerCell;

    public void SetTransform()
    {
      
        SecondPlayerCell = PlayerManager.Instance.PlayerCell;
        transform.position = SecondPlayerCell.transform.position;
        HighlightNeigbhours();

    }

    void HighlightNeigbhours()
    {
        foreach (CellProperties ncell in SecondPlayerCell.Neighbours)
        {
            ncell.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    void LifeSpan()
    {
        Lifetime--;
        if(Lifetime <= 0)
        {
            if(gameObject.activeInHierarchy)
            {
                PowerUps.Instance.DoubleActive = false;
                gameObject.SetActive(false);
                Destroy(this);
            }
        }
    }



}
