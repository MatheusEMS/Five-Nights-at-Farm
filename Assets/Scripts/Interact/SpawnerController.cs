using UnityEngine;

public class SpawnerController : MonoBehaviour
{

   [SerializeField] private enum EstadosSpawner
    {
        Habilitado,
        Desabilitado
    }
    //para debug
    [SerializeField] private bool check_desabilitado = false; 

    private EstadosSpawner estadoSpawner = EstadosSpawner.Habilitado;

    [SerializeField] private Transform spawnPointIngrediente;


    [SerializeField] private float timerHabilitar = 5;
    private float timer;

    [SerializeField] private GameObject erro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = timerHabilitar;
    }

    // Update is called once per frame
    void Update()
    {
        //fazer algo indicando que está desabilitado

        //contagem para voltar a ser habilitado
        if (estadoSpawner == EstadosSpawner.Desabilitado)
        {
            erro.SetActive(true);
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                erro.SetActive(false);
                //Debug.Log("Habilitou de novo");
                estadoSpawner = EstadosSpawner.Habilitado; 
            }
        }
    }

    public void Desabilitado()
    {
        check_desabilitado = true;
        //desabilita e seta o timer
        timer = timerHabilitar;
        estadoSpawner = EstadosSpawner.Desabilitado; 
    }
    
    public void SpawnaIngrediente(GameObject ingrediente)
    {
        if (estadoSpawner == EstadosSpawner.Habilitado)
        {
            if (GameObject.Find(ingrediente.name + "(Clone)"))
            {
                Debug.Log("Exists");
            }
            else
            {
                Debug.Log("Doesn't exist");

                Instantiate(ingrediente, new Vector3(spawnPointIngrediente.transform.position.x,
                spawnPointIngrediente.transform.position.y,
                spawnPointIngrediente.transform.position.z), Quaternion.Euler(-90,0,0));
            }
        } else
        {
            Debug.Log("Foi desabilitado por um animal");
        }
    }
}
