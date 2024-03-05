using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade: Weapon
{

    private void OnDrawGizmos() //항상 기즈모 생성
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

    //private void OnDrawGizmosSelected() //오브젝트를 선택 중일때 기즈모생성.
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, 4f);
    //}
}
