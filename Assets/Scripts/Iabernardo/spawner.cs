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
    public List<GameObject> spawnlist;
    private int ia;
    
    // posicao inicial do spawn
    [SerializeField] private int xPos;
    [SerializeField] private int zPos;
    // coordenada aleatoria para spawnar o inimigo
    private int xPosF;
    private int zPosF;
    //posição do spawner no mundo
    [SerializeField] public int xPosV;
    [SerializeField] public int zPosV;
    //delimita variação de posição de spawn com relação a posição do proprio spawner
    [SerializeField] public int xModN = -24;
    [SerializeField] public int zModN = -24;
    [SerializeField] public int xModP = 24;
    [SerializeField] public int zModP = 24;
    public int timer = 2;
    [SerializeField] private bool spaw = true;
    [SerializeField] private bool surge = false;
    [SerializeField] public int EnemyCount;
    [SerializeField] private bool ocupado = true;
    //[SerializeField] private bool realocar = false;
    private bool checkState = false;
    [SerializeField] private int realocn = 0;



    private void Start()
    {
        //randomizar seed e inimigos dentro da list
        Random.InitState(100);
        spawnlist.Add(Ia_sarue);
        spawnlist.Add(Ia_cobra);
        spawnlist.Add(Ia_onca);
    }

   
    private void Update()
    {
        // StartCoroutine(SpawnEnemies());
        ///se ja spawnou um inimigo comece a spawnar outro

        checkState = GameController.instance.CheckEstado();

       
      // Debug.Log("estado jogo: "+ checkState);
            if ((spaw == true || surge == true) && checkState == true)
            {
                StartCoroutine(SpawInimigo());
           
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
       
        yield return new WaitForSeconds(timer);
        spaw = true;


  
        Debug.Log("spawnando");
        if (EnemyCount < 20)
        {
            xPos = Random.Range(xModN, xModP);
            xPosF = xPosV + xPos;
            zPos = Random.Range(zModN, zModP);
            zPosF = zPosV + zPos;
            ia = Random.Range(0, 4);

            Vector3 positionToCheck = new Vector3(xPosF, -4, zPosF);
            ocupado = Physics.CheckSphere(positionToCheck, 0.5f);



            if (ocupado == false) { 
            Debug.Log("Spawnou");

            Instantiate(spawnlist[ia], new Vector3(xPosF, -4, zPosF), Quaternion.identity);
            spaw = true;
            EnemyCount += 1;

            }else if (EnemyCount < 20){
                spaw = true;
                realocn++;

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

