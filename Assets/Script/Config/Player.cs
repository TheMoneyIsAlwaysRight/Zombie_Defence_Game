using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Human
{
    [SerializeField] float movespeed;
    Vector2 moveDir;
    public static Vector2 mouse;
    float angle;
    [SerializeField] Animator animator;

    void cursor()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 dir = ((Vector3)mouse - transform.position).normalized;
        //transform.up = dir;

        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        // itDebug.Log($"mousepoint ({Input.mousePosition}), angle : ({angle})");

        // transform.rotation = Quaternion.Euler(0, 0, angle -90);

        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Update()
    {
        this.Hpcheck();
        cursor();
        gameObject.transform.Translate(moveDir * movespeed * Time.deltaTime,Space.World);
    }

    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }



    public void Fire(Weapon curweapon)
    {
        if (curweapon.magazine > 0)
        {

            Vector3 fireDir = transform.up;
            RaycastHit2D ray = Physics2D.Raycast(curweapon.firepoint.transform.position, fireDir);
            //RaycastHit2D ray = Physics2D.Linecast(curweapon.firepoint.transform.position, fireDir);
            if (ray.collider != null)
            {
                animator.SetBool("Fire", true);
                if (ray.collider.gameObject.GetComponent<Human>())
                {
                    Human human = ray.collider.gameObject.GetComponent<Human>();
                    ray.collider.gameObject.GetComponent<IDamagable>().Damage(human,curweapon.damage);
                }

            }
            Debug.DrawRay(transform.position, fireDir * float.MaxValue, Color.red, 1f);
            curweapon.magazine--;
            animator.SetBool("Fire", false);
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
