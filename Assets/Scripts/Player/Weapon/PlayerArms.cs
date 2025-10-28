using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerArms : MonoBehaviour
{
    //video : codigo de movimento de camera 3
    [SerializeField] Animator animBracos;
    public bool ataque_da_onca = false;
    //float para ter uma interpolação entre anim
    private float curSpeed;
    private int targetSpeed;
    [SerializeField] float acceleration = 4;

    //capsulas saindo da arma
    [SerializeField] private Transform posEject;
    //tiro
    [SerializeField] private Transform posTiro;

    //marca de tiro na parede
    [SerializeField] private GameObject decal;

    [SerializeField] private VisualEffect vfxFumaca;
    [SerializeField] private VisualEffect vfxSangue;

    //[SerializeField] private Image mira;
    public LayerMask mask;
    [SerializeField] bool CanFire = true;


    //Audios
    AudioManager audioManager;

    //Codigo para dano com os animais
    [SerializeField] public GameObject Ia_sarue;
    [SerializeField] public GameObject Ia_cobra;
    [SerializeField] public GameObject Ia_onca;
    public List<GameObject> ia_animais;
    public GameObject ia_alvo;
    public GameObject alvo;
    [SerializeField] private float damage;
    //pega o audioManager
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    public void Eject(GameObject prefab)
    {
        var projectObj = Instantiate(prefab, posEject.position, Quaternion.identity);
        projectObj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0.1f, 0.1f, 0.1f), ForceMode.Impulse);
    }


    //animacoes
    private bool aiming, jumping, reloading;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    IEnumerator Efeito_Ataque_Onca()
    {

        yield return new WaitForSeconds(2);
        ataque_da_onca = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.pausa == false) //pausa o player
        {

            if (ataque_da_onca)
            {

                StartCoroutine(Efeito_Ataque_Onca());

            }

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                //acceleration = 4;
                targetSpeed = 1;
            }
            else
            {
                //acceleration = 16;
                targetSpeed = 0;
            }

            curSpeed = Mathf.MoveTowards(curSpeed, targetSpeed, Time.deltaTime * acceleration);

            //animBracos.SetFloat("curSpeed", curSpeed);

            //se tiver mira, adicionar aiming no animator
            aiming = Input.GetKey(KeyCode.Mouse1);
            //animBracos.SetFloat("aiming", aiming);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (CanFire == true && ataque_da_onca == false)
                {

                    
                    if (GlobalAmmo.municaopistolacount > 1)
                    {
                        StartCoroutine(AtirandoPistola());
                        Debug.Log("atirou");
                        //GlobalAmmo.municaopistolacount -= 1;
                        audioManager.PlaySFX(audioManager.shoot);
                        Shoot();
                        
                        //CanFire = false;
                        
                    }
                    else
                    {
                        Debug.Log("atirou ultima bala");
                        audioManager.PlaySFX(audioManager.shoot);
                        GlobalAmmo.municaopistolacount -= 1;
                        StartCoroutine(Recarregar());
                        CanFire = false;
                        

                    }

                    RaycastHit hit;

                    if (Physics.Raycast(posTiro.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f))
                    {
                        //hit.collider.gameObject.GetComponent<Rigidbody>().AddRelativeForce(hit.point, ForceMode.Impulse);
                        //Instantiate(decal, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f), Quaternion.identity);

                        decal = oPooler.inst.GetPoolObj();
                        ///acertar um inimigo
                        if(hit.transform.gameObject == Ia_cobra  && GlobalAmmo.municaopistolacount > 1)
                        {
                            ia_alvo = hit.transform.gameObject;
                           // alvo = ia_alvo.GetInstanceID();
                            SistemadeVida vida = GetComponent<SistemadeVida>();
                            vida.TakeDamage3(damage);
                        }
                        else if(hit.transform.gameObject == Ia_onca && GlobalAmmo.municaopistolacount > 1)
                        {
                            ia_alvo = hit.transform.gameObject;
                           // ia_alvo.GetInstanceID();
                            SistemadeVida vida = GetComponent<SistemadeVida>();
                            vida.TakeDamage3(damage);

                        }
                        else if(hit.transform.gameObject == Ia_sarue && GlobalAmmo.municaopistolacount > 1) {
                           
                            ia_alvo = hit.transform.gameObject;
                           // ia_alvo.GetInstanceID();
                            SistemadeVida vida = GetComponent<SistemadeVida>();
                            vida.TakeDamage3(damage);


                        }
                       // ia_alvo = null;

                        if (decal == null)
                        {
                            return;
                        }

                        decal.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f);
                        decal.SetActive(true);

                        if (hit.collider.gameObject.CompareTag("Inimigo"))
                        {
                            SangueVFX(new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f));
                        }
                        else
                        {
                            FumacaVFX(new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f));
                        }

                    }
               
                }
            }
        }
    }

    void Shoot()
    {
        //se tiver mira

        //if (aiming)
        //{
        //animBracos.CrossFade("AimShoot", 0.02f, 0, 0);
        //}
        //else
        //{

        //animacao
        //animBracos.CrossFade("PistolShoot", 0.02f, 0, 0);

        //se tiver muito efeitos, fazer instantiate com o objPooling é o melhor
        //vfxFumaca.SendEvent("TiroHit");

        //}
    }

    void FumacaVFX(Vector3 pos)
    {
        vfxFumaca.transform.position = pos;
        vfxFumaca.SendEvent("TiroHit");
    }

    void SangueVFX(Vector3 pos)
    {
        vfxSangue.transform.position = pos;
        vfxSangue.SendEvent("TiroSangue");
    }
    
    IEnumerator AtirandoPistola()
    {
        GlobalAmmo.municaopistolacount -= 1;
        yield return new WaitForSeconds(5);
        CanFire = true;
        

    }
    IEnumerator Recarregar()
    {

       yield return 

        CanFire = true;
        GlobalAmmo.municaopistolacount = 8;
    }

}
