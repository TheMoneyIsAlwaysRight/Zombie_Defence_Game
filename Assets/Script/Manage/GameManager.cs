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
    public static GameManager gamemanager;


    public GameManager Game { get { return gamemanager; } }

    public void Awake()
    {
        if(gamemanager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gamemanager = new GameManager();
        }
    }


    void Update()
    {
        CountSurvivor();
    }
    public void TerrorWin()
    {
        TWIN.SetActive(true);

        GamePause();
    }
    public void CounterWIN()
    {
        CTWIN.SetActive(true);

        GamePause();
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
            CounterWIN();
        }
        else if (ctcount.Count <= 0)
        {
            TerrorWin();
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
