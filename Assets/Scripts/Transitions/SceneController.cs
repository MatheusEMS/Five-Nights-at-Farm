using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

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

    public void NextLevel()
    {
        StartCoroutine(LoadLevel(null));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string next)
    {
        transitionAnim.SetTrigger("End");


        yield return new WaitForSeconds(2f);

        AsyncOperation operation;

        if (next == null)
        {
            //vai para a proxima cena com está no build profiles
            operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            operation = SceneManager.LoadSceneAsync(next);
        }

        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
            yield return null;

        operation.allowSceneActivation = true;

        yield return null;

        transitionAnim.SetTrigger("Start");

        if (next == "SampleScene")
        {
            Debug.Log("carregou");
        }
    }
}
