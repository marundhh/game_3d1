using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesManager : MonoBehaviour
{
    public int attack;
    public int health;
    public float critDamage = 1.5f; //ti le ra don chi mang
    public float critRate = 0.5f; //xac suat ra don chi mang
    public int armor; //giap, giup giam sat thuong

    /*public void TakeDamage(int amount)
    {
        
        health -= amount;
    }*/

    //ham dinh don tan cong, truyen vao chi so bi tan cong
    public void TakeDamage(int amount)
    {
        //giam mau nhung phu thuoc them vao giap
        health -= amount - (amount * armor / 100);

        health -= amount;
        Vector3 rand = new Vector3(Random.Range(0, 0.25f),
                                   Random.Range(0, 0.25f),
                                   Random.Range(0, 0.25f));

        if (gameObject.CompareTag("Enemy"))//neu la ke thu
        {
            Slider slider = gameObject.transform
            .GetChild(1).transform //lay toi canvas
                .GetChild(0).transform //lay toi HealBar
                    .GetComponent<Slider>(); //lay component slider
            slider.value = health; //cap nhat gia tri slide

            if (health <= 0)//khi mau bang 0
            {
                EnemyDie();
            }
        }

        if (gameObject.CompareTag("Player"))
        {
            // Sẽ code hiển thị thông số máu
            if (health <= 0)
            {
                Time.timeScale = 0;
                // PlayerDie(); // Sẽ viết sau
            }
        }

    }

    public void EnemyDie()
    {
        Debug.Log("ke thu die");
        //truy xuong con roi lay animator
        Animator ani = gameObject.transform.GetChild(0).GetComponent<Animator>();
        //chuyen sang animation chet
        ani.SetBool("isDead", true);
        Destroy(gameObject, 5f);//huy sau 5 giay
    }

    //gay damage cho target
    public void DealDamage(GameObject target)
    {
        //lay AttributesManager cua target truyen vao
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            //lay attack cua chinh minh truyen vao co target
            //atm.TakeDamage(attack);

            float totalDamage = attack;
            //tao ra so ngau nhien ma nho hon xac suat thi them crit
            if (Random.Range(0f, 1f) < critRate)
                totalDamage *= critDamage;
            atm.TakeDamage((int)totalDamage);
        }
    }
}
