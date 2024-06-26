using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    public LayerMask unwalkableMask; //지나갈 수 없는 지역을 나타내는 레이어마스크
    public Vector2 gridWorldSize;//격자의 전체 사이즈
    public float nodeRadius; //노드의 반지름
    float nodeDiameter; // 노드의 지름(격자 한칸의 변의 길이를 설정해줄 크기)
    int gridSizeX, gridSizeY; //격자의 밑변과 높이 크기
    public List<Node> path;
    Vector3 worldBottomLeft;


    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //(격자 월드 밑변 / 노드의 지름)으로 격자에 노드가 몇개나 들어갈 수 있는지 여부 계산.
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);//(격자 월드 높이 / 노드의 지름)으로 격자에 노드가 몇개나 들어갈 수 있는지 여부 계산.
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY]; //start에서 계산한 크기만큼 노드 2차원 배열 생성
        worldBottomLeft = transform.position - Vector3.right * (gridWorldSize.x / 2) - Vector3.up * (gridWorldSize.y / 2); //격자에서 왼쪽 아래 꼭지점
        // Debug.Log($"worldBottomLeft = {worldBottomLeft}");
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                grid[x,y] =new Node(walkable, worldPoint, x, y);
               // Debug.Log($"{grid[x, y].worldPosition.x},{grid[x, y].worldPosition.y}에 {walkable}한 노드 [{x},{y}]생성됨.");
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition) //현재 캐릭터가 서 있는 노드가 어딘지 반환.
    {

        float percentX = (worldPosition.x - worldBottomLeft.x) / gridWorldSize.x;
        float percentY = (worldPosition.y - worldBottomLeft.y) / gridWorldSize.y;

        //Debug.Log($"그리드 왼쪽 최하위 기준으로 약 {percentX*100}%,{percentY*100}%");
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        //Debug.Log($"이 캐릭터의 위치는 그리드로 표현된 노드 상으로는 {x},{y}");
        return grid[x, y];
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

    //    if (grid != null)
    //    {
    //        foreach (Node n in grid)
    //        {
    //            Gizmos.color = (n.walkable) ? Color.white : Color.red;
    //            if (path != null)
    //            {
    //                if (path.Contains(n))
    //                {
    //                    Gizmos.color = Color.black;
    //                }
    //            }
    //            else
    //            {
    //            }
    //            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));

    //        }
    //    }
    //}

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) //자기 자신 제외.
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }
}