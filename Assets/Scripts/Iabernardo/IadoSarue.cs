using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class IadoSarue : MonoBehaviour
{

   
    public GameObject[] pontos_de_comida;
    [SerializeField] public Transform gerador_1;
    [SerializeField] public Transform gerador_2;
    [SerializeField] public float maxTime = 1.0f;
    [SerializeField] public float maxDistance = 5.0f;
    private GameObject cobra_alvo;
    public Transform IaTransform;
    public Transform fugapoint;
    private bool fuga = false;
    NavMeshAgent agent;
    [SerializeField] private Collider colliderdmg;
    [SerializeField] private bool escolher = true;
    private int timer_2 = 0;
    [SerializeField] private int p_a = 0;
    [SerializeField] private int p_aV = 0;

    public float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        p_a = 0;
        timer_2 = 0;
        agent = GetComponent<NavMeshAgent>();
        fuga = false;
        Random.InitState(2);
        //pegando todos os pontos de comida 
        pontos_de_comida = GameObject.FindGameObjectsWithTag("Geradores");

        cobra_alvo = GameObject.FindGameObjectWithTag("Cobra");


        escolher = true;
    }

    // Update is called once per frame
    void Update()
    {

        cobra_alvo = GameObject.FindGameObjectWithTag("Cobra");
        timer_2++;
        //simplificar em 2 alvos de geradores de ingredientes
        if (p_a < 50)
        {

            p_aV = 0;

        }else if(p_a > 50)
         {

           p_aV = 1;


         }
        timer -= Time.deltaTime;
        ////escolhendo um numero aleatorio para escolher o gerador alvo
        if (escolher)
        {
            p_a = Random.Range(0, 100);
           
            
            if (timer_2 >= 150)
            {
                p_a = Random.Range(0, 100);
                escolher = false;
                timer_2 = 0 ;
            }

        }
        //se nã oestiver fugindo va até o gerador
        if (timer < 0.0f && fuga == false && cobra_alvo == null)
        {
            float sqDistance = (pontos_de_comida[p_aV].transform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance)
            {
                agent.destination = pontos_de_comida[p_aV].transform.position;
            }
            timer = maxTime;
        }

        //timer -= Time.deltaTime;

        //se ja sabototou um gerador fuja
        if (timer < 0.0f && fuga == true)
        {
            float sqDistance = (fugapoint.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance)
            {
                agent.destination = fugapoint.position;
                
            }
            
        }
        if (cobra_alvo && fuga == false)
        {

            float sqDistance = (cobra_alvo.transform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance)
            {
                agent.destination = cobra_alvo.transform.position;
            }
            timer = maxTime;

        }

    }
    public void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Geradores") || other.CompareTag("Cobra"))
        {


            fuga = true;
            if (other.CompareTag("Cobra"))
            {
                Destroy(other.transform.gameObject);

            }

        }
        else if (other.CompareTag("fugapoint"))
        {
           
            Destroy(this.transform.gameObject);
            // fuga = false;

        }

        



        SpawnerController geradordecomida =  other.GetComponent<SpawnerController>();
        geradordecomida.Desabilitado();
        


        
      


    }

}
