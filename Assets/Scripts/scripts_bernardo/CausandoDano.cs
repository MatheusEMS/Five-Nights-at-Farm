using System.Runtime.CompilerServices;
using UnityEngine;

public class CausandoDano : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float atkspd;
    [SerializeField] private float atktime;
   // private bool atk = true;
    [SerializeField] private Collider colliderdmg;
    

    
    private void OnTriggerEnter(Collider other)
    {




        if (other.CompareTag("Player"))
        {
           
            SistemadeVida Player = other.GetComponent<SistemadeVida>();
            Player.TakeDamage(damage);


        }
       

        new WaitForSeconds(2);
        colliderdmg.enabled = false;


    }

    private void Update()
    {
        if (colliderdmg.enabled == false)
        {

            atkspd++;

        }
        if (atkspd >= atktime)
        {

            atkspd = 0;
            colliderdmg.enabled = true;
        }

    }


}
