using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FIRETRACKING : MonoBehaviour
{
    Coroutine firetrackcoroutine;
    [SerializeField] WeaponManager weaponManager;


    private void OnEnable()
    {
        firetrackcoroutine = StartCoroutine(FiretrackerCoroutine()); 
    }
    private void OnDisable()
    {
        StopCoroutine(firetrackcoroutine);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Human>())
        {
            Human human = collision.gameObject.GetComponent<Human>();
            human.GetComponent<IDamagable>().Damage(human,weaponManager.curweapon.damage);
            Debug.Log("hit");
        }
    }
    public IEnumerator FiretrackerCoroutine()
    {
        yield return new WaitForSeconds(0.03f);
        gameObject.SetActive(false);
    }
}
