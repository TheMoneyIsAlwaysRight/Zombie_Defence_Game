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
        //���� ���� ��ȯ�ϴ� � �Ŵ����� ����� ��������.
    }
    void OnDay()
    {
        //
    }




     


}
