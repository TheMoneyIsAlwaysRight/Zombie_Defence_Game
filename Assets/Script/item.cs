using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] GameObject HandWeapon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().GetComponentInChildren<Hand>().PickUpWeapon(HandWeapon);
            Destroy(gameObject);
        }
    }
}

