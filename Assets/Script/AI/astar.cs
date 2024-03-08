//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Grid : MonoBehaviour
//{
//    public LayerMask unwalkableMask;
//    public Vector2 gridWorldSize;
//    public float nodeRadius;
//    Node[,] grid;
//    float nodeDiameter;
//    int gridSizeX, gridSizeY;


//    private void Update()
//    {
//        nodeDiameter = nodeRadius * 2; // 노드의 지름 설정
//        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); // X축 그리드 크기 설정
//        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); // Y축 그리드 크기 설정
//        CreateGrid(); // 그리드 생성 함수 호출
//    }


//    void CreateGrid()
//    {
//        grid = new Node[gridSizeX, gridSizeY]; // 그리드 배열 초기화
//        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2; // 그리드의 왼쪽 아래 모서리 좌표 계산

//        for (int x = 0; x < gridSizeX; x++) // X축 반복
//        {
//            for (int y = 0; y < gridSizeY; y++) // Y축 반복
//            {
//                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius); // 현재 노드의 월드 좌표 계산

//                // 현재 노드가 걷기 가능한지 여부를 체크하고 그리드에 노드 추가
//                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius)); // 노드의 반경 내에 장애물이 있는지 확인
//                grid[x, y] = new Node(walkable, worldPoint); // 노드 생성 및 그리드에 추가
//            }
//        }
//    }


//    private void OnDrawGizmos()
//    {

//        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0)); // 그리드 영역을 와이어 프레임으로 그림

//        if (grid != null) // 그리드가 생성되었는지 확인
//        {
//            foreach (Node n in grid) // 그리드의 모든 노드에 대해 반복
//            {
//                Gizmos.color = (n.walkable) ? Color.white : Color.red; // 걷기 가능한 노드는 흰색, 그렇지 않으면 빨간색으로 설정
//                Gizmos.DrawWireCube(new Vector3(n.worldPosition.x, n.worldPosition.y, 0), Vector3.one * (nodeDiameter - .1f)); // 노드를 큐브로 그림
//            }
//        }
//        //{
//        //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,gridWorldSize.y,0)); // 그리드 영역을 와이어 프레임으로 그림

//        //    if (grid != null) // 그리드가 생성되었는지 확인
//        //    {
//        //        foreach (Node n in grid) // 그리드의 모든 노드에 대해 반복
//        //        {
//        //            Gizmos.color = (n.walkable) ? Color.white : Color.red; // 걷기 가능한 노드는 흰색, 그렇지 않으면 빨간색으로 설정
//        //            Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f)); // 노드를 큐브로 그림
//        //        }
//        //    }

//    }
//}
