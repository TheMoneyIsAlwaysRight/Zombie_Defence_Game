using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class AIStatePatten : MonoBehaviour
{
    [SerializeField] Transform missionPlace;
    [SerializeField] PathFinding pathfinder;
    [SerializeField] GameObject Enemy;
    List<Node> MissionTrack;
    int nextNode;
    float alertTime = 3f;
    

    float angleRange = 90f; // 각도범위
    float distance = 15f; // 부채꼴(시야)의 반지름 크기.
    enum State //Ai의 상태 패턴
    {
        shopping,
        mission,
        alert,
        battle,
        die,
    }
    State curstate;
    bool IspathFind = false;

    private void Start()
    {
        //curstate = State.shopping;
        curstate = State.mission;
        nextNode = 0;
    }
    private void Update()
    {
        switch (curstate)
        {
            case State.shopping:
                Shopping();
                break;
            case State.mission:

                Mission();
                break;
            case State.alert:

                Alert();
                break;
            case State.battle:
                Battle();
                break;
            case State.die:
                Die();
                break;
        }

    }
    void ChangeState(State state) //상태 바꾸기
    {
        this.curstate = state;
    }

    void Shopping() //상점에서 무기를 구매함.
    {
       //가장 무기가 비싼 것부터 고르고, 남은 돈으로 그 다음으로 비싼 것을 고른다.
    }
    void Battle() //교전 상태.
    {
        /* 0.즉시 걸음을 멈추고, 적군을 향해 조준하고 사격함.
         * 1.난이도에 따라 적을 향한 조준 속도를 다르게 설정.
         * 2.이 상태에서 적이 만약 시야에서 벗어나면 경계 상태로 전환.
         */
        gameObject.GetComponent<AI>().movespeed = 0;

        Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position);
        transform.up = (targetVector).normalized;

        //Al.fire();
        if(targetVector.sqrMagnitude>distance*distance)
        {
            ChangeState(State.alert);

        }
    }
    void Mission()
    {
        //임무 상태.이를 테면 폭탄 설치하러 가는 길
        /*0. 현재 위치에서 임무까지의 거리 경로 탐색.
        *1. 임무 상태 도중 적군과 조우하면 교전 상태로 전환
        *2. 목적지 도착 시 임무 수행 또는 그 자리에서 방어 모드 진행.
        */
        gameObject.GetComponent<AI>().movespeed = 8f;

        if (!IspathFind) //경로 탐색을 한번만 실행하게 함.
        {
            pathfinder.FindPath(transform.position, missionPlace.position);
            nextNode = 0;
            Debug.Log("경로 탐색 완료");
            IspathFind = true;
        }
        else
        {
            AIPath();

            Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position); //1
            if (targetVector.sqrMagnitude < distance * distance)
            {
                float angle = Vector2.Angle(targetVector.normalized, transform.up);

                if (angle < angleRange)
                {
                    ChangeState(State.battle);
                }
            }
        }
    }
    void Alert() //경계 상태.
    {
        /* 적이 있었던 마지막 위치를 기억하고 일정 시간 동안 그 쪽을 주시함.
         * 이 시간동안 적이 다시 시야에 들어온다면 교전 상태로 전환
         * 일정 시간 동안 적이 시야에 잡히지 않는다면, 다시 임무 상태로 전환. 
         */
        gameObject.GetComponent<AI>().movespeed = 2f;

        Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position);
        Coroutine alertcoroutine = StartCoroutine(AlertTime());
        if (targetVector.sqrMagnitude < distance * distance) //적이 다시 시야에 들어왔을 때
        {
            StopCoroutine(alertcoroutine);
            ChangeState(State.battle);
        }
        else if(this.alertTime <=0f)
        {
            StopCoroutine(alertcoroutine);
            ChangeState(State.mission);
            IspathFind = false;
            this.alertTime = 5f;
            nextNode = 0;
            Debug.Log("경로를 재탐색합니다.");

        }

    }
    void Die() //사망 상태
    {
        //현재 들고 있던 무기와 수류탄, 폭탄을 떨어뜨리고, 비활성화함.
        //dropped weapon();
    }


    private void OnDrawGizmos()
    {
        Handles.color = new Color(2f, 0f, 0f, 0.1f);

        Handles.DrawSolidArc(transform.position, transform.forward, transform.up, angleRange / 2, distance);

        Handles.DrawSolidArc(transform.position, transform.forward, transform.up, -angleRange / 2, distance);
    }
    IEnumerator AlertTime()
    {
        this.alertTime -= Time.deltaTime;
        //Debug.Log($"남은 시간{this.alertTime}");
        yield return new WaitForSeconds(0.1f);
    }

    void AIPath()
    {
        if (pathfinder != null)
        {
            if (pathfinder.npcpath.Count > 0)
            {
                List<Node> path = pathfinder.npcpath;
                Node next = path[nextNode];
                Vector2 dir = (next.worldPosition - transform.position).normalized;
                // gameObject.transform.up = dir;

                transform.Translate(dir * Time.deltaTime*gameObject.GetComponent<AI>().movespeed, Space.World);

                if(Vector2.Distance(gameObject.transform.position,next.worldPosition) < 0.1f)
                {

                    if (next == path[path.Count - 1])
                    {
                        IspathFind = false;
                        ChangeState(State.alert);
                        return;
                    }

                    nextNode++;
                }
            }
            
        }
    }

}
