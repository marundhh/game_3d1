using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab của đồng xu
    public int coinCount = 5; // Số lượng đồng xu rớt ra
    public float spawnRadius = 1f; // Bán kính spawn đồng xu

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm là player
        if (other.CompareTag("Player"))
        {
            // Rớt ra coin
            SpawnCoins();

            // Hủy rương (biến mất)
            Destroy(gameObject);
        }
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinCount; i++)
        {
            // Tạo vị trí ngẫu nhiên xung quanh rương trong bán kính
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = transform.position.y; // Giữ nguyên độ cao

            // Tạo coin
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }
}
