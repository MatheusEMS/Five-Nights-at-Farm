using System.Collections;
using UnityEngine;

public class ClientesController : MonoBehaviour
{
    [SerializeField] private Transform SpawnPointClientes;
    [SerializeField] private GameObject Clientes;


    private GameObject novoCliente;

    private bool checkSpawn = false;

    private ClientesBehavior clientesBehavior;

    public float tempoMinEspera = 50f;
    public float tempoMaxEspera = 65f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        novoCliente = Instantiate(Clientes, SpawnPointClientes.transform.position, Quaternion.identity);

        clientesBehavior = novoCliente.GetComponent<ClientesBehavior>();

        clientesBehavior.tempoMinEspera = tempoMinEspera - 3 * GameController.instance.fase;
        clientesBehavior.tempoMaxEspera = tempoMaxEspera - 4 * GameController.instance.fase;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.pausa == false)
        {
            if (GameObject.Find("Cliente(Clone)"))
            {
                checkSpawn = false;
                //Debug.Log("achou cliente");
            }
            else
            {
                if (checkSpawn == false)
                {
                    StartCoroutine(SpawnCliente());
                }
            }
        }
    }

    IEnumerator SpawnCliente()
    {
        checkSpawn = true;
        yield return new WaitForSeconds(0.5f);
        if (GameController.instance.pausa == false)
        {
            novoCliente = Instantiate(Clientes, SpawnPointClientes.transform.position, Quaternion.identity);

            clientesBehavior = novoCliente.GetComponent<ClientesBehavior>();

            clientesBehavior.tempoMinEspera = 35f - 3 * GameController.instance.fase;
            clientesBehavior.tempoMaxEspera = 50f - 4 * GameController.instance.fase;
                //Debug.Log("n�o tem cliente");   
        }     
    }
}
