using System.Collections.Generic;
using UnityEngine;

public class RandomCoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int numberOfCoins = 10;
    public Vector2 spawnAreaSize = new Vector2(10, 10);
    public float minimumDistance = 1.5f;

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        int spawnedCoins = 0;
        int maxAttempts = 100;
        int attempts = 0;

        while (spawnedCoins < numberOfCoins && attempts < maxAttempts)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                10f, // Bắt đầu từ trên cao để Raycast xuống
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );

            if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, 20f))
            {
                randomPosition = hit.point;

                if (IsPositionValid(randomPosition))
                {
                    Instantiate(coinPrefab, randomPosition, Quaternion.identity);
                    usedPositions.Add(randomPosition);
                    spawnedCoins++;
                }
            }

            attempts++;
        }

        if (spawnedCoins < numberOfCoins)
        {
            Debug.LogWarning("Không thể spawn đủ Coins, hãy tăng kích thước khu vực hoặc giảm khoảng cách tối thiểu.");
        }
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 usedPosition in usedPositions)
        {
            if (Vector3.Distance(position, usedPosition) < minimumDistance)
            {
                return false;
            }


        }

        return true;
    }
}
