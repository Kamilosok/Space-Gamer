using UnityEngine;

public class CameraWork : MonoBehaviour
{
    private Transform parentTransform;
    private RaycastHit raycastHit;
    private Vector3 basicPos = new Vector3(-0.6f, 0.2f, 0);
    private void Start()
    {
        parentTransform = transform.parent;
    }
    private void Update()
    {
        //Making a raycast to check if anything blocks camera view, if true - move camera to the point where raycast collided
        if (Physics.Raycast(parentTransform.position, -(parentTransform.position - transform.position), out raycastHit, 6.4f))
            transform.position = raycastHit.point;
        //Else - if camera isn't in it's native position, move it there
        else if (transform.localPosition != basicPos)
        {
            transform.localPosition = basicPos;
        }
    }
}
