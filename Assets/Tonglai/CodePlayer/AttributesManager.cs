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

            if (health <= 0)
            {
                EnemyDie();
            }
        }
    }

    public void EnemyDie() 
     {
        Debug.Log("ke thu die");
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
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}