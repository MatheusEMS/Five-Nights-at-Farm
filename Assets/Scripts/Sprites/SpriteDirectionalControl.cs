using UnityEngine;

public class SpriteDirectionalControl : MonoBehaviour
{
    [SerializeField] float backAngle = 65f;
    [SerializeField] float sideAngle = 155f;
    [SerializeField] Transform mainTransform;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    private void LateUpdate()
    {
        Vector3 camFowardVector = new Vector3(Camera.main.transform.forward.x,0f, Camera.main.transform.forward.z);
        //Debug.DrawRay(Camera.main.transform.position, camFowardVector * 5f, Color.magenta);

        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camFowardVector, Vector3.up);

        Vector2 animationDirection = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);



        if(angle < backAngle)
        {
            //Back animation
            animationDirection = new Vector2(0f, -1f);
        }else if (angle < sideAngle)
        {
            //side animation
            //in this case is have only one side animation
            /*animationDirection = new Vector2(1f, 0f);

            //this change the side animation based on what side
            //the camera is viewing the sprite from
            if (signedAngle < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }*/

            //use this if you have 2 different animations
            //for left and right
            if (signedAngle < 0)
            {
                animationDirection = new Vector2(-1f, 0f);
            }
            else
            {
                animationDirection = new Vector2(1f, 0f);
            }
            
        }
        else
        {
            //Front animation
            animationDirection = new Vector2(0f, 1f);
        }

        animator.SetFloat("moveX", animationDirection.x);
        animator.SetFloat("moveY", animationDirection.y);
    }
}
