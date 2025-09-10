using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class spawner : MonoBehaviour
{
    public Transform spawnerlocation;
   [SerializeField] public GameObject Ia;
    public int xPos;
    public int zPos;

    [SerializeField] public int EnemyCount;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {

        while (EnemyCount < 20)
        {
            
            xPos = Random.Range(-16, 13);
            zPos = Random.Range(-20, 1);

            Instantiate(Ia, new Vector3(xPos, -4, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1);
            EnemyCount++;
        }
    }
}
