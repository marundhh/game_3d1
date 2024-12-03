using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public AttributesManager player_atm;
    public AttributesManager enemy_atm;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            player_atm.DealDamage(enemy_atm.gameObject);
        if(Input.GetKeyDown(KeyCode.P))
            enemy_atm.DealDamage(player_atm.gameObject);
        if (Input.GetKeyDown(KeyCode.I))
        {
            DamagePopUpGenerator.Current.CreatePopUp(Vector3.one,Random.Range(0, 1000).ToString(), Color.yellow );
        }
    }

    void Start()
    {
        
    }
    


    
}
