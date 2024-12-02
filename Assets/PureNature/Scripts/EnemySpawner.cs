using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // Danh sách các loại kẻ địch
    public int startEnemiesPerWave = 5; // Số lượng kẻ địch ban đầu mỗi sóng
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn
    public float timeBetweenWaves = 5f; // Thời gian nghỉ giữa các sóng
    public int waveIncrement = 2; // Tăng số lượng kẻ địch mỗi sóng

    [Header("Spawn Area")]
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); // Kích thước khu vực spawn
    public Vector3 spawnAreaCenter = Vector3.zero; // Tâm của khu vực spawn

    private int currentWave = 0;
    private int currentEnemiesInWave;
    private int currentEnemyCount = 0;

    void Start()
    {
        // Bắt đầu sóng đầu tiên
        StartCoroutine(StartNextWave());
    }

    void SpawnEnemy()
    {
        // Kiểm tra nếu số lượng kẻ địch đạt tối đa trong sóng hiện tại
        if (currentEnemiesInWave >= startEnemiesPerWave + currentWave * waveIncrement)
            return;

        // Tính toán vị trí spawn ngẫu nhiên trong khu vực spawn
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
            spawnAreaCenter.y,
            Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2)
        );

        // Chọn một loại kẻ địch ngẫu nhiên
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Tạo kẻ địch
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        currentEnemiesInWave++;
        currentEnemyCount++;
    }

    IEnumerator StartNextWave()
    {
        while (true)
        {
            // Chờ cho đến khi toàn bộ kẻ địch trong sóng hiện tại bị tiêu diệt
            while (currentEnemyCount > 0)
            {
                yield return null;
            }

            // Chờ một khoảng thời gian trước khi bắt đầu sóng tiếp theo
            yield return new WaitForSeconds(timeBetweenWaves);

            // Tăng số sóng
            currentWave++;
            currentEnemiesInWave = 0;

            // Bắt đầu spawn sóng mới
            for (int i = 0; i < startEnemiesPerWave + currentWave * waveIncrement; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    public void OnEnemyDestroyed()
    {
        // Giảm số lượng kẻ địch khi một kẻ địch bị phá hủy
        currentEnemyCount--;
    }

    void OnDrawGizmosSelected()
    {
        // Vẽ khu vực spawn để dễ dàng chỉnh sửa trong editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}