using System.Collections;
using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    public static Transform playerTransform;
    [SerializeField] float backAngle = 65f;
    [SerializeField] float sideAngle = 155f;
    [SerializeField] Transform mainTransform;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    private bool olhar_pro_player = true;
   // public static Transform targetPoin = playerTransform.position;
    // Update is called once per frame
    private void LateUpdate()
    {


        if (olhar_pro_player)
        {
            StartCoroutine(olhar_player());

        }


        Vector3 camFowardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);    

        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camFowardVector,Vector3.up);
        Vector2 animationDirection = new Vector2(0, -1f);

        float angle = Mathf.Abs(signedAngle);

        ;
       
        if (angle < backAngle)
        {

            animationDirection = new Vector2(0f, -1f);
            
        }
        else if (angle > sideAngle)
        {
           animationDirection = new Vector2 (1f, 0f);
          
            if (signedAngle < 0)
            {

                spriteRenderer.flipX = true;


            }
            else
            {

                spriteRenderer.flipX = false;
            }


        }
        else
        {

            animationDirection = new Vector2(0f, 1f);
        }

        animator.SetFloat("movex", animationDirection.x);
        animator.SetFloat("movey", animationDirection.y);
    }
    IEnumerator olhar_player()
    {
      //  transform.LookAt(targetPoint, Vector3.up);
        yield return new WaitForSeconds(1);
        olhar_pro_player = true;


    }
}
