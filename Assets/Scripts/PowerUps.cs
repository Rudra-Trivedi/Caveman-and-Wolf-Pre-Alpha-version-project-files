using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PowerUps : MonoBehaviour {

    public static PowerUps Instance;

    public GameObject Player, SecondPlayer;

    [SerializeField]
    private GameObject player_g, secondPlayer_g;
    
    public GameObject Bola, Trap;
    public CellProperties BolaCell,TrapCell;
    public Animator SnoopyAnimator, SecondSnoopyAnimator, WolfAnimator;

    public bool DoubleActive;

    public int BolaFlag,TrapFlag, PackFlag;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
      //  SnoopyAnimator = Player.GetComponent<Animator>();
      //  SecondSnoopyAnimator = SecondPlayer.GetComponent<Animator>();

        BolaFlag = TrapFlag = 0;
        DoubleActive = false;
    }

  

    public void Double()
    {
        DoubleActive = true;
        GameObject g = Instantiate(SecondPlayer)as GameObject;
        SecondSnoopyAnimator = g.GetComponent<Animator>();
        SecondPlayerScript.Instance.SetTransform();
        ElixrController.Instance.Elixrvalue1 -=  1;
    }

    public void DoubleTurn()
    {

        GridManager.Instance.turn = 2;
        ElixrController.Instance.Elixrvalue1 -= 2;

    }

    public void BolaPower()
    {
        UIManager.Instance.Timer = 5;
        TrapFlag = 0;
        BolaFlag = 1;
        ElixrController.Instance.Elixrvalue1 -= 3;
        
    }

    public void WolfTrap()
    {
        UIManager.Instance.Timer = 5;
        BolaFlag = 0;
        TrapFlag = 1;
        ElixrController.Instance.Elixrvalue1 -= 4;
    }

    public void P1five()
    {
        ElixrController.Instance.Elixrvalue1 -= 5;
        SnoopyAnimator.SetTrigger("SpearThrow");
        
    }

    public void WolfPack()    // wolf pack
    {
        UIManager.Instance.Timer = 5;
        PackFlag = 1;
        ElixrController.Instance.Elixrvalue2 -= 1;
    }

    public void DoubleStep()    // Double Step
    {

        GridManager.Instance.turn = -1;
       
        ElixrController.Instance.Elixrvalue2 -= 2;

    }

    public void FlameOff()
    {
        ElixrController.Instance.Elixrvalue2 -= 3;
    }

    public void Stun()  // Stun
    {
        UIManager.Instance.Timer += 2;
        WolfAnimator.SetTrigger("SandAttack");
        GridManager.Instance.turn = -1;
        ElixrController.Instance.Elixrvalue2 -= 4;
    }

    public void ExtraExit() // extra exit
    {
        GridManager.Instance.ExtraCell();
        ElixrController.Instance.Elixrvalue2 -= 5;
    }

    public void BolaSpawn(Vector3 Spawn, CellProperties cell)
    {
        Instantiate(Bola,Spawn,Quaternion.identity);
        BolaCell = cell;
    }

    public void TrapSpawn(Vector3 Spawn, CellProperties cell)
    {
        Instantiate(Trap, Spawn, Quaternion.identity);
        TrapCell = cell;
    }

}
