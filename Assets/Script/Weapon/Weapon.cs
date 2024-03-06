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
     [SerializeField] public int damage;
     [SerializeField] public int maxammo;
     [SerializeField] public int magazine;
     [SerializeField] public int magazineCapacity;
     [SerializeField] public float firecooltime;
     [SerializeField] public int reloadtime;
     [SerializeField] public GameObject dropPrefab;
     [SerializeField] public GameObject firepoint;

    void Awake()
    {
        WeaponName = gameObject.name;
    }
  
}
