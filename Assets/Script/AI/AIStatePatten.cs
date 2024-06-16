using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class AIStatePatten : MonoBehaviour
{
    [SerializeField] PathFinding pathfinder;
    [SerializeField] GameObject Enemy;
    float AimToEnemyTime = .1f;
    int nextNode;
    float alertTime = 1f;
    Coroutine AimandFire;
    [SerializeField] GameObject FindEnemyNodeListParent; //���� ���� ��� ����Ʈ
    Vector2 targetVector;

    List<Node> MissionNodeTrack;
    List<Transform> ReconPositionList;
    List<Node> ForReconPositionNodeTrack;
    [SerializeField]bool IsRecon;
    
    IEnumerator AimToEnemy()
    {
        AimToEnemyTime -= Time.deltaTime;
        yield return new WaitForSeconds(1f);
    }
    float angleRange = 360f; // ��������
    float distance = 15f; // ��ä��(�þ�)�� ������ ũ��.
    enum State //Ai�� ���� ����
    {
        shopping,
        mission,
        alert,
        battle,
        die,
        recon,
        defence
    }
    State curstate;
    bool IspathFind = false;

    private void Start()
    {
        //curstate = State.shopping;
        curstate = State.mission;
        nextNode = 0;

        ReconPositionList = FindEnemyNodeListParent.GetComponentsInChildren<Transform>().ToList<Transform>();
        curstate = State.mission;

        targetVector = (Enemy.transform.position - gameObject.transform.position);

    }
    private void Update()
    {
      AIWeaponPatten();
    }

    private void FixedUpdate()
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
            case State.defence:
                Defence();
                break;
            case State.die:
                Die();
                break;
        }
    }
    void ChangeState(State state) //���� �ٲٱ�
    {
        this.curstate = state;
    }

    void Shopping() //�������� ���⸦ ������.
    {
       //���� ���Ⱑ ��� �ͺ��� ����, ���� ������ �� �������� ��� ���� ����.
    }
    void Battle() //���� ����.
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
                    //Debug.Log("���� ������ ��...");
                    gameObject.GetComponent<AI>().Reload(gameObject.GetComponent<AI>().weaponmanager.curweapon);
                }
            }
            ChangeState(State.alert);

        }
    }
    void Mission()
    {
        gameObject.GetComponent<AI>().movespeed = 7f;

        if (!IspathFind)
        {
            int RandomNode = Mathf.RoundToInt(Random.Range(0, ReconPositionList.Count - 1));
            if (Vector2.Distance(transform.position, (Vector2)ReconPositionList[RandomNode].position) < 0.5f)
            {
                return;
            }
            MissionNodeTrack = pathfinder.FindPath(transform.position, (Vector2)ReconPositionList[RandomNode].position);
            nextNode = 0;
            IspathFind = true;
        }
        else
        {
            if (MissionNodeTrack == null)
            {
                IspathFind = false;
                return;
            }
            else if(MissionNodeTrack != null)
            {
                AIPath(MissionNodeTrack);
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
    }

    void AIPath(List<Node> nodeList)
    {
        if (nodeList != null && nodeList.Count > 0)
        {
            Node next = nodeList[0];

            Vector2 dir = (next.worldPosition - transform.position).normalized;
            transform.Translate(dir * Time.deltaTime * gameObject.GetComponent<AI>().movespeed, Space.World);
            if (Vector2.Distance(gameObject.transform.position, next.worldPosition) < 2f)
            {
                if (next == nodeList[nodeList.Count - 1]) //nodeList�� ��ǥ ���� ����
                {
                    ChangeState(State.alert);
                    nodeList.RemoveAt(0);
                    return;
                }
                nodeList.RemoveAt(0);
            }
        }

    }
    //void Recon()
    //{
    //    gameObject.GetComponent<AI>().movespeed = 7f;

    //    if (!IspathFind)
    //    {
    //        int RandomNode = Mathf.RoundToInt(Random.Range(0, ReconPositionList.Count - 1));
    //        if (Vector2.Distance(transform.position,(Vector2)ReconPositionList[RandomNode].position) < 0.5f)
    //        {
    //            return;
    //        }
    //        ForReconPositionNodeTrack = pathfinder.FindPath(transform.position, (Vector2)ReconPositionList[RandomNode].position);
    //        IspathFind = true;
    //    }
    //    else
    //    {
    //        AIPath(ForReconPositionNodeTrack);

    //        if (targetVector.sqrMagnitude < distance * distance)
    //        {
    //            float angle = Vector2.Angle(targetVector.normalized, transform.up);

    //            if (angle < angleRange)
    //            {
    //                ChangeState(State.battle);
    //                IspathFind = false;

    //            }
    //        }
    //    }

    //}
    void Alert() //��� ����.
    {
        gameObject.GetComponent<AI>().movespeed = 2f;

        Vector2 targetVector = (Enemy.transform.position - gameObject.transform.position);
        Coroutine alertcoroutine = StartCoroutine(AlertTime());
        if (targetVector.sqrMagnitude < distance * distance) //���� �ٽ� �þ߿� ������ ��
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
    void Defence() //��� ����.
    {       
        if (targetVector.sqrMagnitude < distance * distance)
        {
            float angle = Vector2.Angle(targetVector.normalized, transform.up);

            if (angle < angleRange)
            {
                ChangeState(State.battle);
            }
        }
    }
    void Die() //��� ����
    {
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
    List<Node> CalculatePath()
    {
        List<Node> list = pathfinder.FindPath(transform.position,targetVector);

        IspathFind = true;
        if(list == null)
        {
            Debug.Log("��� �ľ� �Ұ�");
        }
        return list;

    }

}
