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
    [SerializeField] GameObject CTWIN;
    [SerializeField] GameObject TWIN;


    void Update()
    {
        CountSurvivor();
    }

    void CountSurvivor()
    {
        TCount = GameObject.FindGameObjectsWithTag("T");
        CTCount = GameObject.FindGameObjectsWithTag("CT");
        tcount = TCount.ToList<GameObject>();
        ctcount = CTCount.ToList<GameObject>();

        //Debug.Log($"Terrist is {tcount.Count}");
        //Debug.Log($"Counter Terrist is {ctcount.Count}");

        if(tcount.Count <= 0)
        {
            CTWIN.SetActive(true);

            GamePause();
        }
        else if (ctcount.Count <= 0)
        {
            TWIN.SetActive(true);

            GamePause();
        }
    }

   public static void GamePause()
   {
        Time.timeScale = 0f;
   }
   public static void GameResume()
   {
        Time.timeScale = 1f;
   }
}
