using UnityEngine;

public class StepsManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    public AudioClip walkDirt;

    //for steps
    [Header("------- Audio Steps -------")]
    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask LayerMask;
    private void Start()
    {

    }

    public void PlaySFXsteps(AudioClip clip)
    {
        SFXSource.pitch = Random.Range(0.8f, 1f);
        SFXSource.PlayOneShot(clip);
    }

    public void Footstep()
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, LayerMask))
        {
            if (hit.collider.CompareTag("dirt"))
            {
                PlaySFXsteps(walkDirt);
            }
        }
    }
}
