using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BombHUD: MonoBehaviour
{
    [SerializeField] GameObject TimerHud;
    TMP_Text count;
    float bombTimer = 25f;
    Coroutine a;

    float Timer;

    IEnumerator BombTimer()
    {
        bombTimer -= Time.deltaTime;
        yield return new WaitForSeconds(1);
        
    }

    private void OnEnable()
    {
        TimerHud.SetActive(true);
        count = TimerHud.GetComponent<TMP_Text>();
    }
    private void Update()
    {
        a = StartCoroutine(BombTimer());
        Debug.Log($"{bombTimer}");
        count.text = bombTimer.ToString();
        if (bombTimer <= 0)
        {
            StopCoroutine(a);
            GameManager.gameManager.TerrorWin();
        }
        
    }
}
