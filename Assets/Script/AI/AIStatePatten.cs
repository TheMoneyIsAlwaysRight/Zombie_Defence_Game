using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePatten : MonoBehaviour
{
    [SerializeField] Transform targetPlace;
    enum State //Ai의 상태 패턴
    {
        shopping,
        mission,
        trace,
        attack,
        die,
    }
    State curstate;

    private void Update()
    {
       
    }
    void Shopping()
    {
    }
    void attack()
    {

    }
    void Mission()
    {
        
    }
    void Trace()
    {

    }
    void Die()
    {

    }
}
