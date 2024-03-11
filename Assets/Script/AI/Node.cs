using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gCost;
    public int hCost;

    public Node(bool walkable_,Vector3 worldPosition_)
    {
        walkable = walkable_;
        worldPosition = worldPosition_;
    }
    public int fCost
    {
        get { return gCost + hCost; }
    }
}
