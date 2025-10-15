using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{

    [SerializeField]private string nomeDoLevelDoJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelCreditos;

    public void jogar()
    {
        //utilizando o SceneController
        SceneController.instance.LoadScene(nomeDoLevelDoJogo);
        //SceneManager.LoadScene(nomeDoLevelDoJogo);

    }
    public void AbrirCreditos()
    {

        painelMenuInicial.SetActive(false);
        painelCreditos.SetActive(true);

    }

    public void FecharCreditos()
    {


        painelMenuInicial.SetActive(true);
        painelCreditos.SetActive(false);

    }

    public void SairJogo()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();

    }
}
