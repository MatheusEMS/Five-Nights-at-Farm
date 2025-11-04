using UnityEditor.PackageManager;
using UnityEngine;

public class ClientesController : MonoBehaviour
{
    [SerializeField] private Transform SpawnPointClientes;
    [SerializeField] private GameObject Clientes;


    private GameObject novoCliente;

    private ClientesBehavior clientesBehavior;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        novoCliente = Instantiate(Clientes, SpawnPointClientes.transform.position, Quaternion.identity);

        clientesBehavior = novoCliente.GetComponent<ClientesBehavior>();

        clientesBehavior.tempoMinEspera = 35f - 3 * GameController.instance.fase;
        clientesBehavior.tempoMaxEspera = 50f - 4 * GameController.instance.fase;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.pausa == false)
        {
            if (GameObject.Find("Cliente(Clone)"))
            {
                //Debug.Log("achou cliente");
            }
            else
            {
                novoCliente = Instantiate(Clientes, SpawnPointClientes.transform.position, Quaternion.identity);

                clientesBehavior = novoCliente.GetComponent<ClientesBehavior>();

                clientesBehavior.tempoMinEspera = 35f - 3 * GameController.instance.fase;
                clientesBehavior.tempoMaxEspera = 50f - 4 * GameController.instance.fase;
                //Debug.Log("n�o tem cliente");
            }
        }
    }
}
