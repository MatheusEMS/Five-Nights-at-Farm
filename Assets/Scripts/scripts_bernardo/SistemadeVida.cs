using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SistemadeVida : MonoBehaviour
{
    //[SerializeField] public bool debuff_1 = false;
    //[SerializeField] public bool debuff_2 = false;

    [SerializeField] private bool player1;
    [SerializeField] private float health;



    public void Update()
    {
        if (player1 == false)
        {
            if (health <= 0)
            {

                Destroy(gameObject);

            }



        }

    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        
        StartCoroutine(debuff_o());
    }
    public void TakeDamage2(float damage)
    {
        health -= damage;
        Debug.Log(health);

        StartCoroutine(debuff_C());
    }
    public void TakeDamage3(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }


   

    IEnumerator debuff_C()
    {

        FPcontroller Player = GetComponent<FPcontroller>();
        Player.MaxSpeed = 3.5f;
        yield return new WaitForSeconds(4);
        Player.MaxSpeed = 5.5f;
    }
    IEnumerator debuff_o()
    {

        FPcontroller Player = GetComponent<FPcontroller>();
        Player.ataque_da_onca = true;
        
        yield return new WaitForSeconds(2);
        Player.ataque_da_onca = false;
    }
}
