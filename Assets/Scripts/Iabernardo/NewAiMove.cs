using UnityEngine;
using UnityEngine.AI;
public class NewAiMove : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] public float maxTime = 1.0f;
    [SerializeField] public float maxDistance = 1.0f;
    public Transform IaTransform;
    NavMeshAgent agent;

    float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        Transform meuTransform = GameObject.FindWithTag("Player").transform;
        playerTransform = meuTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.pausa == true)
        {
            agent.destination = IaTransform.position;

        }


        if (GameController.instance.pausa == false)
        {
            transform.LookAt(playerTransform.transform, Vector3.up);
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                float sqDistance = (playerTransform.position - agent.destination).sqrMagnitude;
                if (sqDistance > maxDistance)
                {
                    agent.destination = playerTransform.position;
                }
                timer = maxTime;
            }

            //transform.LookAt(playerTransform, Vector3.up);

        }
    }
}
