using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class AI : Human
{
    [SerializeField] float movespeed;
    Vector2 moveDir;
    public static Vector2 mouse;
    float angle;

    [SerializeField] Transform target;

    float angleRange = 30f; // 각도범위
    float radius = 3f; // 부채꼴(시야)의 반지름 크기.

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    void AIeye() // Ai의 시야
    {
        Vector3 targetVector = (target.transform.position - gameObject.transform.position); // 나와 적과의 벡터

        if (targetVector.magnitude < radius) //나와 적과의 거리가 지름 크기 보다 작다면
        {
            float Dot = Vector3.Dot(targetVector.normalized, transform.forward); //두 백터 내적 결과를
            float theta = Mathf.Acos(Dot); //아크 코사인(역코사인)을 통해 세타(각) 구하기.
            float degree = Mathf.Rad2Deg * theta;

            if(degree >= angleRange)
            {
                Debug.Log("적이 나를 발견했습니다.");
            }
            else
            {
                Debug.Log("적이 나를 놓쳤습니다.");
            }

            /*
             * Mathf.Rad2Deg => 라디안 값 -> 각도
             * Mathf.Deg2Rad => 각도 -> 라디안 값
             * 
             * 
             */
        }
        else
        {
            Debug.Log("적이 나를 발견하지 못했습니다.");
        }

    }
    private void OnDrawGizmos()
    {
        Handles.color = _blue;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.up, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.up, -angleRange / 2, radius);
    }












    void Update()
    {
        this.Hpcheck();
        AIeye();
        gameObject.transform.Translate(moveDir * movespeed * Time.deltaTime,Space.World);
    }
    void AiMove() //Ai의 움직임
    {
        this.Hpcheck();
        gameObject.transform.Translate(moveDir * movespeed * Time.deltaTime, Space.World);

    }


    public void Fire(Weapon curweapon)
    {
        if (curweapon.magazine > 0)
        {

            Vector3 fireDir = transform.up;
            RaycastHit2D ray = Physics2D.Raycast(curweapon.firepoint.transform.position, fireDir);
            if (ray.collider != null)
            {
                if (ray.collider.gameObject.GetComponent<Human>())
                {
                    Human human = ray.collider.gameObject.GetComponent<Human>();
                    ray.collider.gameObject.GetComponent<IDamagable>().Damage(human, curweapon.damage);
                }
            }
            Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 1f);
            curweapon.magazine--;
        }

    }

    public void Reload(Weapon curweapon)
    {
        if (curweapon.maxammo > curweapon.magazineCapacity)
        {
            curweapon.maxammo -= curweapon.magazineCapacity - curweapon.magazine;
            curweapon.magazine = curweapon.magazineCapacity;
        }
        else if (curweapon.maxammo <= curweapon.magazineCapacity && (curweapon.maxammo != 0))
        {
            curweapon.magazine = curweapon.maxammo;
            curweapon.maxammo = 0;
        }
        else
        {
            Debug.Log("탄약이 다 떨어졌습니다. 상점에서 무기를 사거나, 다른 무기를 찾으십시오.");
        }

    }


}
