using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BOMB : MonoBehaviour
{
    float PlantingTime = 5f;
    [SerializeField] GameObject PlantedBomb;
    [SerializeField] GameObject BombHUD;
    static bool IsPlanted;
    Coroutine bombplant;

   IEnumerator Planting()
   {
        Debug.Log($"ÆøÅº ¼³Ä¡Áß : {PlantingTime}");
        PlantingTime -= Time.deltaTime;
        yield return new WaitForSeconds(PlantingTime);
   }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!IsPlanted && PlantingTime <= 0)
            {
                StopCoroutine(bombplant);
                Instantiate(PlantedBomb, transform.position, transform.rotation);
                gameObject.SetActive(false);

                transform.parent.GetComponent<WeaponManager>().BombPlanted();

                BombHUD.SetActive(true);
                IsPlanted = true;
            }
            else
            {
                bombplant = StartCoroutine(Planting());
            }

        }
    }
    }
