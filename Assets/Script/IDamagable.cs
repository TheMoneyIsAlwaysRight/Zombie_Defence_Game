using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Human : MonoBehaviour,IDamagable
{
    [SerializeField] public int hp; //체력
    [SerializeField] protected int ap; //방탄복 수치
    [SerializeField] protected int money;  //갖고 있는 돈 
    [SerializeField] protected bool isAi;  //플레이어가 조종중인지 아닌지
    /*
     *[0] : Main Weapon 
     *[1] : Sub Weapon
     *[2] : Knife
     *[3]~[4] : Grenades
     *[5] : C4 bomb
     **/
    private void Update()
    {
        Hpcheck();
    }


    protected void Hpcheck()
    {
        if(hp<=0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

}
