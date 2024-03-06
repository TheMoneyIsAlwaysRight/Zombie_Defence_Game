using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //[SerializeField] TMP_Text hp;
    //[SerializeField] TMP_Text ap;
    [SerializeField] TMP_Text maxammo;
    [SerializeField] TMP_Text magazine;
    [SerializeField] WeaponManager playerweaponManager;
    void Update()
    {       
        checkCurweapon();
    }

    void checkCurweapon()
    {
        if (playerweaponManager != null)
        {
            this.maxammo.text = (playerweaponManager.curweapon.maxammo).ToString();
            this.magazine.text = (playerweaponManager.curweapon.magazine).ToString();
        }
    }
}
