using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ClientesBehavior : MonoBehaviour
{
    private enum EstadosCliente
    {
        Chegando,
        Esperando,
        SaindoSatisfeito,
        SaindoInsastifeito
    }
    private EstadosCliente estadoCliente = EstadosCliente.Chegando;

    public float speed;
    //[SerializeField] private Transform target;
    //[SerializeField] private Transform targetEnd;

    private float tempoEspera;

    private int ReceitaPedida;

    private float step;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tempoEspera = Random.Range(30f, 50f);
        ReceitaPedida = Random.Range(1,3);
    }

    // Update is called once per frame
    void Update()
    {
        switch (estadoCliente)
        {
            case EstadosCliente.Chegando:
                    if (transform.position == new Vector3(5.87f, 2f, 3.44f))
                    {
                        estadoCliente = EstadosCliente.Esperando;
                    }
                    else
                    {
                        step = speed * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(5.87f, 2f, 3.44f), step);
                    }
                break;
            case EstadosCliente.Esperando:
                tempoEspera -= Time.deltaTime;
                if(tempoEspera <= 0)
                {
                    estadoCliente = EstadosCliente.SaindoInsastifeito;
                    Debug.Log("Perdeu reputa��o");
                }
                break;
            case EstadosCliente.SaindoSatisfeito:
                if (transform.position == new Vector3(-5.09f, 2f, 12.47f))
                {
                    Destroy(gameObject);
                }
                else
                {
                    step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(-5.09f, 2f, 12.47f), step);
                }
                break;
            case EstadosCliente.SaindoInsastifeito:
                if (transform.position == new Vector3(-5.09f, 2f, 12.47f))
                {
                    Destroy(gameObject);
                }
                else
                {
                    step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(-5.09f, 2f, 12.47f), step);
                }
                break;
            default:
                // Code to execute if no other case matches
                break;
        }
    }

    public void EntregandoReceita()
    {
        if (GameObject.FindWithTag("segurando") != null && GameObject.FindWithTag("segurando").layer == 7) //ve se � uma receita
        {
            Debug.Log(GameObject.FindWithTag("segurando"));

            for (var i = 0; i < 3; i++) //i < o numero de receitas disponiveis
            {
                Debug.Log("Entregando Receita" + i);
                if (GameObject.FindWithTag("segurando").name == "Receita" + i+"(Clone)")
                {
                    if (i == 0)
                    {
                        estadoCliente = EstadosCliente.SaindoInsastifeito;
                        Destroy(GameObject.FindWithTag("segurando"));
                        //GameObject.FindWithTag("arma").SetActive(true); // m funcioa pois a arma está dividida

                        Debug.Log("Perdeu reputa��o");

                        return;
                    }
                    else
                    {
                        if (ReceitaPedida == i)
                        {
                            estadoCliente = EstadosCliente.SaindoSatisfeito;
                            Destroy(GameObject.FindWithTag("segurando"));
                            //GameObject.FindWithTag("arma").SetActive(true);     
                            Debug.Log("Ganhou reputa��o");
                            return;
                        }

                    }
                }
            }

            estadoCliente = EstadosCliente.SaindoInsastifeito;
            Destroy(GameObject.FindWithTag("segurando"));
            Debug.Log("Perdeu reputa��o");
        }
    }

    //Debug na tela
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 600, 0, 400, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", estadoCliente,"receita pedida " + ReceitaPedida,"tempo espera " + tempoEspera));
        GUILayout.EndArea();
    }
}
