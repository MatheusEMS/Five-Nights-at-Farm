using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CausandoDano : MonoBehaviour
{
    [SerializeField] private float damage;
    //private float atkspd;
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


        // new WaitForSeconds(2);
        StartCoroutine(TimetoAtk());
        colliderdmg.enabled = false;


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

       yield return new WaitForSeconds(3);
        colliderdmg.enabled = true;





    }

}
