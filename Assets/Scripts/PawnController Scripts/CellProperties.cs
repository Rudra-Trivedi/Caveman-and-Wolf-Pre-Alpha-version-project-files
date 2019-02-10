using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CellProperties : MonoBehaviour
{
    public static CellProperties Instance;
    public int row = 0;
    public int column = 0;
    public List<CellProperties> Neighbours;
    public List<ExitCells> ExitNeighbours;
    float duration = 1.5f;
    public string Winner;
    float speed;
    public Text WindText;
   
  //  public Animator SnoopyAnimator;
    
    
    float lerp;

    public Renderer rend;

    private void Awake()
    {
        Instance = this;
      
        rend = GetComponent<Renderer>();
        rend.material.color = Color.black;
    }
    private void Start()
    {
        

        speed = 1 * Time.deltaTime;
       

    }
    private void Update()
    {
        PlayerHighlight();
        GameOver();

    }

    void GameOver()
    {
        
        if (MainMenu.Instance.AIturn == true)
        {

            GameOverMulti();

        }
        if (MainMenu.Instance.AIturn == false)
        {

            GameOverSingle();

        }
    }

    
   
    void PlayerHighlight()
    {
       
            if (GridManager.Instance.turn > 0)
            {

                lerp = Mathf.PingPong(Time.time, duration) / duration;
                PlayerManager.Instance.newrend.material.color = Color.Lerp(Color.black, Color.white, lerp);
                if (PowerUps.Instance.DoubleActive)
                {
                    SecondPlayerScript.Instance.newrend.material.color = Color.Lerp(Color.black, Color.white, lerp);
                }

            }
        
       
            
        
        else if (GridManager.Instance.turn <= 0 )
        {

            lerp = Mathf.PingPong(Time.time, duration) / duration;
            AIManager.Instance.newrend.material.color = Color.Lerp(Color.black, Color.white, lerp);

        }
        else if (MainMenu.Instance.AIturn == false)
        {

            if (CharacterSelect.Instance.IsWolf == false)
            {
                lerp = Mathf.PingPong(Time.time, duration) / duration;
                PlayerRun.Instance.wolfrend.material.color = Color.Lerp(Color.magenta, Color.white, lerp);
            }
            if (CharacterSelect.Instance.IsWolf == true)
            {

                lerp = Mathf.PingPong(Time.time, duration) / duration;
                PlayerChase.Instance.cavemanrend.material.color = Color.Lerp(Color.magenta, Color.white, lerp);
            }


        }
        if (GridManager.Instance.turn > 0 )
        {
    //        AIManager.Instance.newrend.material.color = Color.cyan;
        }
        if (GridManager.Instance.turn <= 0 )
        {
           
           // PlayerManager.Instance.newrend.material.color = Color.white;
        }
        if(MainMenu.Instance.AIturn == false)
        {
            if (CharacterSelect.Instance.IsWolf == false)
            {
                PlayerRun.Instance.wolfrend.material.color = Color.magenta;
            }
            if (CharacterSelect.Instance.IsWolf == true)
            {
                PlayerChase.Instance.cavemanrend.material.color = Color.magenta;
            }
        }
    }


    private void OnMouseDown()
    {
        

        if (PowerUps.Instance.BolaFlag == 1 && GridManager.Instance.turn > 0 )
        {
            iTween.LookTo(PlayerManager.Instance.gameObject, this.transform.position, 0.1f);
            PowerUps.Instance.SnoopyAnimator.SetTrigger("BolaThrow");
            PowerUps.Instance.BolaSpawn(this.transform.position, this);
            PowerUps.Instance.BolaFlag = 0;
            return;

        }

        if (PowerUps.Instance.TrapFlag == 1 && GridManager.Instance.turn > 0)
        {

            PowerUps.Instance.TrapSpawn(this.transform.position, this);
            PowerUps.Instance.TrapFlag = 0;
            return;

        }

        if (MainMenu.Instance.AIturn == true)
        {

            MultiPlayerTurnCheck();

        }

        else if (MainMenu.Instance.AIturn == false)
        {

            SinglePlayerTurnCheck();

        }


        ResetNeighbours();

        ResetRingOfFire();
       

    }

  

    void GameOverMulti()
    {
        if (MainMenu.Instance.AIturn)
        {
            if (PlayerManager.Instance.PlayerCell == AIManager.Instance.AICell)
            {
                Debug.Log("Game Over");

                SceneManager.LoadScene("GameOver");
            }
            if (PowerUps.Instance.DoubleActive)
            {
                if (SecondPlayerScript.Instance.SecondPlayerCell == AIManager.Instance.AICell)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }

    void GameOverSingle()
    {
        
        if (CharacterSelect.Instance.IsWolf == false)
        {
            if (PlayerManager.Instance.PlayerCell == PlayerRun.Instance.RunCell)
            {
                Winner = " Player";

                SceneManager.LoadScene("GameOver");
            }

            if (PowerUps.Instance.DoubleActive)
            {
                if (SecondPlayerScript.Instance.SecondPlayerCell == PlayerRun.Instance.RunCell)
                {
                    Winner = " Player";

                    SceneManager.LoadScene("GameOver");
                }
            }
        }
        if (CharacterSelect.Instance.IsWolf == true)
        {
            if (AIManager.Instance.AICell == PlayerChase.Instance.ChaseCell)
            {
                Winner = " Opponent";

                SceneManager.LoadScene("GameOver");
            }

            if (PowerUps.Instance.DoubleActive)
            {
                if (SecondPlayerScript.Instance.SecondPlayerCell == PlayerChase.Instance.ChaseCell)
                {
                    Winner = " Opponent";

                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }


    void MultiPlayerTurnCheck()
    {
        if (PlayerManager.Instance.PlayerCell.Neighbours.Contains(this) && GridManager.Instance.turn > 0 )
        {
            UIManager.Instance.Timer = 5;
            PowerUps.Instance.SnoopyAnimator.SetTrigger("Walk");
            Debug.Log(PowerUps.Instance.SnoopyAnimator.gameObject.name);
            //   SnoopyAnimator.SetTrigger("Walk");
            // SnoopyAnimator.Play("Walking");
            iTween.LookTo(PlayerManager.Instance.gameObject, this.transform.position, 0.1f);
            iTween.MoveTo(PlayerManager.Instance.gameObject, this.transform.position, 5);
            PlayerManager.Instance.PlayerCell = this;
          
           // rend.material.color = Color.red;
           // transform.localScale = new Vector3(1f, 1f, 1f);
            foreach (CellProperties ncell in Neighbours)
            {
                ncell.rend.material.mainTexture = GridManager.Instance.HighlightTexture;
            }


            GridManager.Instance.turn--;



        }

        else if (PowerUps.Instance.DoubleActive && GridManager.Instance.turn > 0)
        {
            if (SecondPlayerScript.Instance.SecondPlayerCell.Neighbours.Contains(this))
            {
                UIManager.Instance.Timer = 5;
                if (PowerUps.Instance.SecondSnoopyAnimator.gameObject.activeSelf)
                {
                    PowerUps.Instance.SecondSnoopyAnimator.SetTrigger("Walk");
                }
                iTween.LookTo(SecondPlayerScript.Instance.gameObject, this.transform.position, 0.1f);
                iTween.MoveTo(SecondPlayerScript.Instance.gameObject, this.transform.position, 5);
                SecondPlayerScript.Instance.SecondPlayerCell = this;
               // rend.material.color = Color.red;
               // transform.localScale = new Vector3(1f, 1f, 1f);
                foreach (CellProperties ncell in Neighbours)
                {
                    ncell.rend.material.color = Color.blue;
                }


                GridManager.Instance.turn--;



            }
        }


        else if (AIManager.Instance.AICell.Neighbours.Contains(this) && GridManager.Instance.turn <= 0)
        {

            UIManager.Instance.Timer = 5;

            PowerUps.Instance.WolfAnimator.SetTrigger("Walk");
            iTween.LookTo(AIManager.Instance.gameObject, this.transform.position, 0.1f);
            iTween.MoveTo(AIManager.Instance.gameObject, this.transform.position, 5f);
            AIManager.Instance.AICell = this;
         
            // rend.material.color = Color.red;
            // transform.localScale = new Vector3(1f, 1f, 1f);
            foreach (CellProperties ncell in Neighbours)
            {
                ncell.rend.material.mainTexture = GridManager.Instance.HighlightTexture;
            }



            GridManager.Instance.turn++;



        }
    }


    void SinglePlayerTurnCheck()
    {
        if (CharacterSelect.Instance.IsWolf == false)
        {
            if (PlayerManager.Instance.PlayerCell.Neighbours.Contains(this) && GridManager.Instance.turn > 0)
            {

                UIManager.Instance.Timer = 5;
                PowerUps.Instance.SnoopyAnimator.SetTrigger("Walk");
               
                iTween.LookTo(PlayerManager.Instance.gameObject, this.transform.position, 0.1f);
                iTween.MoveTo(PlayerManager.Instance.gameObject, this.transform.position, 5);
                PlayerManager.Instance.PlayerCell = this;
               // rend.material.color = Color.red;
              //  transform.localScale = new Vector3(1f, 1f, 1f);
                foreach (CellProperties ncell in Neighbours)
                {
                    ncell.rend.material.color = Color.blue;
                }


                PlayerRun.Instance.EvadePlayer();


            }
            if (PowerUps.Instance.DoubleActive)
            {
                if (SecondPlayerScript.Instance.SecondPlayerCell.Neighbours.Contains(this) && GridManager.Instance.turn > 0)
                {
                    UIManager.Instance.Timer = 5;
                    if (PowerUps.Instance.SecondSnoopyAnimator.gameObject.activeSelf)
                    {
                        PowerUps.Instance.SecondSnoopyAnimator.SetTrigger("Walk");
                    }
                    iTween.LookTo(SecondPlayerScript.Instance.gameObject, this.transform.position, 0.1f);
                    iTween.MoveTo(SecondPlayerScript.Instance.gameObject, this.transform.position, 5);

                    SecondPlayerScript.Instance.SecondPlayerCell = this;
                   // rend.material.color = Color.red;
                  //  transform.localScale = new Vector3(1f, 1f, 1f);
                    foreach (CellProperties ncell in Neighbours)
                    {
                        ncell.rend.material.color = Color.blue;
                    }


                    PlayerRun.Instance.EvadePlayer();



                }
            }
        }
        if (CharacterSelect.Instance.IsWolf == true)
        {
            if (AIManager.Instance.AICell.Neighbours.Contains(this) && GridManager.Instance.turn <= 0)
            {
                UIManager.Instance.Timer = 5;

                PowerUps.Instance.WolfAnimator.SetTrigger("Walk");
               
                iTween.LookTo(AIManager.Instance.gameObject, this.transform.position, 0.1f);
                iTween.MoveTo(AIManager.Instance.gameObject, this.transform.position, 5);
                AIManager.Instance.AICell = this;
              //  rend.material.color = Color.red;
              //  transform.localScale = new Vector3(1f, 1f, 1f);
                foreach (CellProperties ncell in Neighbours)
                {
                    ncell.rend.material.color = Color.blue;
                }


                PlayerChase.Instance.ChasePlayer();


            }
            //if (PowerUps.Instance.DoubleActive)
            //{
            //    if (SecondPlayerScript.Instance.SecondPlayerCell.Neighbours.Contains(this) && GridManager.Instance.turn > 0)
            //    {
            //        UIManager.Instance.Timer = 5;

            //        if (PowerUps.Instance.SecondSnoopyAnimator.gameObject.activeSelf)
            //        {
            //            PowerUps.Instance.SecondSnoopyAnimator.SetTrigger("Walk");
            //        }

            //        iTween.LookTo(SecondPlayerScript.Instance.gameObject, this.transform.position, 0.1f);
            //        iTween.MoveTo(SecondPlayerScript.Instance.gameObject, this.transform.position, 5);

            //        SecondPlayerScript.Instance.SecondPlayerCell = this;
            //       // rend.material.color = Color.red;
            //      //  transform.localScale = new Vector3(1f, 1f, 1f);
            //        foreach (CellProperties ncell in Neighbours)
            //        {
            //            ncell.rend.material.color = Color.blue;
            //        }

            //        PlayerChase.Instance.ChasePlayer();

            //    }
            //}
        }
    }


    void ResetNeighbours()
    {
        foreach (List<CellProperties> cells in GridManager.Instance.Cells)
        {
            foreach (CellProperties ncell in cells)
            {
                if (MainMenu.Instance.AIturn)
                {
                    if (ncell == PlayerManager.Instance.PlayerCell || ncell == AIManager.Instance.AICell)
                    {
                        continue;
                    }
                }

                if (MainMenu.Instance.AIturn == false)
                {
                    if (CharacterSelect.Instance.IsWolf == false)
                    {
                        if (ncell == PlayerManager.Instance.PlayerCell)
                        {
                            continue;
                        }
                    }
                    if(CharacterSelect.Instance.IsWolf)
                    {
                        if (ncell == AIManager.Instance.AICell)
                        {
                            continue;
                        }
                    }
                }

                if (PowerUps.Instance.DoubleActive)
                {
                    if (ncell == SecondPlayerScript.Instance.SecondPlayerCell)
                    {
                        continue;
                    }
                }

              //  ncell.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                if (MainMenu.Instance.AIturn == true)
                {
                    if (PowerUps.Instance.DoubleActive)
                    {
                        if (!PlayerManager.Instance.PlayerCell.Neighbours.Contains(ncell) && !AIManager.Instance.AICell.Neighbours.Contains(ncell) && !SecondPlayerScript.Instance.SecondPlayerCell.Neighbours.Contains(ncell))
                        {
                            ncell.rend.material.mainTexture = GridManager.Instance.OriginalTexture;
                        }
                    }
                    else
                    {
                        if (!PlayerManager.Instance.PlayerCell.Neighbours.Contains(ncell) && !AIManager.Instance.AICell.Neighbours.Contains(ncell))
                        {
                            ncell.rend.material.mainTexture = GridManager.Instance.OriginalTexture;
                        }
                    }

                }



                if (MainMenu.Instance.AIturn == false)
                {
                    if (CharacterSelect.Instance.IsWolf == false)
                    {
                        if (!PowerUps.Instance.DoubleActive)
                        {
                            if (!PlayerManager.Instance.PlayerCell.Neighbours.Contains(ncell) && !PlayerRun.Instance.RunCell.Neighbours.Contains(ncell))
                            {
                                ncell.rend.material.mainTexture = GridManager.Instance.OriginalTexture;
                            }
                        }

                        else
                        {
                            if (!PlayerManager.Instance.PlayerCell.Neighbours.Contains(ncell) && !PlayerRun.Instance.RunCell.Neighbours.Contains(ncell) && !SecondPlayerScript.Instance.SecondPlayerCell.Neighbours.Contains(ncell))
                            {
                                ncell.rend.material.mainTexture = GridManager.Instance.OriginalTexture;
                            }
                        }
                    }
                    if (CharacterSelect.Instance.IsWolf == true)
                    {
                       // if (!PowerUps.Instance.DoubleActive)
                       // {
                       //     if (!PlayerManager.Instance.PlayerCell.Neighbours.Contains(ncell) && !PlayerChase.Instance.ChaseCell.Neighbours.Contains(ncell))
                       //     {
                       //         ncell.rend.material.color = Color.black;
                        //    }
                      //  }

                        
                       
                            if (!AIManager.Instance.AICell.Neighbours.Contains(ncell) && !PlayerChase.Instance.ChaseCell.Neighbours.Contains(ncell) )
                            {
                                ncell.rend.material.mainTexture = GridManager.Instance.OriginalTexture;
                            }
                        
                    }
                }

            }
        }
    }


    void ResetRingOfFire()
    {



        if (MainMenu.Instance.AIturn == false)
        {
            if (CharacterSelect.Instance.IsWolf == false)
            {
                foreach (ExitCells ecell in ExitNeighbours)
                {
                    if (ecell.gameObject.activeInHierarchy)
                    {
                        if (PlayerManager.Instance.PlayerCell.ExitNeighbours.Contains(ecell))
                        {
                            ecell.gameObject.SetActive(false);
                            foreach(GameObject gb in ecell.FireSpot)
                            {
                                gb.SetActive(true);
                            }
                        }

                        if (PowerUps.Instance.DoubleActive)
                        {

                            if (SecondPlayerScript.Instance.SecondPlayerCell.ExitNeighbours.Contains(ecell))
                            {
                                ecell.gameObject.SetActive(false);
                                Debug.Log(ecell.FireSpot.Count);
                                foreach (GameObject gb in ecell.FireSpot)
                                {
                                    gb.SetActive(true);
                                }
                            }
                        }
                    }
                }

            }

            else if (CharacterSelect.Instance.IsWolf == true)
            {
                foreach (ExitCells ecell in ExitNeighbours)
                {
                    if (ecell.gameObject.activeInHierarchy)
                    {
                        if (PlayerChase.Instance.ChaseCell.ExitNeighbours.Contains(ecell))
                        {
                            ecell.gameObject.SetActive(false);
                            foreach (GameObject gb in ecell.FireSpot)
                            {
                                gb.SetActive(true);
                            }
                        }

                    }
                }
            }
        }

        else
        {
            foreach (ExitCells ecell in ExitNeighbours)
            {
                if (ecell.gameObject.activeInHierarchy)
                {
                    if (PlayerManager.Instance.PlayerCell.ExitNeighbours.Contains(ecell))
                    {
                        ecell.gameObject.SetActive(false);
                        foreach (GameObject gb in ecell.FireSpot)
                        {
                            gb.SetActive(true);
                        }
                    }

                    if (PowerUps.Instance.DoubleActive)
                    {

                        if (SecondPlayerScript.Instance.SecondPlayerCell.ExitNeighbours.Contains(ecell))
                        {
                            ecell.gameObject.SetActive(false);
                            foreach (GameObject gb in ecell.FireSpot)
                            {
                                gb.SetActive(true);
                            }
                        }
                    }
                }
            }
        }

    }



}

        
        
       
    

   



