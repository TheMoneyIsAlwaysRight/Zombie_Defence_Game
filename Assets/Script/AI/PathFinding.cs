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
            Debug.Log("전에 있었던 경로를 삭제합니다.");
        }

        Node startNode = grid.NodeFromWorldPoint(startPos); //시작 노드 
        //Debug.Log($"적의 위치 노드:({startNode.gridX},{startNode.gridY})");

        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        //Debug.Log($"나의 위치 노드:({targetNode.gridX},{targetNode.gridY})");

        List<Node> openSet = new List<Node>(); //방문해야할 노드들
        List<Node> visited = new List<Node>(); //방문한 노드들

        openSet.Add(startNode); //방문해야할 노드리스트 중 맨처음에 시작 노드를 삽입.

        while (openSet.Count > 0)
        {
            Node node = openSet[0]; //방문해야할 노드 중 가장 맨 처음 노드꺼냄.

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                { //꺼낸 노드의 F값 < 오픈리스트의 모든 요소의 값 또는 꺼낸 노드의 f값 == 오픈리스트의 f값이 동일하면
                    if (openSet[i].hCost < node.hCost)
                    { 
                        node = openSet[i];
                    }
                }
            }
            openSet.Remove(node);
            visited.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return; //경로를 다 찾은 것으로 판정.

            }

            foreach (Node neighbour in grid.GetNeighbors(node)) //이웃 노드를 전부 확인하면서
            {
                //Debug.Log($"{node.gridX},{node.gridY}의 이웃 노드 {neighber.gridX},{neighber.gridY} 검사중");

                if (!neighbour.walkable || visited.Contains(neighbour)) //만약 이웃 노드가 이동할 수 없거나, 이미 방문한 노드라면 제외
                {
                    //Debug.Log($"{neighber.gridX},{neighber.gridY}의 노드는 제외");
                    continue;
                }
                //Debug.Log($"검사 중인 이웃노드{neighber}에서 G값은: {node.gCost}");

                int newCostToNeighbor = node.gCost + GetDistance(node,neighbour);
                //Debug.Log($"현재 노드의 G값 :{node.gCost}");
                // Debug.Log($"현재 검사중인 이웃노드{neighber.gridX},{neighber.gridY}와의 거리는 {GetDistance(node, neighber)}");
                
                if (newCostToNeighbor < neighbour.gCost || !openSet.Contains(neighbour))
                { //엉뚱한 길의 탐색을 막는다.. 이를테면 1시 방향에 적군이 있는데 7시 방향 노드를 탐색할 이유가 없다.

                    neighbour.gCost = newCostToNeighbor;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;
                    //Debug.Log($"현재 검사중인 이웃노드({neighbour.gridX},{neighbour.gridY})에서 목표지점까지 거리비용 : {neighbour.gCost} + {neighbour.hCost}");
                    //Debug.Log($"현재 검사중인 이웃노드의 부모노드는 {neighbour.parent.gridX},{neighbour.parent.gridY}");

                   if(!openSet.Contains(neighbour)) //책에서 이부분이 빠져있다.
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    int GetDistance(Node A, Node B) //지금 노드와 목표 노드간의 휴리스틱
    {
        int dstX = Mathf.Abs(A.gridX - B.gridX);
        int dstY = Mathf.Abs(A.gridY - B.gridY);
        if (dstX > dstY)
        {
            //Debug.Log($"적과 나의 거리 계산은 약 {14 * dstY + 10 * (dstX - dstY)}");
            return 14 * dstY + 10 * (dstX - dstY);
        }
        // Debug.Log($"적과 나의 거리 계산은 약 {14 * dstX + 10 * (dstY - dstX)}");
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
