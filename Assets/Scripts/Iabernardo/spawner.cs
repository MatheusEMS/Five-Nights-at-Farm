using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class spawner : MonoBehaviour
{
    public Transform spawnerlocation;
   [SerializeField] public GameObject Ia;
    private int xPos;
    private int zPos;
    private int xPosF;
    private int zPosF;
    [SerializeField] public int xPosV;
    [SerializeField] public int zPosV;
    public int Timer = 2;
    private bool spaw = true;
    [SerializeField] public int EnemyCount;

    private void Update()
    {
        // StartCoroutine(SpawnEnemies());
        if (spaw == true)
        {
            StartCoroutine(SpawInimigo());
        }
       
       

      
    }

    IEnumerator SpawInimigo()
    {
        spaw = false;

        yield return new WaitForSeconds(Timer);
        if (EnemyCount < 20)
        {
            xPos = Random.Range(-16, 35);
            xPosF = xPosV + xPos;
            zPos = Random.Range(-20, 20);
            zPosF = zPosV + zPos;

            EnemyCount++;

            Instantiate(Ia, new Vector3(xPosF, -4, zPosF), Quaternion.identity);


        }
        spaw = true;

    }

}

    // private IEnumerator SpawnEnemies()
    // {

    //  while (EnemyCount < 20)
    //  {

    //   xPos = Random.Range(-16, 13);
    //   zPos = Random.Range(-20, 1);

   // Instantiate(Ia, new Vector3(xPos, -4, zPos), Quaternion.identity);
         //   yield return new WaitForSeconds(1);
          //  EnemyCount++;
      //  }
  //  }

