using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaScript : MonoBehaviour {

    private void OnMouseDown()
    {
        if(GridManager.Instance.turn > 0)
        {
            if (PlayerManager.Instance.PlayerCell.Neighbours.Contains(PowerUps.Instance.BolaCell))
            {
                iTween.LookTo(PlayerManager.Instance.gameObject, PowerUps.Instance.BolaCell.transform.position, 0.1f);
                iTween.MoveTo(PlayerManager.Instance.gameObject, PowerUps.Instance.BolaCell.transform.position, 5f);
                PlayerManager.Instance.PlayerCell = PowerUps.Instance.BolaCell;
                return;
            }
        }
        else if(GridManager.Instance.turn <=0)
        {
            if (AIManager.Instance.AICell.Neighbours.Contains(PowerUps.Instance.BolaCell))
            {
                iTween.LookTo(AIManager.Instance.gameObject, PowerUps.Instance.BolaCell.transform.position, 0.1f);
                iTween.MoveTo(AIManager.Instance.gameObject, PowerUps.Instance.BolaCell.transform.position, 5f);
                PowerUps.Instance.WolfAnimator.SetTrigger("Bola");
                AIManager.Instance.AICell = PowerUps.Instance.BolaCell;
                GridManager.Instance.turn += 2;
                return;
            }
        }
    }
}
