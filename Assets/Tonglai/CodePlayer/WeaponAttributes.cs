using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    // Start is called before the first frame update
    public AttributesManager atm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AttributesManager>().TakeDamage(atm.attack);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy Dam Player");
            other.GetComponent<AttributesManager>().TakeDamage(atm.attack);
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
