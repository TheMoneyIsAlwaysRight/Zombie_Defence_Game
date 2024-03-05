using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameObject[] TCount;
    static GameObject[] CTCount;

    static List<GameObject> tcount;
    static List<GameObject> ctcount;

    void Update()
    {
        CountSurvivor();
    }

    void CountSurvivor()
    {
        TCount = GameObject.FindGameObjectsWithTag("Terrist");
        CTCount = GameObject.FindGameObjectsWithTag("CounterTerrist");
        tcount = TCount.ToList<GameObject>();
        ctcount = CTCount.ToList<GameObject>();

        Debug.Log($"Terrist is {tcount.Count}");
        Debug.Log($"Counter Terrist is {ctcount.Count}");

        if(tcount.Count <= 0)
        {
            Debug.Log("Counter Terrrist Is Win");
        }
        else if (ctcount.Count <= 0)
        {
            Debug.Log("Terrrist Is Win");
        }
    }

    public void GamePause()
   {
        Time.timeScale = 0f;
   }
   public void GameResume()
   {
        Time.timeScale = 1f;
   }
}
