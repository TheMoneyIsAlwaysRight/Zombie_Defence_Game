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
    void Update()
    {
        
        checkCurweapon();
    }

    void checkCurweapon()
    {
        this.maxammo.text = (WeaponManager.curweapon.maxammo).ToString();
        this.magazine.text = (WeaponManager.curweapon.magazine).ToString();
    }
}
