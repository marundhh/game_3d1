using UnityEngine;
using UnityEngine.UI;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int maxHealth; // Giá trị tối đa của máu
    public Slider healthSlider; // Tham chiếu đến Slider hiển thị máu
    public int attack;
    public float critDamage = 1.5f;
    public float critChance = 0.5f;
    public int armor;
    public GameObject coinPrefab;
    public AudioClip hitSound;
    public GameObject gameOverCanvas; // Reference to Game Over Canvas
    private AudioSource audioSource;

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth); // Đảm bảo máu không giảm dưới 0 hoặc vượt quá maxHealth

        DamagePopUpGenerator.Current.CreatePopUp(
            transform.position,
            amount.ToString(),
            Color.yellow
        );

        if (gameObject.CompareTag("Player"))
        {
            // Cập nhật Slider máu
            if (healthSlider != null)
            {
                healthSlider.value = (float)health / maxHealth; // Giá trị từ 0 đến 1
            }
            else
            {
                Debug.LogWarning("Health Slider is not assigned!");
            }

            if (health <= 0)
            {
                // Hiển thị con trỏ chuột
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0; // Pause the game
                if (gameOverCanvas != null)
                {
                    gameOverCanvas.SetActive(true); // Show Game Over Canvas
                }
                else
                {
                    Debug.LogWarning("Game Over Canvas is not assigned!");
                }
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            Slider slider = gameObject.transform
                .GetChild(1).transform
                .GetChild(0).transform
                .GetComponent<Slider>();
            slider.value = health;
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            if (health <= 0)
            {
                EnemyDie();
            }
        }
    }

    public void EnemyDie()
    {
        Debug.Log("Enemy died");
        SpawnCoin();
        Destroy(gameObject);
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            float totalDamage = attack;
            if (Random.Range(0f, 1f) < critChance)
            {
                totalDamage *= critDamage;
            }
            atm.TakeDamage((int)totalDamage);
        }
    }

    private void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Coin prefab is not assigned!");
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Gán giá trị ban đầu cho thanh máu nếu tồn tại
        if (healthSlider != null)
        {
            healthSlider.maxValue = 1;
            healthSlider.value = (float)health / maxHealth;
        }
    }
}
