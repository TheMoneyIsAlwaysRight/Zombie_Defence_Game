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
    public int gridSizeX, gridSizeY; //격자의 밑변과 높이 크기
    public GameObject Endpoint;
    

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/ nodeDiameter); //(격자 월드 밑변 / 노드의 지름)으로 격자에 노드가 몇개나 들어갈 수 있는지 여부 계산.
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);//(격자 월드 높이 / 노드의 지름)으로 격자에 노드가 몇개나 들어갈 수 있는지 여부 계산.
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY]; //start에서 계산한 크기만큼 노드 2차원 배열 생성
        Vector3 worldBottomLeft = transform.position - Vector3.right * (gridWorldSize.x / 2) - Vector3.up*(gridWorldSize.y / 2); //격자에서 왼쪽 아래 꼭지점
        Debug.Log($"{worldBottomLeft} is WorldBottemLeft");
        for(int x=0;x<gridSizeX;x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                //격자의 정중앙 위치벡터 worldPoint라고 지정하고 여기에 노드를 심는 과정. 
                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask) == false)
                {
                    grid[x, y] = new Node(true, worldPoint);
                    // Debug.Log($"{worldPoint} New Node Create...");
                }
                else
                {
                    grid[x, y] = new Node(false, worldPoint);
                    Debug.Log($"{worldPoint} unwalkableMask Node Create...");
                }
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

        return grid[x,y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,gridWorldSize.y,0));
        // 와이어프레임 큐브그리기(큐브의 중앙 위치에 관한 벡터, 큐브의 사이즈)
        if(grid != null)
        {
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

}
