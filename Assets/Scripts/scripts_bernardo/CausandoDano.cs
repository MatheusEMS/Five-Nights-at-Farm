using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CausandoDano : MonoBehaviour
{
    [SerializeField] private float damage;
    //private float atkspd;
    [SerializeField] private float atktime;
    [SerializeField] private string especie;
   // private bool atk = true;
    [SerializeField] private Collider colliderdmg;
    

    
    private void OnTriggerEnter(Collider other)
    {




        if (other.CompareTag("Player") && especie == "cobra")
        {
           

            SistemadeVida Player = other.GetComponent<SistemadeVida>();
            Player.TakeDamage2(damage);
            StartCoroutine(TimetoAtk());
            colliderdmg.enabled = false;

        }
        else if (other.CompareTag("Player") && especie == "onca")
        {
            SistemadeVida Player = other.GetComponent<SistemadeVida>();
            Player.TakeDamage(damage);
            StartCoroutine(TimetoAtk());
            colliderdmg.enabled = false;


        }


        // new WaitForSeconds(2);
       // StartCoroutine(TimetoAtk());
       // colliderdmg.enabled = false;


    }

  //  private void Update()
 //   {
   //     if (colliderdmg.enabled == false)
    //    {
    //        new WaitForSeconds(3);
    //        atkspd++;

    //    }
    //    if (atkspd >= atktime)
     //   {

        //    atkspd = 0;
        //    colliderdmg.enabled = true;
      //  }

   // }
   IEnumerator TimetoAtk()
    {

       yield return new WaitForSeconds(atktime);
        colliderdmg.enabled = true;





    }

}
