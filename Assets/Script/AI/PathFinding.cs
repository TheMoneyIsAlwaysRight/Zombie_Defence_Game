using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    Grid grid;
    public List<Node> npcpath;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        if(npcpath != null)
        {
            npcpath = null;
        }

        Node startNode = grid.NodeFromWorldPoint(startPos); //시작 노드 
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>(); //방문해야할 노드들
        List<Node> visited = new List<Node>(); //방문한 노드들
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            Node node = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                { 
                    if (openSet[i].hCost < node.hCost)
                    { 
                        node = openSet[i];
                    }
                }
            }
            openSet.Remove(node);
            visited.Add(node);

            if (node == targetNode) //경로를 다 찾았다면.
            {
                RetracePath(startNode, targetNode);
                return;

            }

            foreach (Node neighbour in grid.GetNeighbors(node))
            {
                if (!neighbour.walkable || visited.Contains(neighbour))
                {
              
                    continue;
                }

                int newCostToNeighbor = node.gCost + GetDistance(node,neighbour);

                if (newCostToNeighbor < neighbour.gCost || !openSet.Contains(neighbour))
                { 

                    neighbour.gCost = newCostToNeighbor;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;
                   if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    int GetDistance(Node A, Node B) 
    {
        int dstX = Mathf.Abs(A.gridX - B.gridX);
        int dstY = Mathf.Abs(A.gridY - B.gridY);
        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
        npcpath = path;
    }
}
