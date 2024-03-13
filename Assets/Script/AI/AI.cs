using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;
using System.IO;

public class AI : Human
{
    [SerializeField] public float movespeed;
    Vector2 moveDir;
    public static Vector2 mouse;
    [SerializeField] WeaponManager weaponmanager;
    [SerializeField] FIRETRACKING firetrack; //ÃÑ¾ËÀÇ ±ËÀû

    void Update()
    {
        Hpcheck();
    }
    public void Fire(Weapon curweapon)
    {
        if (curweapon.magazine > 0)
        {
            firetrack.gameObject.SetActive(true);

            Vector3 fireDir = transform.up;

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

    }
}
