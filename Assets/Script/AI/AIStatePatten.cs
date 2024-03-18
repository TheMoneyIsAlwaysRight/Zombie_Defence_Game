using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    float AimToEnemyTime = 1.5f;
    int nextNode;
    float alertTime = 3f;
    Coroutine AimandFire;
    [SerializeField] GameObject FindEnemyNodeListParent; //적군 정찰 노드 리스트

    List<Transform> NodeList; 
    
    IEnumerator AimToEnemy()
    {
        AimToEnemyTime -= Time.deltaTime;
        yield return new WaitForSeconds(1f);
    }
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
        NodeList = FindEnemyNodeListParent.GetComponentsInChildren<Transform>().ToList<Transform>();

    }
    private void Update()
    {
        AIWeaponPatten();
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
        gameObject.GetComponent<AI>().movespeed = 0;

        Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position);
        transform.up = (targetVector).normalized;
        if (targetVector.sqrMagnitude<distance*distance)
        {
            AimandFire = StartCoroutine(AimToEnemy());
            if (AimToEnemyTime <=0)
            {
                StopCoroutine(AimandFire);
                gameObject.GetComponent<AI>().Fire();
                if (gameObject.GetComponent<AI>().weaponmanager.curweapon.magazine <= 0)
                {
                    //Debug.Log("적이 재장전 중...");
                    gameObject.GetComponent<AI>().Reload(gameObject.GetComponent<AI>().weaponmanager.curweapon);
                }
            }
            ChangeState(State.alert);

        }
    }
    void Mission()
    {
        gameObject.GetComponent<AI>().movespeed = 15f;

        if (!IspathFind)
        {
            Debug.Log("새 경로 찾기");
            int RandomNode = Mathf.RoundToInt(Random.Range(0, NodeList.Count-1));
            if(Vector2.Distance(transform.position, NodeList[RandomNode].position)< 0.1f){
                Debug.Log("동일 경로로 판정. 새 경로 탐색");
                return; }
            pathfinder.FindPath(transform.position,NodeList[RandomNode].position);
            nextNode = 0;
            IspathFind = true;
        }
        else
        {
            AIPath();
            Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position);

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

        }

    }
    void Die() //사망 상태
    {
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
        yield return new WaitForSeconds(0.1f);
    }
    void AIWeaponPatten()
    {
        if (gameObject.GetComponent<AI>().weaponmanager.HAND[0] == null)
        {
            gameObject.GetComponent<AI>().weaponmanager.BuyWeapon(gameObject.GetComponent<AI>().weaponmanager.WeaponInfo[30]);
            return;
        }
        if (gameObject.GetComponent<AI>().weaponmanager.curweapon != gameObject.GetComponent<AI>().weaponmanager.HAND[0])
        {
            gameObject.GetComponent<AI>().weaponmanager.ChangeWeapon(gameObject.GetComponent<AI>().weaponmanager.HAND[0]);
        }
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
                transform.Translate(dir * Time.deltaTime*gameObject.GetComponent<AI>().movespeed, Space.World);

                if(Vector2.Distance(gameObject.transform.position,next.worldPosition) <= 0.5f)
                {
                 
                    if (next == path[path.Count - 1])
                    {
                        Debug.Log("경로 도착 완료");
                        IspathFind = false;    
                        return;
                    }                 
                    nextNode++;
                }
            }
            else
            {
                Debug.Log($"{pathfinder}의 카운트가 이상합니다");
                return;
            }

        }
        else
        {
            Debug.Log($"{pathfinder}가 Null입니다");
            return;
        }
    }

}
