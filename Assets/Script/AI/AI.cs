using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;
using System.IO;

public class AI : Human
{
    [SerializeField] public float movespeed;
    Vector2 moveDir;
}
