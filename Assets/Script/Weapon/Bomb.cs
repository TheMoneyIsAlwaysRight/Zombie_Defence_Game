using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject BombHud;
    [SerializeField] TMP_Text timer;
    bool IsDefused;
    private void OnEnable()
    {
        Debug.Log("Bomb has been Planted!!");
        BombHud.SetActive(true);
        timer = BombHud.GetComponent<TMP_Text>();
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(40f);
        if (IsDefused)
        {
            GameManager.gamemanager.Game.CounterWIN();
        }
        else if(!IsDefused)
        {
            GameManager.gamemanager.Game.TerrorWin();
        }
        
    }


}
