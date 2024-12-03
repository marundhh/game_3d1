using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;

    [SerializeField] private float weaponLength = 1f;  // Chiều dài của raycast
    [SerializeField] private float weaponDamage = 10f; // Lượng sát thương

    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }

    void Update()
    {
        
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false; // Reset trạng thái để có thể gây sát thương lại
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        // Hiển thị raycast trong editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }

   
    private void OnTriggerExit(Collider other)
    {
        // Khi đối tượng rời khỏi vùng va chạm, reset trạng thái
        if (other.CompareTag("Player"))
        {
            hasDealtDamage = false;
        }
    }
}

