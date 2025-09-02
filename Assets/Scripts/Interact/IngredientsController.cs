using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    public Transform spawnPointFruta1;
    [SerializeField]
    private GameObject prefabFruta1;
    [SerializeField] private GameObject arma;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PegouFruta(GameObject ingrediente)
    {
        print(ingrediente);

        arma.SetActive(false);

        ingrediente.transform.position = GameObject.Find("SeguraItem").transform.position;
        ingrediente.transform.SetParent(GameObject.Find("SeguraItem").transform);
        ingrediente.tag = "Untagged";

    }

    public void SpawnaFruta1()
    {
        if (arma.activeSelf == false)
        {
            Debug.Log("destroy fruit");
            Destroy(GameObject.Find("Fruta1(Clone)"));
            arma.SetActive(true);
        }
        else
        {
            if (GameObject.Find("Fruta1(Clone)"))
            {
                Debug.Log("Exists");
            }
            else
            {
                Debug.Log("Doesn't exist");
                Instantiate(prefabFruta1, new Vector3(spawnPointFruta1.transform.position.x,
                spawnPointFruta1.transform.position.y,
                spawnPointFruta1.transform.position.z), Quaternion.identity);
            }
        }
    }
}
