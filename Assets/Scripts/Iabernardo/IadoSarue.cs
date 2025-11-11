using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class IadoSarue : MonoBehaviour
{

   
    public List<GameObject> pontos_de_comida;
    [SerializeField] public GameObject gerador_1;
    [SerializeField] public GameObject gerador_2;
    [SerializeField] public float maxTime = 1.0f;
    [SerializeField] public float maxDistance = 5.0f;

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
      
        agent = GetComponent<NavMeshAgent>();
        fuga = false;
        Random.InitState(2);
        pontos_de_comida.Add(gerador_1);
        pontos_de_comida.Add(gerador_2);
        
        escolher = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(p_a < 50)
        {

            p_aV = 2;

        }else if(p_a > 50)
        {

            p_aV = 1;


        }
        timer -= Time.deltaTime;
        if (escolher)
        {
            p_a = Random.Range(0, 100);
           
            timer_2++;
            if (timer_2 >= 80)
            {
                
                escolher = false;

            }

        }
        if (timer < 0.0f && fuga == false)
        {
            float sqDistance = (pontos_de_comida[p_aV].transform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance)
            {
                agent.destination = pontos_de_comida[p_aV].transform.position;
            }
            timer = maxTime;
        }

        //timer -= Time.deltaTime;
        if (timer < 0.0f && fuga == true)
        {
            float sqDistance = (fugapoint.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance)
            {
                agent.destination = fugapoint.position;
                
            }
            
        }

    }
    public void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Interactable"))
        {


            fuga = true;


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
