using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class item : MonoBehaviour
{
    [Header("Number")]
    [SerializeField] public int weaponnumber;
    [Header("Style")]
    [SerializeField] public int weaponstyle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().GetComponentInChildren<WeaponManager>().PickUpWeapon(gameObject);
        }
    }
}

