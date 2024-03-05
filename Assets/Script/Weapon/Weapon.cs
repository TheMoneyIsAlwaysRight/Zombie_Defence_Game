using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    [Header("Weapon_Info")]
    [SerializeField] int damage;
    [SerializeField] public int maxammo;
    [SerializeField] public int magazine;
    [SerializeField] public int magazineCapacity;
    [SerializeField] public float firecooltime;
    [SerializeField] int reloadtime;

    [SerializeField] public GameObject dropPrefab;
    [SerializeField] GameObject firepoint;
    [SerializeField] GameObject Player;

    public IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(this.firecooltime);
        }
    }
    public IEnumerator ReloadCoroutine()
    {
        Player.GetComponent<Animator>().SetBool("Reload", true);
        yield return new WaitForSeconds(this.reloadtime);
        Reload();
        Player.GetComponent<Animator>().SetBool("Reload", false);
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
            Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 0.2f);
            magazine--;
        }
        else
        {
        }
    }

    public void Reload()
    {
        if(magazine < magazineCapacity)
        {
            maxammo -= magazineCapacity - magazine;
            magazine = magazineCapacity;
        }
    }
}
