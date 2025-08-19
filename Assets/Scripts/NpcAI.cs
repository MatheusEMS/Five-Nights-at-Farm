using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.AI;

    public enum NpcStates
    {
        ATACAR,
        PERSEGUIR,
        RECUAR
    }

public class NpcAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private NpcStates estadosNPC;
    [SerializeField] private float distPersegue, distAtaque, distRecua;
    [SerializeField] private Transform alvo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        estadosNPC = NpcStates.PERSEGUIR;
    }

    // Update is called once per frame
    void Update()
    {
        AnaliseEstados();
    }

    void AnaliseEstados()
    {
        var dist = Vector3.Distance(transform.position, alvo.position);

        if (dist >= distPersegue)
        {
            estadosNPC = NpcStates.PERSEGUIR;
        }

        if (dist < distRecua)
        {
            estadosNPC = NpcStates.RECUAR;
        }

        if (dist < distAtaque && dist > distRecua)
        {
            estadosNPC = NpcStates.ATACAR;
        }
        VerificaEstados();
    }

    void VerificaEstados()
    {
        if (estadosNPC == NpcStates.PERSEGUIR)
        {
            agent.isStopped = false;
            agent.SetDestination(alvo.position);

        }
        else if (estadosNPC == NpcStates.ATACAR)
        {
            agent.isStopped = true;
        }
        else if (estadosNPC == NpcStates.RECUAR)
        {
            agent.isStopped = false;
            FuncaoRecuar();
        }
    }

    void FuncaoRecuar()
    {
        Vector3 toPlayer = alvo.transform.position - transform.position;

        Vector3 targetPosition = toPlayer.normalized * -10;
        agent.destination = targetPosition;
        agent.isStopped = false;
    }
}
