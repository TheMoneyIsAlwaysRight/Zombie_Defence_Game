using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    [SerializeField] GameObject[] inventory = new GameObject[5];
    [SerializeField] string[] inventroyInfo = new string[5];
    [SerializeField] GameObject droppoint;

    public GameObject curweapon, prevweapon;

    Coroutine firecoroutine;
    Coroutine reloadcoroutine;


    private void Update()
    {
        for(int x=0; x<inventory.Length; x++)
        {
            if (inventory[x] != null)
            {
                inventroyInfo[x] = inventory[x].name;
            }
        }
    }


    void OnFire(InputValue value)
    {

        if (value.isPressed)
        {
            firecoroutine = StartCoroutine(curweapon.GetComponent<Weapon>().FireCoroutine());
        }
        else
        {
            StopCoroutine(firecoroutine);
        }

    }
    public void PickUpWeapon(GameObject pickupweapon)
    {
        if(pickupweapon.GetComponent<Rifle>() && (inventory[0] == null))
        {
            inventory[0] = pickupweapon;
        }
        if (pickupweapon.GetComponent<Pistol>() && (inventory[1] == null))
        {
            inventory[1] = pickupweapon;
        }
        if (pickupweapon.GetComponent<Grenade>() && (inventory[3] == null))
        {
            inventory[3] = pickupweapon;
        }
        if (pickupweapon.GetComponent<Bomb>() && (inventory[4] == null))
        {
            inventory[4] = pickupweapon;
        }
    }

    void OnDropWeapon(InputValue value)
    {
        DropWeapon();
    }
    void DropWeapon()
    {
        if (curweapon != inventory[2])
        {
            curweapon.gameObject.SetActive(false);

            GameObject dropitem = curweapon.GetComponent<Weapon>().dropPrefab;
            Instantiate(dropitem, droppoint.transform.position, transform.rotation);
            for(int x=0;x<inventory.Length;x++)
            {
                if (inventory[x] == curweapon)
                {
                    inventory[x] = null;
                    break;
                }
            }
            prevweapon = inventory[2];
            curweapon = inventory[2];
            curweapon.SetActive(true);
        }
        else
        {
            Debug.Log("이 무기는 버릴 수 없다.");
        }

    }
    private void Start()
    {

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        curweapon = inventory[2];

        curweapon.gameObject.SetActive(true);
        
    }

    void OnReload()
    {
        reloadcoroutine = StartCoroutine(curweapon.GetComponent<Weapon>().ReloadCoroutine());
    }
    void OnRifle(InputValue button)
    {

        if (inventory[0] != null)
        {
            prevweapon = curweapon;

            curweapon = inventory[0];

            if (prevweapon != curweapon)
            {
                prevweapon.gameObject.SetActive(false);
                curweapon.gameObject.SetActive(true);
            }

        }
        else
        {
            Debug.Log("The Rifle Weapon didn`t exist.");
        }
    }
    void OnPistol(InputValue button)
    {
        if (inventory[1] != null)
        {
            prevweapon = curweapon;
            curweapon = inventory[1];

            if (prevweapon != curweapon)
            {
                prevweapon.gameObject.SetActive(false);
                curweapon.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("The Pistol Weapon didn`t exist.");
        }

    }
    void OnMelee(InputValue button)
    {

        if (inventory[2]  != null)
        {
            prevweapon = curweapon;
            curweapon = inventory[2];

            if (prevweapon != curweapon || (prevweapon == inventory[2] && curweapon == inventory[2]))
            {
                prevweapon.gameObject.SetActive(false);
                curweapon.gameObject.SetActive(true);
            }
        }
    }
    void OnGrenade(InputValue button)
    {
        if (inventory[3] != null)
        {
            prevweapon = curweapon;
            curweapon = inventory[3];

            if (prevweapon != curweapon)
            {
                prevweapon.gameObject.SetActive(false);
                curweapon.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("The Grenade Weapon didn`t exist.");
        }

    }
    void OnBomb(InputValue button)
    {
        if (inventory[4] != null)
        {
            prevweapon = curweapon;
            curweapon = inventory[4];

            if (prevweapon != curweapon)
            {
                prevweapon.gameObject.SetActive(false);
                curweapon.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("The Bomb Weapon didn`t exist.");
        }

    }
}
