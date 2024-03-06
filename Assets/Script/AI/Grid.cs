using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadinus; //노드의 반지름
    Node[,] grid;

    float nodeDiameter; //노드의 지름
    int gridSizeX;
    int gridSizeY;


    private void Start()
    {
        nodeDiameter = nodeRadinus * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter); //그리드의 가로
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); //그리드의 세로
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        Vector3 worldPoint;
        for(int x=0;x<gridSizeX;x++)
        {
            for(int y=0;y<gridSizeY;y++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadinus) + Vector3.forward * (y * nodeDiameter + nodeRadinus);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadinus, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));
    //    if(grid!=null)
    //    {
    //        foreach(Node n in grid)
    //        {
    //            Gizmos.color = (n.isWalkAble) ? Color.clear : Color.red;
    //            Gizmos.DrawCube((n.worldPos, Vector3.one * (nodeDiameter - .1f))

    //        }
    //    }
    //}
}
