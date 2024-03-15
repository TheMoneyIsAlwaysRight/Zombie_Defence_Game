using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Human
{
    [SerializeField] float movespeed;
    Vector2 moveDir;
    public static Vector2 mouse;
    float angle;

    void cursor()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 dir = ((Vector3)mouse - transform.position).normalized;
        //transform.up = dir;

        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        // itDebug.Log($"mousepoint ({Input.mousePosition}), angle : ({angle})");

        // transform.rotation = Quaternion.Euler(0, 0, angle -90);

        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Update()
    {
        this.Hpcheck();
        cursor();
        gameObject.transform.Translate(moveDir * movespeed * Time.deltaTime,Space.World);
    }

    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }
}
