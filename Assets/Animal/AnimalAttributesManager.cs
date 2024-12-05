using UnityEngine;

public class AnimalAttributesManager : MonoBehaviour
{
    public int health;
    public int attack;
    public GameObject meatPrefab;
    public AudioClip hitSound;
    private AudioSource audioSource;

    public void TakeDamage(int amount)
    {
        health -= amount;
        DamagePopUpGenerator.Current.CreatePopUp(
            transform.position,
            amount.ToString(),
            Color.yellow
        );

        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Thú đã chết");
        SpawnMeat();
        Destroy(gameObject);
    }

    private void SpawnMeat()
    {
        if (meatPrefab != null)
        {
            Instantiate(meatPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Meat prefab chưa được gán trong Inspector!");
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
}
