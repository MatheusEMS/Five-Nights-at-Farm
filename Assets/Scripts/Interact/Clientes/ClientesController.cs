using UnityEngine;

public class ClientesController : MonoBehaviour
{
    [SerializeField] private Transform SpawnPointClientes;
    [SerializeField] private GameObject Clientes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(Clientes, SpawnPointClientes.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Cliente(Clone)"))
        { 
            Debug.Log("achou cliente");
        }else
        {
            Instantiate(Clientes, SpawnPointClientes.transform.position, Quaternion.identity);
            Debug.Log("não tem cliente");
        }
    }
}
