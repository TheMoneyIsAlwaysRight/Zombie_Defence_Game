using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    Stack<GameObject> Menu;



    private void OnEnable()
    {
        Menu = new Stack<GameObject>();
    }
}
