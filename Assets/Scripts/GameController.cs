using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    //[SerializeField] private List<float> tempoFase; //se tiver tempo para cada fase
    [SerializeField] private List<int> quantClientes; //qts de clientes que aparece em cada fase
    [SerializeField] private List<int> quantasFalhas; //qts de clientes insastafeito pode deixar para passar de fase
    public int clientesAtendidosSatisfeitos = 0;
    public int clientesAtendidosInsatisfeitos = 0;

    public int fase = 0;

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

    private float timerTutorial = 1;

    private enum StateGame
    {
        Intro,
        Prefase, //menu levels ou algo assim
        Jogando,
        Resultados,
        Pausado,
        NoTutorial
    }
    private StateGame estadoJogo = StateGame.Jogando; //jogando no momento para teste

    public bool pausa = true; //pausa jogador,npcs e inimigo, controla qd move

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
                TelaResultados.SetActive(false);

                //intro

                break;
            case StateGame.Prefase:

                //tira cursor e trava o mouse
                Hud.SetActive(false);
                TelaResultados.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                pausa = true;
                diaTXT.text = "Day " + (fase + 1);

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

                if (!GameObject.Find("Cliente(Clone)")) //espera o cliente ir embora
                {
                    //Parar musica
                    AudioManager.instance.StopMusic();

                    Hud.SetActive(false);
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

                    /*if (diaTXT.alpha >= 0.98)
                    {
                        Debug.Log("entrou aqui");
                        timer = tempoTelaPreta;
                        estadoJogo = StateGame.Prefase;
                    }*/
                }

                break;
            case StateGame.NoTutorial:
                pausa = true;
                Hud.SetActive(false);


                if (Input.GetKeyDown(KeyCode.E) && timerTutorial < 0)
                {
                    HudController.instance.FecharTutorial();
                    estadoJogo = StateGame.Jogando;
                }

                timerTutorial -= Time.deltaTime;
                break;
            case StateGame.Pausado:
                //Se tiver
                pausa = true;

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

        HudController.instance.DisableInteractionText();

        Debug.Log("CLICOU BOTAO");



        clientesAtendidosSatisfeitos = 0;
        clientesAtendidosInsatisfeitos = 0;

        timer = tempoTelaPreta;

        StartCoroutine(ResetPlayerPositionWithDelay());
        if (fase == 5) // ultima fase
        {
            SceneController.instance.LoadScene("Final");
        }
        else
        {
            SceneController.instance.LoadScene("SampleScene");
        }


        //estadoJogo = StateGame.Intro;


        //diaTXT.text = "Dia " + (fase + 1);
        //diaTXT.DOFade(1, 3);
        //TelaPretaPrefase.DOFade(1, 4);

    }

    IEnumerator ResetPlayerPositionWithDelay()
    {
        // Espera o tempo especificado
        yield return new WaitForSeconds(2f);
        estadoJogo = StateGame.Prefase;
        diaTXT.alpha = 1;

        var tempColor = TelaPretaPrefase.color;
        tempColor.a = 1f;
        TelaPretaPrefase.color = tempColor;

        //GameObject.FindGameObjectWithTag("Player").transform.position = InitialPos;

    }

    public void EntrouTutorial()
    {
        if (estadoJogo == StateGame.Jogando)
        {
            timerTutorial = 1;
            estadoJogo = StateGame.NoTutorial;
        }
        else
        {
            Debug.Log("não está jogando");
        }
        
    }
}
