using UnityEngine;
public class RotateWall : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private bool ifRotate;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        ifRotate = animator.GetBool("Rotate");
        //On trigger collision - if the object is not rotating, make it rotate
        if (!ifRotate)
        {
            animator.SetBool("Rotate", true);
        }
        //Else - make it stop rotating
        else
            animator.SetBool("Rotate", false);
    }
}
