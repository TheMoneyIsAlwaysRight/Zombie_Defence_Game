using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIRETRACKING : MonoBehaviour
{
    Coroutine firetrackcoroutine;


    private void OnEnable()
    {
        firetrackcoroutine = StartCoroutine(FiretrackerCoroutine()); 
    }
    private void OnDisable()
    {
        StopCoroutine(firetrackcoroutine);
    }
    public IEnumerator FiretrackerCoroutine()
    {
        yield return new WaitForSeconds(0.03f);
        gameObject.SetActive(false);
    }
}
