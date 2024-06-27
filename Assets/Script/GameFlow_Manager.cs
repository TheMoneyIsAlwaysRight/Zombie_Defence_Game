using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow_Manager : MonoBehaviour
{
    public enum Time
    {
        Day,
        Night

    }

    Time ownTime;

    void ChangeTime(Time time)
    {
        this.ownTime = time;
    }



    void OnNight()
    {
        //좀비 떼를 소환하는 어떤 매니저에 명령을 내려야함.
    }
    void OnDay()
    {
        //
    }




     


}
