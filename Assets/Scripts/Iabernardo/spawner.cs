using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class spawner : MonoBehaviour
{
    //public Transform spawnerlocation;
    [SerializeField] public GameObject Ia_sarue;
    [SerializeField] public GameObject Ia_cobra;
    [SerializeField] public GameObject Ia_onca;
    public List<GameObject> spawnlist;
    private int ia;
    [SerializeField] private int xPos;
    [SerializeField] private int zPos;
    private int xPosF;
    private int zPosF;
    [SerializeField] public int xPosV;
    [SerializeField] public int zPosV;
    public int timer = 2;
    [SerializeField] private bool spaw = true;
    [SerializeField] private bool surge = false;
    [SerializeField] public int EnemyCount;


    private void Start()
    {
        Random.InitState(60);
        spawnlist.Add(Ia_sarue);
        spawnlist.Add(Ia_cobra);
        spawnlist.Add(Ia_onca);
    }


    private void Update()
    {
        // StartCoroutine(SpawnEnemies());
        if (spaw == true)
        {
            StartCoroutine(SpawInimigo());
            surge = false;
        }

     //   xPos = Random.Range(-12, 26);
      //  xPosF = xPosV + xPos;
      //  zPos = Random.Range(-10, 12);
      //  zPosF = zPosV + zPos;
      //  ia = Random.Range(0, spawnlist.Count);

        

    }

    IEnumerator SpawInimigo()
    {

        spaw = false;

        yield return new WaitForSeconds(4);
        if (EnemyCount < 20)
        {
            xPos = Random.Range(-12, 26);
            xPosF = xPosV + xPos;
            zPos = Random.Range(-10, 12);
            zPosF = zPosV + zPos;
            ia = Random.Range(0, spawnlist.Count);


            EnemyCount++;

            Instantiate(spawnlist[ia], new Vector3(xPosF, -4, zPosF), Quaternion.identity);

            surge = true;
            spaw = true;
        }
        

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

