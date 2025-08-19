using System.Collections.Generic;
using UnityEngine;

public class oPooler : MonoBehaviour
{
    public static oPooler inst;
    public GameObject poolObj;
    public int quantPool;
    //aumenta a lista
    public bool limiteSeg;

    private List<GameObject> poolObjectsList;

    private void Awake()
    {
        inst = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poolObjectsList = new List<GameObject>();

        for (int x = 0; x < quantPool; x++)
        {
            GameObject obj = Instantiate(poolObj);
            obj.SetActive(false);
            poolObjectsList.Add(obj);
        }
    }

    public GameObject GetPoolObj()
    {
        for (int x = 0; x < poolObjectsList.Count; x++)
        {
            if (!poolObjectsList[x].activeInHierarchy)
            {
                return poolObjectsList[x];
            }
        }

        if (limiteSeg)
        {
            GameObject obj = Instantiate(poolObj);
            poolObjectsList.Add(obj);
            return obj;
        }

        return null;
    }
}
