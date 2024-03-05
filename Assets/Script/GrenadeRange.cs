using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeRange : MonoBehaviour
{
    public LayerMask playerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((1<<collision.gameObject.layer) & playerMask) != 0)
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(player!= null)
            {
                Debug.Log("Player hitted by grenade..");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerMask) != 0)
        {

        }
    }
}
