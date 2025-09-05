using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    public Transform spawnPointFruta, spawnPointFruta2;
    private Transform spawnUsado;
    //[SerializeField]
    //private GameObject prefabFruta1,prefabFruta2;
    [SerializeField] private GameObject arma;
    private List<string> receita;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        receita = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cozinhar()
    {
        if (GameObject.FindWithTag("segurando") != null)
        {
            if (receita.Count < 3) //tamanho max de ingerdientes
            {
                receita.Add(GameObject.FindWithTag("segurando").name);

                foreach (var x in receita)
                {
                    Debug.Log("lista:" + x);
                }

                print("tamanho lista receita:" + receita.Count);

                reciclar();
            }
            else
            {
                print("Panela cheia");
            }
        }
        else
        {
            print("não tem ingredientes para colocar");
        }
    }

    public void PegouFruta(GameObject ingrediente)
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

    public void SpawnaFruta(GameObject ingrediente)
    {
        if (GameObject.Find(ingrediente.name + "(Clone)"))
        {
            Debug.Log("Exists");
        }
        else
        {
            Debug.Log("Doesn't exist");

            if (ingrediente.name == "Fruta1")
            {
                spawnUsado = spawnPointFruta;
            }
            else
            {
                spawnUsado = spawnPointFruta2;
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
            Debug.Log("não está segurando fruta");
        }
    }
}
