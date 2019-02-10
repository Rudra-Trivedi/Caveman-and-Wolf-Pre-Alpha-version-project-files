using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour {
    public static GridManager Instance;
    public List<List<CellProperties>> Cells;
    public ExitCells[] ExitArray;

    Animator WindAnim;
    
    public GameObject caveman, wolf, Wind ;
    int flag, WindTimer;
    public int i;
    public int turn;
    int turndet;
    public Texture HighlightTexture, OriginalTexture;
    

    private void Update()
    {
        exitCall();
        
        CheckExit();
    }

    private void Awake()
    {
        Instance = this;
        
        ExitArray = new ExitCells[14];
        InitializeGrid();
        SetExitCells();
        
    }

    private void Start()
    {

        SetTurn();

        if (MainMenu.Instance.AIturn == true)
        {
            caveman = Instantiate(caveman) as GameObject;
            Animator Cavemananim = caveman.GetComponent<Animator>();
            PowerUps.Instance.SnoopyAnimator = Cavemananim;

            wolf = Instantiate(wolf) as GameObject;
            Animator Wolfanim = wolf.GetComponent<Animator>();
            PowerUps.Instance.WolfAnimator = Wolfanim;

            PlayerChase.Instance.gameObject.SetActive(false);
            PlayerRun.Instance.gameObject.SetActive(false);
        }
        if (MainMenu.Instance.AIturn == false)
        {

            if (CharacterSelect.Instance.IsWolf == true)
            {
                wolf = Instantiate(wolf) as GameObject;
                Animator Wolfanim = wolf.GetComponent<Animator>();
                PowerUps.Instance.WolfAnimator = Wolfanim;
                PlayerRun.Instance.gameObject.SetActive(false);

            }

            if (CharacterSelect.Instance.IsWolf == false)
            {
              
                caveman = Instantiate(caveman) as GameObject;
                Animator Cavemananim = caveman.GetComponent<Animator>();
                PowerUps.Instance.SnoopyAnimator = Cavemananim;
                PlayerChase.Instance.gameObject.SetActive(false);
            }
        }

        
        

        //   anim.Play("Idle");
        InvokeRepeating("BlowWind", 1, 1);
    }

   

    public void InitializeGrid () {

       

        Cells = new List<List<CellProperties>>();

        int i, j;

        for (i=0; i<4; i++)
        {

            Cells.Add(new List<CellProperties>());
            for(j=0; j<4; j++)
            {
                string a = "c" + i + j;
                Debug.Log(a);
                Cells[i].Add(GameObject.Find(a).GetComponent<CellProperties>());
            }
        }

        for(i=0;i<14;i++)
        {
            string s = "Exit" + i;
            Debug.Log(s);
            ExitArray[i] = GameObject.Find(s).GetComponent<ExitCells>();
        }
       

      
        
    }
    void SetExitCells()
    {
        foreach(ExitCells ecell in ExitArray)
        {
            ecell.gameObject.SetActive(false);
        }
    }


    void exitCall()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }



    void CheckExit()
    {
        flag = 0;
        foreach (ExitCells ecell in GridManager.Instance.ExitArray)
        {
            if(ecell.HasWolf || ecell.IsExtra)
            {
                continue;
            }


            if (ecell.gameObject.activeInHierarchy)
            {
                flag = 1;

            }

        }

    }

    void BlowWind()
    {

        if (flag == 0)
        {
            WindTimer++;
        }
        if (flag == 1)
        {
            WindTimer = 0;
        }



        if (WindTimer == 10)
        {
            WindTimer = 0;

            SetExitActive();

        }

        CellProperties.Instance.WindText.text = "Wind Time : " + WindTimer;
    }

    public void SetExitActive()
    {
    
        i = Random.Range(0, 14);
        Debug.Log(i);
        ExitCells newexit = ExitArray[i];
        if (i == 0 || i == 1 || i == 2 || i == 7 || i == 8 || i == 9 || i == 10)
        {
            Destroy(Instantiate(Wind, new Vector3(newexit.transform.position.x, newexit.transform.position.y, newexit.transform.position.z), Quaternion.Euler(newexit.transform.rotation.x, newexit.transform.rotation.y - 90, newexit.transform.rotation.z)), 2f);
        }
        else
        {
            Destroy(Instantiate(Wind, new Vector3(newexit.transform.position.x, newexit.transform.position.y, newexit.transform.position.z), newexit.transform.rotation), 2f);
        }
       
        newexit.gameObject.SetActive(true);
        foreach (GameObject gb in ExitArray[i].FireSpot)
        {
            gb.SetActive(false);
        }

    }

    public void ExtraCell()
    {

        int i;
        i = Random.Range(0, 14);
        foreach(GameObject gb in ExitArray[i].FireSpot)
        {
            gb.SetActive(false);
        }
        ExitArray[i].gameObject.SetActive(true);
        ExitArray[i].IsExtra = true;

    }

    void SetTurn()
    {

        turndet = Random.Range(0, 2);
        Debug.Log("Turn value =  " + turndet.ToString());

        if (MainMenu.Instance.AIturn == true)
        {
            Debug.Log("Multi local turn being called");
            if (turndet == 0)
            {
                turn = 1;

            }
            else if (turndet == 1)
            {
                turn = 0;

            }
        }
        else if (MainMenu.Instance.AIturn == false)
        {
            if (CharacterSelect.Instance.IsWolf == false)
            {
                turn = 1;
            }
            if (CharacterSelect.Instance.IsWolf == true)
            {
                turn = -1;
            }
        }
    }




}
