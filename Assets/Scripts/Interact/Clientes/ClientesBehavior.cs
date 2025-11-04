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

    [SerializeField] private GameObject popUpPrefab;

    private bool checkPopup = false;

    private GameObject popupObject;

    public float tempoMinEspera = 35f;
    public float tempoMaxEspera = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Tempo min: " + tempoMinEspera);
        Debug.Log("Tempo max: " + tempoMaxEspera);
        tempoEspera = Random.Range(tempoMinEspera, tempoMaxEspera); //tempo que ele vai esperar pela comida
        ReceitaPedida = Random.Range(1,3); //Qual receita ele vai pedir
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.pausa == false)
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

                    if (checkPopup == false)
                    {
                        popupObject = Instantiate(popUpPrefab, new Vector3(transform.localPosition.x, gameObject.transform.position.y + 2, transform.localPosition.z), new Quaternion());
                        //o que será mostrado em cada receita
                        switch(ReceitaPedida)
                        {
                            case 1:
                                popupObject.GetComponent<PopUps>().textValue = "2 Bananas\n1 Bola\n";
                                break;

                            case 2:
                                popupObject.GetComponent<PopUps>().textValue = "3 Bananas\n";
                                break;
                        }

                        
                        checkPopup = true;
                    }

                    popupObject.GetComponent<PopUps>().timer = tempoEspera;

                    if (tempoEspera <= 0)
                    {
                        estadoCliente = EstadosCliente.SaindoInsastifeito;
                        GameController.instance.clientesAtendidosInsatisfeitos++;

                        Destroy(GameObject.FindWithTag("segurando"));
                        IngredientsController.Instance.arma.SetActive(true);
                        GameObject.FindWithTag("hud").SetActive(true);

                        Debug.Log("Perdeu reputa��o");
                    }
                    break;
                case EstadosCliente.SaindoSatisfeito:
                    //colocar popup dele feliz

                    Destroy(popupObject);
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
                    //colocar popup dele bravo

                    Destroy(popupObject);
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
    }

    public void EntregandoReceita()
    {
        if (GameObject.FindWithTag("segurando") != null && GameObject.FindWithTag("segurando").layer == 7) //ve se � uma receita
        {
            Debug.Log(GameObject.FindWithTag("segurando"));

            for (var i = 0; i < 3; i++) //i < o numero de receitas disponiveis
            {
                Debug.Log("Entregando Receita" + i);
                if (GameObject.FindWithTag("segurando").name == "Receita" + i + "(Clone)")
                {
                    if (i == 0)
                    {
                        estadoCliente = EstadosCliente.SaindoInsastifeito;
                        GameController.instance.clientesAtendidosInsatisfeitos++;

                        Destroy(GameObject.FindWithTag("segurando"));
                        IngredientsController.Instance.arma.SetActive(true);

                        Debug.Log("Perdeu reputa��o 1");

                        return;
                    }
                    else
                    {
                        if (ReceitaPedida == i)
                        {
                            estadoCliente = EstadosCliente.SaindoSatisfeito;
                            GameController.instance.clientesAtendidosSatisfeitos++;

                            Destroy(GameObject.FindWithTag("segurando"));
                            IngredientsController.Instance.arma.SetActive(true);

                            Debug.Log("Ganhou reputa��o");
                            return;
                        }

                    }
                }
            }
            // Acho q não precisa dessas linhas, checar
            estadoCliente = EstadosCliente.SaindoInsastifeito;
            GameController.instance.clientesAtendidosInsatisfeitos++;
            Destroy(GameObject.FindWithTag("segurando"));
            Debug.Log("Perdeu reputa��o 2");
        }
    }

    //Debug na tela
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 500, 0, 400, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", estadoCliente,"receita pedida " + ReceitaPedida,"tempo espera " + tempoEspera));
        GUILayout.EndArea();
    }
}
