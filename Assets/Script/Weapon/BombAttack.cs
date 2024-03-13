using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BombAttack : MonoBehaviour
{
    float PlantingTime = 5f;
    [SerializeField] GameObject PlantedBomb;
    bool IsPlanted;
    Coroutine bombplant;

   IEnumerator Planting()
   {
        Debug.Log("ÆøÅº ¼³Ä¡Áß...");
        yield return new WaitForSeconds(PlantingTime);
        IsPlanted = true;
   }

    private void Update()
    {
        if(IsPlanted && (bombplant != null))
        {
            StopCoroutine(bombplant);
            Instantiate(PlantedBomb, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else
        {
            bombplant = StartCoroutine(Planting());
        }
    }
}
