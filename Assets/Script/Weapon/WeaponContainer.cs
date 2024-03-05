using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "weapons",menuName ="ScriptableObject/Weapons")]

public class WeaponContainer : ScriptableObject
{
    public WeaponInfo[] weaponInfos;



    [Serializable]
    public struct WeaponInfo
    {
        public Weapon prefab;
        public string name;
        public int price;
        public int damage;
        public int maxammo;
        public int bullet;
        public float firecooltime;
        public float reloadtime;
    }


}
