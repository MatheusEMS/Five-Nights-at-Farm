using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static GameController;

public class spawner : MonoBehaviour
{
    //public Transform spawnerlocation;
    [SerializeField] public GameObject Ia_sarue;
    [SerializeField] public GameObject Ia_cobra;
    [SerializeField] public GameObject Ia_onca;
    public List<GameObject> spawnlist3;
    public List<GameObject> spawnlist2;
    public List<GameObject> spawnlist1;
    private int ia;
  

    // posicao inicial do spawn
    [SerializeField] private int xPos;
    [SerializeField] private int zPos;
    // coordenada aleatoria para spawnar o inimigo
    private int xPosF;
    private int zPosF;
    //posi��o do spawner no mundo
    [SerializeField] public int xPosV;
    [SerializeField] public int zPosV;
    //delimita varia��o de posi��o de spawn com rela��o a posi��o do proprio spawner
    [SerializeField] public int xModN = -24;
    [SerializeField] public int zModN = -24;
    [SerializeField] public int xModP = 24;
    [SerializeField] public int zModP = 24;
    public int timer = 8;
    [SerializeField] private bool spaw = true;
    [SerializeField] private bool surge = false;
    [SerializeField] public int EnemyCount;
    [SerializeField] private bool ocupado = true;
    //[SerializeField] private bool realocar = false;
    private bool checkState = false ;
    [SerializeField] private int realocn = 0;
    [SerializeField] private int faseatual = 0;


    private void Start()
    {
        //randomizar seed e inimigos dentro da list
        //Random.InitState(100);
        //level1
        spawnlist1.Add(Ia_sarue);
        //level2
        spawnlist2.Add(Ia_sarue);
        spawnlist2.Add(Ia_cobra);
        //level3+
        spawnlist3.Add(Ia_sarue);
        spawnlist3.Add(Ia_cobra);
        spawnlist3.Add(Ia_onca);
    }

   
    private void Update()
    {
        // StartCoroutine(SpawnEnemies());
        ///se ja spawnou um inimigo comece a spawnar outro

        checkState = GameController.instance.CheckEstado();

       
      // Debug.Log("estado jogo: "+ checkState);
            if ((spaw == true || surge == true) && checkState == true && GameController.instance.pausa == false)
            {
                StartCoroutine(SpawInimigo());
           
            }

        //   xPos = Random.Range(-12, 26);
        //  xPosF = xPosV + xPos;
        //  zPos = Random.Range(-10, 12);
        //  zPosF = zPosV + zPos;
        //  ia = Random.Range(0, spawnlist.Count);



         faseatual = GameController.instance.fase;
    }
    
    IEnumerator SpawInimigo()
    {

        spaw = false;

        yield return new WaitForSeconds(timer);
        spaw = true;


        if (faseatual == 0)
        {
            Debug.Log("spawnando");
            if (EnemyCount < 4)
            {
                xPos = Random.Range(xModN, xModP);
                xPosF = xPosV + xPos;
                zPos = Random.Range(zModN, zModP);
                zPosF = zPosV + zPos;
                //ia = Random.Range(0,2);

                Vector3 positionToCheck = new Vector3(xPosF, 1, zPosF);
                ocupado = Physics.CheckSphere(positionToCheck, 0.5f);



                if (ocupado == false)
                {
                    Debug.Log("Spawnou");

                    Instantiate(Ia_sarue, new Vector3(xPosF, 1, zPosF), Quaternion.identity);
                    spaw = true;
                    EnemyCount += 1;

                }
                else if (EnemyCount < 4)
                {
                    spaw = true;
                    realocn++;

                }



            }
        }

        if (faseatual == 1)
        {
            Debug.Log("spawnando");
            if (EnemyCount < 6)
            {
                xPos = Random.Range(xModN, xModP);
                xPosF = xPosV + xPos;
                zPos = Random.Range(zModN, zModP);
                zPosF = zPosV + zPos;
                ia = Random.Range(0,3);

                Vector3 positionToCheck = new Vector3(xPosF, 1, zPosF);
                ocupado = Physics.CheckSphere(positionToCheck, 0.5f);



                if (ocupado == false)
                {
                    Debug.Log("Spawnou");

                    Instantiate(spawnlist2[ia], new Vector3(xPosF, 1, zPosF), Quaternion.identity);
                    spaw = true;
                    EnemyCount += 1;

                }
                else if (EnemyCount < 6)
                {
                    spaw = true;
                    realocn++;

                }



            }
        }

        if (faseatual >= 2)
        {
            Debug.Log("spawnando");
            if (EnemyCount < 10)
            {
                xPos = Random.Range(xModN, xModP);
                xPosF = xPosV + xPos;
                zPos = Random.Range(zModN, zModP);
                zPosF = zPosV + zPos;
                ia = Random.Range(0, 4);

                Vector3 positionToCheck = new Vector3(xPosF, 1, zPosF);
                ocupado = Physics.CheckSphere(positionToCheck, 0.5f);



                if (ocupado == false)
                {
                    Debug.Log("Spawnou");

                    Instantiate(spawnlist3[ia], new Vector3(xPosF, 1, zPosF), Quaternion.identity);
                    spaw = true;
                    EnemyCount += 1;

                }
                else if (EnemyCount < 10)
                {
                    spaw = true;
                    realocn++;

                }



            }
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

