using System.Collections.Generic;
using UnityEngine;

public class ClusteredCoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Tham chiếu tới Prefab Coin
    public int numberOfClusters = 5; // Số cụm Coin
    public int coinsPerCluster = 5; // Số Coin trong mỗi cụm
    public Vector2 mapSize = new Vector2(50, 50); // Kích thước toàn bản đồ
    public float clusterRadius = 5f; // Bán kính cụm Coin
    public float coinSpacing = 1.5f; // Khoảng cách tối thiểu giữa các Coin trong cụm

    private List<Vector3> usedPositions = new List<Vector3>(); // Vị trí đã dùng

    void Start()
    {
        SpawnClusters();
    }

    void SpawnClusters()
    {
        for (int i = 0; i < numberOfClusters; i++)
        {
            // Xác định vị trí trung tâm cụm
            Vector3 clusterCenter = new Vector3(
                Random.Range(-mapSize.x / 2, mapSize.x / 2),
                10f, // Bắt đầu từ trên cao để Raycast xuống
                Random.Range(-mapSize.y / 2, mapSize.y / 2)
            );

            // Sử dụng Raycast để đảm bảo cụm nằm trên mặt đất
            if (Physics.Raycast(clusterCenter, Vector3.down, out RaycastHit hit, 20f))
            {
                clusterCenter = hit.point;
                SpawnCoinsInCluster(clusterCenter);
            }
        }
    }

    void SpawnCoinsInCluster(Vector3 clusterCenter)
    {
        int spawnedCoins = 0;
        int maxAttempts = 100; // Giới hạn số lần thử tìm vị trí hợp lệ
        int attempts = 0;

        while (spawnedCoins < coinsPerCluster && attempts < maxAttempts)
        {
            // Tạo vị trí ngẫu nhiên trong bán kính cụm
            Vector3 randomPosition = clusterCenter + new Vector3(
                Random.Range(-clusterRadius, clusterRadius),
                0,
                Random.Range(-clusterRadius, clusterRadius)
            );

            // Kiểm tra nếu vị trí hợp lệ
            if (IsPositionValid(randomPosition))
            {
                // Tạo Coin tại vị trí hợp lệ
                Instantiate(coinPrefab, randomPosition, Quaternion.identity);
                usedPositions.Add(randomPosition);
                spawnedCoins++;
            }

            attempts++;
        }

        if (spawnedCoins < coinsPerCluster)
        {
            Debug.LogWarning("Không thể spawn đủ Coins trong cụm. Hãy tăng bán kính cụm hoặc giảm số lượng Coin.");
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        // Đảm bảo vị trí không quá gần các Coin khác
        foreach (Vector3 usedPosition in usedPositions)
        {
            if (Vector3.Distance(position, usedPosition) < coinSpacing)
            {
                return false;
            }
        }
        return true;
    }
}
