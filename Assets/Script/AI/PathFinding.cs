using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class ASTAR : MonoBehaviour
{
    [SerializeField] Grid grid;


    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        //while(openSet.Count>0)
        //{
        //    Node node = openSet[0];
        //    for(int i=1;i<openSet.Count;i++)
        //    {
        //        if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
        //        {
        //            if (openSet[i].hCost < node.hCost)
        //            {
        //                node = openSet[i];
        //            }
        //        }
        //    }
        //}
    }
}
