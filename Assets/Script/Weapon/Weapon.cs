using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [Header("Number")]
    [SerializeField] public int weaponnumber; //0,1,2,3,4...

    [Header("Style")]
    [SerializeField] public int weaponstyle; //0,1,2,3,4

    [Header("WeaponInfo")]
     [SerializeField] string WeaponName;
     [SerializeField] int damage;
     [SerializeField] public int maxammo;
     [SerializeField] public int magazine;
     [SerializeField] public int magazineCapacity;
     [SerializeField] public float firecooltime;
     [SerializeField] public int reloadtime;
     [SerializeField] public GameObject dropPrefab;
     [SerializeField] GameObject firepoint;

    void Awake()
    {
        WeaponName = gameObject.name;
    }

    /*
     * 이 주석 아래 항목들을 WeaponManager C#파일로 옮겨야한다.
     * Weapon 항목에서 장전,장전 코루틴,발사,발사 코루틴은 가독성이 좋지 않다.
     */

    public IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(firecooltime);
        }
    }
    public IEnumerator ReloadCoroutine()
    {

        yield return new WaitForSeconds(reloadtime);
        Reload();
    }

    public void Fire()
    {
        if (magazine > 0)
        {

            Vector3 fireDir = transform.up;
            RaycastHit2D ray = Physics2D.Raycast(firepoint.transform.position, fireDir);
            if (ray.collider != null)
            {
                if (ray.collider.gameObject.GetComponent<Human>())
                {
                    Human human = ray.collider.gameObject.GetComponent<Human>();
                    ray.collider.gameObject.GetComponent<IDamagable>().Damage(human, damage);
                }
            }
            Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 1f);
            magazine--;
        }

    }

    public void Reload()
    {
        if(maxammo > magazineCapacity) 
        {
            maxammo -= magazineCapacity - magazine;
            magazine = magazineCapacity;
        }
        else if(maxammo <= magazineCapacity && (maxammo != 0))
        {
            magazine = maxammo;
            maxammo = 0;
        }
        else
        {
            Debug.Log("탄약이 다 떨어졌습니다. 상점에서 무기를 사거나, 다른 무기를 찾으십시오.");
        }

    }
}
