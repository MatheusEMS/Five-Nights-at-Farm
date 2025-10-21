using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    //[SerializeField] private List<float> tempoFase; //se tiver tempo para cada fase
    [SerializeField] private List<int> quantClientes; //qts de clientes que aparece em cada fase
    [SerializeField] private List<int> quantasFalhas; //qts de clientes insastafeito pode deixar para passar de fase
    public int clientesAtendidosSatisfeitos = 0;
    public int clientesAtendidosInsatisfeitos = 0;
    private int fase = 0;

    [SerializeField] private float tempoTelaPreta = 2f;
    private float timer;

    [Header("------- Hud References -------")]
    [SerializeField] private TextMeshProUGUI Resultadotext;
    [SerializeField] private GameObject TelaResultados;
    [SerializeField] private TextMeshProUGUI diaTXT;
    [SerializeField] private Image TelaPretaPrefase;
    [SerializeField] private GameObject Hud;

    [SerializeField] private TextMeshProUGUI continuarTentarTXT;

    //pos inicial player
    private Vector3 InitialPos;

    private enum StateGame
    {
        Intro,
        Prefase, //menu levels ou algo assim
        Jogando,
        Resultados,
        Pausado
    }
    private StateGame estadoJogo = StateGame.Prefase; //jogando no momento para teste

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

        InitialPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        timer = tempoTelaPreta;
    }

    // Update is called once per frame
    void Update()
    {
        switch (estadoJogo)
        {
            case StateGame.Intro:
                pausa = true;
                Hud.SetActive(false);

                //intro

                break;
            case StateGame.Prefase:

                //tira cursor e trava o mouse
                Hud.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                pausa = true;
                diaTXT.text = "Dia " + (fase + 1);

                timer -= Time.deltaTime;

                if (timer < 0f)
                {
                    // colocar transição para reiniciar a cena
                    diaTXT.DOFade(0, 3);
                    TelaPretaPrefase.DOFade(0, 4);
                }

                if(diaTXT.alpha <= 0.1)
                {
                    estadoJogo = StateGame.Jogando;
                }
                break;
            case StateGame.Jogando:
                Hud.SetActive(true);
                pausa = false;

                if (clientesAtendidosSatisfeitos + clientesAtendidosInsatisfeitos == quantClientes[fase])
                {
                    //checar qts clientes ficaram insastifeitos
                    if (clientesAtendidosInsatisfeitos > quantasFalhas[fase]) //ve se deixa >= ou >
                    {
                        Debug.Log("Não passou de fase");

                        Resultadotext.text = "Não passou de fase";
                        continuarTentarTXT.text = "Tentar de Novo";
                        estadoJogo = StateGame.Resultados;
                    }
                    else
                    {
                        Debug.Log("Passou de fase");

                        fase++;

                        Resultadotext.text = "Passou de fase";
                        continuarTentarTXT.text = "Continuar";
                        estadoJogo = StateGame.Resultados;
                    }
                }
                break;
            case StateGame.Resultados:
                Hud.SetActive(false);
                if (!GameObject.Find("Cliente(Clone)")) //espera o cliente ir embora
                {
                    //Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    pausa = true;

                    if (diaTXT.alpha <= 0 && Input.GetKeyDown(KeyCode.E))
                    {
                        ClicouBotaoContinuarTentar();
                    }

                    if (diaTXT.alpha > 0)
                    {
                        TelaResultados.SetActive(false);
                        
                    }else
                    {
                        TelaResultados.SetActive(true);
                    }

                    //Debug.Log("ALPHA: " + diaTXT.alpha);

                    if (diaTXT.alpha >= 0.98)
                    {
                        Debug.Log("entrou aqui");
                        timer = tempoTelaPreta;
                        estadoJogo = StateGame.Prefase;
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


    //botoes resultados
    public void ClicouBotaoContinuarTentar()
    {
        Debug.Log("CLICOU BOTAO");

        StartCoroutine(ResetPlayerPositionWithDelay());
        

        clientesAtendidosSatisfeitos = 0;
        clientesAtendidosInsatisfeitos = 0;


        diaTXT.text = "Dia " + (fase + 1);
        diaTXT.DOFade(1, 3);
        TelaPretaPrefase.DOFade(1, 4);

    }

    IEnumerator ResetPlayerPositionWithDelay()
    {
        // Espera o tempo especificado
        yield return new WaitForSeconds(4f);

        GameObject.FindGameObjectWithTag("Player").transform.position = InitialPos;
    }
}
