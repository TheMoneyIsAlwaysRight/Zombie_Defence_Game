using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatePatten : MonoBehaviour
{
    [SerializeField] Transform missionPlace;
    [SerializeField] PathFinding pathfinder;
    enum State //Ai의 상태 패턴
    {
        shopping,
        mission,
        alert,
        attack,
        die,
    }
    State curstate;

    private void Update()
    {
       
    }
    void Shopping() //상점에서 무기를 구매함.
    {
       //가장 무기가 비싼 것부터 고르고, 남은 돈으로 그 다음으로 비싼 것을 고른다.
    }
    void battle() //교전 상태.
    {
        /* 0.즉시 걸음을 멈추고, 적군을 향해 조준하고 사격함.
         * 1.난이도에 따라 적을 향한 조준 속도를 다르게 설정.
         * 2.이 상태에서 적이 만약 시야에서 벗어나면 경계 상태로 전환.
         */

    }
    void Mission()
    {
        //임무 상태.이를 테면 폭탄 설치하러 가는 길
        /*0. 현재 위치에서 임무까지의 거리 경로 탐색.
        *1. 임무 상태 도중 적군과 조우하면 교전 상태로 전환  
        */
        pathfinder.FindPath(transform.position, missionPlace.position);
    }
    void alert() //경계 상태.
    {
        /* 적이 있었던 마지막 위치를 기억하고 일정 시간 동안 그 쪽을 주시함.
         * 이 시간동안 적이 다시 시야에 들어온다면 교전 상태로 전환
         * 일정 시간 동안 적이 시야에 잡히지 않는다면, 다시 임무 상태로 전환. 
         */

    }
    void Die() //사망 상태
    {
        //현재 들고 있던 무기와 수류탄, 폭탄을 떨어뜨리고, 비활성화함.
    }
}
