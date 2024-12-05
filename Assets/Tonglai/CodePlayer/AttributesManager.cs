using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;
    public float critDamage = 1.5f;
    public float critChance = 0.5f;
    public int armor;
    public GameObject coinPrefab;
    public AudioClip hitSound;
    private AudioSource audioSource;


    public void TakeDamage(int amount)
    {
        health -= amount;
        Vector3 rand = new Vector3(Random.Range(9, 0.25f), Random.Range(0, 0.25f), Random.Range(0, 0.25f));
        DamagePopUpGenerator.Current.CreatePopUp(
            transform.position,
            amount.ToString(),
            Color.yellow
            );
        if (gameObject.CompareTag("Enemy"))
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
        Debug.Log("ke thu die");
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

                totalDamage *= critDamage;
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
            Debug.LogWarning("Coin prefab chưa được gán trong Inspector!");
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

    // Update is called once per frame
    void Update()
    {

    }
}