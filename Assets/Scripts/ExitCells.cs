using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCells : MonoBehaviour {

    public static ExitCells Instance;
    public bool HasWolf, IsExtra;
    float duration = 1.5f, lerp;
    public List<GameObject> FireSpot;

    private void Awake()
    {
        Instance = this;
        this.HasWolf = false;
        IsExtra = false;
    }
   

    private void Update()
    {
        HighLight();
        if (MainMenu.Instance.AIturn)
        {
            CheckGame();
        }
    }

    void CheckGame()
    {

        foreach(ExitCells ecell in PlayerManager.Instance.PlayerCell.ExitNeighbours)
        {
            if(ecell.HasWolf)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    void HighLight()
    {
        if (PowerUps.Instance.PackFlag == 1)
        {
            foreach(ExitCells ecells in GridManager.Instance.ExitArray)
            {
                ecells.gameObject.SetActive(true);
            }
            lerp = Mathf.PingPong(Time.time, duration) / duration;
            GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.black, lerp);
        }
        if (this.HasWolf == true)
        {
            lerp = Mathf.PingPong(Time.time, duration) / duration;
            GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.black, lerp);
        }
    }

    public int ExitCellNumber;

    private void OnMouseDown()
    {
       
        if(AIManager.Instance.AICell.ExitNeighbours.Contains(this) && GridManager.Instance.turn <= 0)
        {

            AIManager.Instance.AICell.transform.position = this.transform.position;

            SceneManager.LoadScene("GameOver");

        }

        else if(PowerUps.Instance.PackFlag == 1 && GridManager.Instance.turn <= 0)
        {
            PowerUps.Instance.WolfAnimator.SetTrigger("Howl");
            this.HasWolf = true;
            foreach(ExitCells ecell in GridManager.Instance.ExitArray)
            {
                if(ecell.HasWolf == true)
                {
                    continue;

                }
                if(ecell == GridManager.Instance.ExitArray[GridManager.Instance.i])
                {
                    continue;
                }
                else
                {
                    ecell.gameObject.SetActive(false);
                }
            }
            PowerUps.Instance.PackFlag = 0;
        }

    }




}
