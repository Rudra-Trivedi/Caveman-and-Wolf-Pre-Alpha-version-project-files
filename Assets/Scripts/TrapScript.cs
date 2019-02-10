using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapScript : MonoBehaviour {
    private void OnMouseDown()
    {
        if (GridManager.Instance.turn > 0)
        {
            PlayerManager.Instance.transform.position = this.transform.position;
            PlayerManager.Instance.PlayerCell = PowerUps.Instance.TrapCell;
            return;
        }
        else if (GridManager.Instance.turn <= 0)
        {
            iTween.LookTo(AIManager.Instance.gameObject, PowerUps.Instance.TrapCell.transform.position, 0.1f);
            iTween.MoveTo(AIManager.Instance.gameObject, PowerUps.Instance.TrapCell.transform.position, 5f);
            PowerUps.Instance.WolfAnimator.SetTrigger("Trap");
            StartCoroutine(LoadMyScene());
           // SceneManager.LoadScene("GameOver");

            return;
        }
    }
     
    IEnumerator LoadMyScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }

}
