using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{

    Vector2 CursorPos;
    public Vector2 mousePos;

    public RectTransform transform_cursor;
    public RectTransform transform_icon;
   
    private void Start()
    {
        UnityEngine.Cursor.visible = false;

        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Update_MousePosition();
    }
    private void Init_Cursor()
    {
        transform_cursor.pivot = Vector2.up;
        
        if(transform_cursor.GetComponent<Graphic>())
        {
            transform_cursor.GetComponent<Graphic>().raycastTarget = false;

        }
        if (transform_icon.GetComponent<Graphic>())
        {
            transform_icon.GetComponent<Graphic>().raycastTarget = false;
        }
    }


    void Update_MousePosition()
    {
        mousePos = Input.mousePosition;

        transform_cursor.position = mousePos;
        transform_icon.position = transform_cursor.position;

    }


}
