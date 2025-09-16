using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using System.Collections;

public class PlayerArms : MonoBehaviour
{
    //video : codigo de movimento de camera 3

    [SerializeField] Animator animBracos;

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

    // Update is called once per frame
    void Update()
    {
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

        animBracos.SetFloat("curSpeed", curSpeed);

        //se tiver mira, adicionar aiming no animator
        aiming = Input.GetKey(KeyCode.Mouse1);
        //animBracos.SetFloat("aiming", aiming);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CanFire == true)
            {
                if (GlobalAmmo.municaopistolacount > 1)
                {
                    GlobalAmmo.municaopistolacount -= 1;
                    Shoot();
                    CanFire = false;
                    StartCoroutine(AtirandoPistola());
                  }else{

                    GlobalAmmo.municaopistolacount -= 1;
                    CanFire = false;
                    StartCoroutine(Recarregar());

                }

                } 



            RaycastHit hit;

            if (Physics.Raycast(posTiro.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000f))
            {
                //hit.collider.gameObject.GetComponent<Rigidbody>().AddRelativeForce(hit.point, ForceMode.Impulse);
                //Instantiate(decal, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.01f), Quaternion.identity);

                decal = oPooler.inst.GetPoolObj();

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

    void Shoot()
    {
        //se tiver mira

        //if (aiming)
        //{
        //animBracos.CrossFade("AimShoot", 0.02f, 0, 0);
        //}
        //else
        //{
        animBracos.CrossFade("PistolShoot", 0.02f, 0, 0);

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
        yield return new WaitForSeconds(0.8f);
        CanFire = true; 


    }
    IEnumerator Recarregar()
    {

        yield return new WaitForSeconds(1.8f);

        CanFire = true;
        GlobalAmmo.municaopistolacount = 8;
    }

}
