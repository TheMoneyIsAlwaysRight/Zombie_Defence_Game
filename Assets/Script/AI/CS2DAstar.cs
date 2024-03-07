//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using Unity.IO.LowLevel.Unsafe;
//using UnityEngine;
//using System;

//public class CS2DAstar : MonoBehaviour
//{

//    const int CostStraight = 10;
//    const int CostDiagonal = 14;




//    public static bool PathFinding(bool[,] tileMap,PosPoint start,PosPoint end)
//    {
//        int ySize = tileMap.GetLength(0);
//        int xSize = tileMap.GetLength(1);

//        CS2DNode[,] nodes = new CS2DNode[ySize, xSize];


//}

//public class CS2DNode
//{
//    public PosPoint pos;
//    public PosPoint ParentPos;
//    public float f;
//    public float g;
//    public float h;

//    public CS2DNode(PosPoint pos,PosPoint parent,float g,float h)
//    {
//        this.pos = pos;
//        this.ParentPos = parent;
//        this.f = g + h;
//        this.g = g;
//        this.h = h;
//    }
//}

//public struct PosPoint
//{
//    public float x;
//    public float y;
           
//    public PosPoint(float x,float y)
//    {
//        this.x = x;
//        this.y = y;
//    }
//}

