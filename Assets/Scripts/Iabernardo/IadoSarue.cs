using UnityEngine;
using UnityEngine.AI;

public class IadoSarue : MonoBehaviour
{

    public Transform comidaTransform;
    [SerializeField] public float maxTime = 1.0f;
    [SerializeField] public float maxDistance = 5.0f;
    public Transform IaTransform;
    public Transform fugapoint;
    [SerializeField] private bool fuga = false;
    NavMeshAgent agent;
    [SerializeField] private Collider colliderdmg;

    public float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f && fuga == false)
        {
            float sqDistance = (comidaTransform.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance)
            {
                agent.destination = comidaTransform.position;
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
    private void OnTriggerEnter(Collider other)
    {




        if (other.CompareTag("Comida"))
        {

         
            fuga = true;


        }
          else if(other.CompareTag("fugapoint"))
          {
            Destroy(gameObject);
           // fuga = false;

          }

      


    }
}
