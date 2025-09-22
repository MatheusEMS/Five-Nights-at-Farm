using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    //[SerializeField] private List<float> tempoFase; //se tiver tempo para cada fase
    [SerializeField] private List<int> quantClientes; //qts de clientes que aparece em cada fase
    [SerializeField] private List<int> quantasFalhas; //qts de clientes insastafeito pode deixar para passar de fase
    public int clientesAtendidosSatisfeitos = 0;
    public int clientesAtendidosInsatisfeitos = 0;
    private int fase = 0;

    [SerializeField] private TextMeshProUGUI Resultadotext;
    [SerializeField] private GameObject TelaResultados;

    private enum StateGame
    {
        Intro,
        Prefase, //menu levels ou algo assim
        Jogando,
        Resultados,
        Pausado
    }
    private StateGame estadoJogo = StateGame.Jogando; //jogando no momento para teste

    public bool pausa = true; //pausa jogador,npcs e inimigo, controla qd move

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (estadoJogo)
        {
            case StateGame.Intro:
                pausa = true;


                break;
            case StateGame.Prefase:
                pausa = true;

                break;
            case StateGame.Jogando:
                pausa = false;

                if (clientesAtendidosSatisfeitos + clientesAtendidosInsatisfeitos == quantClientes[fase])
                {
                    //checar qts clientes ficaram insastifeitos
                    if (clientesAtendidosInsatisfeitos > quantasFalhas[fase]) //ve se deixa >= ou >
                    {
                        Debug.Log("Não passou de fase");

                        Resultadotext.text = "Não passou de fase";
                        estadoJogo = StateGame.Resultados;
                    }
                    else
                    {
                        Debug.Log("Passou de fase");

                        fase++;

                        Resultadotext.text = "Passou de fase";
                        estadoJogo = StateGame.Resultados;
                    }
                }
                break;
            case StateGame.Resultados:
                if (!GameObject.Find("Cliente(Clone)")) //espera o cliente ir embora
                {
                    pausa = true;
                    TelaResultados.SetActive(true);

                    //colocar botão para ir para a proxima fase
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        clientesAtendidosSatisfeitos = 0;
                        clientesAtendidosInsatisfeitos = 0;

                        TelaResultados.SetActive(false);

                        estadoJogo = StateGame.Jogando;
                    }
                }

                break;
            default:
                // Code to execute if no other case matches
                break;
        }
    }
    
        void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 700, 0, 400, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", "Clientes satisfeitos " + clientesAtendidosSatisfeitos,
        "Clientes Insatisfeitos " + clientesAtendidosInsatisfeitos,
        "fase: " + fase,
        "Estado: " + estadoJogo));
        GUILayout.EndArea();
    }
}
