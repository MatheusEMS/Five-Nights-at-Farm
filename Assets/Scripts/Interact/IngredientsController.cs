using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    public Transform spawnReceita;
    private Transform spawnUsado;
    //[SerializeField]
    //private GameObject prefabIngrediente1,prefabIngrediente2;
    [SerializeField] public GameObject arma;
    [SerializeField] private float tempoCozinhar = 10f;
    private float countDown;

    private List<int> receita;
    private List<int> receitasProntas; //Receitas que podem ser feitas
    public static IngredientsController Instance;
    [SerializeField] private List<GameObject> ReceitasParaSpawnar; //0 receita estragada , 1 receita1 , 2 receita2 , 3 receita3, 4 receita4

    private enum estadosPanela
    {
        vazia,
        disponivel,
        cheia,
        cozinhando
    };
    estadosPanela estadoAtualPanela = estadosPanela.vazia;
    private int qualReceita = 0;

    //pegar a posicao da panela
    [SerializeField]private GameObject panela;

    //Popups
    [SerializeField] private GameObject popUpPrefab;

    private List<String> listaIngredPanela;

    private bool checkPopup = false;

    private GameObject popupObject;

    /// Audio Managaer
    AudioManager audioManager;

    /// PARTICULAS FUMACA
    [Header("------- Particle References -------")]
    [SerializeField] private ParticleSystem fumacaParticle;
    [SerializeField] private Transform fumacaLocation;
    private ParticleSystem fumacaParticleInstance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        receita = new List<int>()
        {
            0, //quant de Ingrediente 1 na panela
            0, //quant de Ingrediente 2 na panela
            0  //quant de Ingrediente 3 na panela
        };


        //Colocar aqui as receitas que podem ser criadas
        receitasProntas = new List<int>()
        {
            2, //RECEITA 1 - Ingrediente 1
            1, //RECEITA 1 - Ingrediente 2
            0, //RECEITA 1 - Ingrediente 3
            0, //RECEITA 2 - Ingrediente 1
            3, //RECEITA 2 - Ingrediente 2
            0, //RECEITA 2 - Ingrediente 3
            1, //RECEITA 3 - Ingrediente 1
            1, //RECEITA 3 - Ingrediente 2
            1, //RECEITA 3 - Ingrediente 3
            0, //RECEITA 2 - Ingrediente 1
            1, //RECEITA 2 - Ingrediente 2
            2, //RECEITA 2 - Ingrediente 3
        };
        countDown = tempoCozinhar;


        popupObject = Instantiate(popUpPrefab, new Vector3(panela.transform.position.x, panela.transform.position.y + 0.7f, panela.transform.position.z), new Quaternion());
        popupObject.GetComponent<PopUps>().timer = countDown;

        listaIngredPanela = new List<string>()
        {

        };
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.pausa == false && estadoAtualPanela == estadosPanela.cozinhando)
        {
            countDown -= Time.deltaTime;
            popupObject.GetComponent<PopUps>().timer = countDown;
            if (countDown <= 0)
            {
                //dar nome e ve se existe para não spawnar varias receitas
                Instantiate(ReceitasParaSpawnar[qualReceita], new Vector3(spawnReceita.transform.position.x,
                spawnReceita.transform.position.y,
                spawnReceita.transform.position.z), Quaternion.Euler(new Vector3(-90, 0, 0)));

                countDown = tempoCozinhar;
                estadoAtualPanela = estadosPanela.vazia;
                qualReceita = 0;

                popupObject.GetComponent<PopUps>().textValue = "";

                //resetando a panela
                receita[0] = 0;
                receita[1] = 0;
                receita[2] = 0;
            }
        }
    }

    //Debug na tela
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 300, 0, 400, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", estadoAtualPanela , countDown, "qual receita " +qualReceita,receita[0].ToString(),receita[1].ToString()));
        GUILayout.EndArea();
    }

    public void LigarPanela()
    {
        if (estadoAtualPanela == estadosPanela.disponivel || estadoAtualPanela == estadosPanela.cheia) // ver se vai ser assim
        {
            print("Ligou a panela " + receitasProntas.Count);

            CreateFumaca();

            for(var i = 0;i < receitasProntas.Count - 1;i += 3)
            {
                qualReceita++;
                //Verifica a qtd de ingredientes com a receita
                if (receita[0] == receitasProntas[i] && receita[1] == receitasProntas[i+1] && receita[2] == receitasProntas[i + 2])
                {
                    //tirar os ingredientes do popup
                    listaIngredPanela.Clear();
                    popupObject.GetComponent<PopUps>().textValue = "Cooking";

                    estadoAtualPanela = estadosPanela.cozinhando;
                    print("cozinhado receita: " + qualReceita);
                    return;
                }
            }
            
            //Não achou receita, fazendo comida duvidosa

            //tirar os ingredientes do popup
            listaIngredPanela.Clear();

            popupObject.GetComponent<PopUps>().textValue = "Cooking";

            estadoAtualPanela = estadosPanela.cozinhando;
            print("cozinhado, mas errou a receita");
            qualReceita = 0;
        }
        else
        {
            print("Coloque ingredientes na panela (Panela vazia)");
        }
    }

    public void colocandoIngredientesNaPanela()
    {
        if (GameObject.FindWithTag("segurando") != null && GameObject.FindWithTag("segurando").layer != 7) //ve se não é uma receita
        {
            if (estadoAtualPanela == estadosPanela.disponivel || estadoAtualPanela == estadosPanela.vazia) 
            {
                audioManager.PlaySFX(audioManager.splash);

                if (GameObject.FindWithTag("segurando").name == "Peixe(Clone)")
                {
                    receita[0]++;
                    listaIngredPanela.Add("Fish");
                }//pois tomate fica trocando de nome sozinho
                else if (GameObject.FindWithTag("segurando").name == "Tomate(Clone)" /*|| GameObject.FindWithTag("segurando").name == "tomate(Clone)"*/)
                {
                    receita[1]++;
                    listaIngredPanela.Add("Tomato");
                }else if (GameObject.FindWithTag("segurando").name == "Alho(Clone)")
                {
                    receita[2]++;
                    listaIngredPanela.Add("Garlic");
                }

                Debug.Log("O QUE TEM NA PANELA: " + string.Join(", ", listaIngredPanela));

                popupObject.GetComponent<PopUps>().textValue = string.Join(", ", listaIngredPanela);

                estadoAtualPanela = estadosPanela.disponivel;

                if (receita[0] + receita[1] == 3) // 3: tamanho max de ingredientes , ver se como é mlr dps
                {
                    estadoAtualPanela = estadosPanela.cheia;
                }


                //Debug
                foreach (var x in receita)
                {
                    Debug.Log("lista: " + x);
                }

                print("tamanho lista receita:" + receita.Count);

                reciclar();
            }
            else
            {
                print("Panela cheia ou cozinhando");
            }
        }
        else
        {
            print("não tem ingredientes para colocar ou é uma receita");
        }
    }

    public void PegouIngrediente(GameObject ingrediente)
    {
        

        if (GameObject.FindWithTag("segurando") == null)
        {

            arma.SetActive(false);

            print(ingrediente);

            ingrediente.transform.position = GameObject.Find("SeguraItem").transform.position;
            ingrediente.transform.SetParent(GameObject.Find("SeguraItem").transform);
            ingrediente.tag = "segurando";
        }
        else
        {
            print("ja está segurando algo");
        }

    }

    /*public void SpawnaIngrediente(GameObject ingrediente)
    {
        if (GameObject.Find(ingrediente.name + "(Clone)"))
        {
            Debug.Log("Exists");
        }
        else
        {
            Debug.Log("Doesn't exist");

            if (ingrediente.name == "Banana")
            {
                spawnUsado = spawnPointIngrediente;
            }
            else
            {
                spawnUsado = spawnPointIngrediente2;
            }

            Instantiate(ingrediente, new Vector3(spawnUsado.transform.position.x,
            spawnUsado.transform.position.y,
            spawnUsado.transform.position.z), Quaternion.identity);
        }
    }*/

    public void reciclar()
    {
        if (arma.activeSelf == false)
        {
            Debug.Log("destroy fruit");
            Destroy(GameObject.FindWithTag("segurando"));
            arma.SetActive(true);
        }
        else
        {
            Debug.Log("não está segurando Ingrediente");
        }
    }

    private void CreateFumaca()
    {
        fumacaParticleInstance = Instantiate(fumacaParticle, fumacaLocation.position,Quaternion.Euler(-90f, 0, 0));
    }
}
