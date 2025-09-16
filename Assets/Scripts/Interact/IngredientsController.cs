using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    public Transform spawnPointIngrediente, spawnPointIngrediente2, spawnReceita;
    private Transform spawnUsado;
    //[SerializeField]
    //private GameObject prefabIngrediente1,prefabIngrediente2;
    [SerializeField] private GameObject arma;
    [SerializeField] private float tempoCozinhar = 10f;
    private float countDown;

    private List<int> receita;
    private List<int> receitasProntas; //Receitas que podem ser feitas

    [SerializeField] private List<GameObject> ReceitasParaSpawnar; //0 receita estragada , 1 receita1 , 2 receita2

    private enum estadosPanela
    {
        vazia,
        disponivel,
        cheia,
        cozinhando
    };
    estadosPanela estadoAtualPanela = estadosPanela.vazia;
    private int qualReceita = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        receita = new List<int>()
        {
            0, //quant de Ingrediente 1 na panela
            0 //quant de Ingrediente 2 na panela
        };


        //Colocar aqui as receitas que podem ser criadas
        receitasProntas = new List<int>()
        {
            2, //RECEITA 1 - Ingrediente 1
            1, //RECEITA 1 - Ingrediente 2
            3, //RECEITA 2 - Ingrediente 1
            0  //RECEITA 2 - Ingrediente 2
        };
        countDown = tempoCozinhar;
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoAtualPanela == estadosPanela.cozinhando)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                Instantiate(ReceitasParaSpawnar[qualReceita], new Vector3(spawnReceita.transform.position.x,
                spawnReceita.transform.position.y,
                spawnReceita.transform.position.z), Quaternion.identity);

                countDown = tempoCozinhar;
                estadoAtualPanela = estadosPanela.vazia;
                qualReceita = 0;
            }
        }
    }

    //Debug na tela
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", estadoAtualPanela , countDown, qualReceita));
        GUILayout.EndArea();
    }

    public void LigarPanela()
    {
        if (estadoAtualPanela == estadosPanela.disponivel || estadoAtualPanela == estadosPanela.cheia) // ver se vai ser assim
        {
            print("Ligou a panela " + receitasProntas.Count);

            for(var i = 0;i < receitasProntas.Count - 1;i += 2)
            {
                qualReceita++;
                if (receita[0] == receitasProntas[i] && receita[1] == receitasProntas[i+1])
                {
                    estadoAtualPanela = estadosPanela.cozinhando;
                    print("cozinhado receita: " + qualReceita);
                    return;
                }
            }

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
                if (GameObject.FindWithTag("segurando").name == "Ingrediente1(Clone)")
                {
                    receita[0]++;
                }
                else if (GameObject.FindWithTag("segurando").name == "Ingrediente2(Clone)")
                {
                    receita[1]++;
                }

                estadoAtualPanela = estadosPanela.disponivel;

                if (receita[0] + receita[1] == 3) // 3: tamanho max de ingredientes , ver se como é mlr dps
                {
                    estadoAtualPanela = estadosPanela.cheia;
                }


                //Debug
                foreach (var x in receita)
                {
                    Debug.Log("lista:" + x);
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

    public void SpawnaIngrediente(GameObject ingrediente)
    {
        if (GameObject.Find(ingrediente.name + "(Clone)"))
        {
            Debug.Log("Exists");
        }
        else
        {
            Debug.Log("Doesn't exist");

            if (ingrediente.name == "Ingrediente1")
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
    }

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
}
