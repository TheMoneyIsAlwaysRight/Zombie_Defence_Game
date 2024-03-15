using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Human : MonoBehaviour, IDamagable
{
    enum BelongTeam
    {
        T,
        CT
    }

    [SerializeField] bool T;
    [SerializeField] string Team;
    [SerializeField] public int hp; //체력
    [SerializeField] public int ap; //방탄복 수치
    [SerializeField] protected int money;  //갖고 있는 돈 
    [SerializeField] FIRETRACKING firetrack; //총알의 궤적
    [SerializeField] Animator animator;
    [SerializeField] GameObject FireFlash;
    [SerializeField] GameObject FireFlash2;
    [SerializeField] public WeaponManager weaponmanager;
    public bool IsReloading { get; private set; }
    public bool IsFiring { get; private set; }

    Coroutine reloadcoroutine;
    float reloadcooltime;
    bool IsReload;
    /*
     *[0] : Main Weapon 
     *[1] : Sub Weapon
     *[2] : Knife
     *[3]~[4] : Grenades
     *[5] : C4 bomb
     **/
    private void Awake()
    {
        if(T)
        {
            gameObject.tag = "T";
            Team = "Terrorist";
        }
        else
        {
            gameObject.tag = "CT";
            Team = "Counter_Terrorist";
        }
    }

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

    public IEnumerator FireCoroutine(Weapon curweapon)
    {
        yield return new WaitForSeconds(weaponmanager.curweapon.firecooltime);
        IsFiring = false;
    }


    public void Fire(Weapon curweapon)
    {
        //if (IsFiring || curweapon == null)
        //{
        //    Debug.Log("발사 쿨타임 즉 반동");
        //    return;
        //}

        if (curweapon == transform.gameObject.GetComponentInChildren<WeaponManager>().HAND[2])
        {
            animator.SetBool("Knife", true);
            Vector3 fireDir = transform.up;
            firetrack.gameObject.SetActive(true);
            curweapon.magazine--;
            Debug.Log("칼질");
            animator.SetBool("Knife", false);
        }
        else if (curweapon.magazine > 0)
        { 
           
            float bulletfireRandom;
            animator.SetBool("Fire", true);
            Vector3 fireDir = transform.up;
            firetrack.gameObject.SetActive(true);
            bulletfireRandom = Random.value;
            if (bulletfireRandom >= 0.5)
            {
                FireFlash.SetActive(true);
            }
            else
            {
                FireFlash2.SetActive(true);
            }
            Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 1f);
            curweapon.magazine--;
            Debug.Log("사격");
            animator.SetBool("Fire", false);
            
        }

    }
    public void Reload(Weapon curWeapon)
    {
        if (IsReloading || curWeapon == null)
        {
            return; // 이미 재장전 중이거나 무기가 없는 경우 재장전을 시작하지 않음
        }

        IsReloading = true;

        Debug.Log("재장전 시작");
        animator.SetBool("Reload", true);
        StartCoroutine(ReloadCoroutine(curWeapon));
    }

    private IEnumerator ReloadCoroutine(Weapon curWeapon)
    {
        yield return new WaitForSeconds(curWeapon.reloadtime);

        int bulletsNeeded = curWeapon.magazineCapacity - curWeapon.magazine;

        if (curWeapon.maxammo >= bulletsNeeded)
        {
            curWeapon.maxammo -= bulletsNeeded;
            curWeapon.magazine += bulletsNeeded;
            Debug.Log("재장전 완료");
        }
        else
        {
            curWeapon.magazine += curWeapon.maxammo;
            curWeapon.maxammo = 0;
            Debug.Log("재장전 완료. 남은 탄약이 부족합니다.");
        }
        animator.SetBool("Reload", false);
        IsReloading = false;
    }
}
