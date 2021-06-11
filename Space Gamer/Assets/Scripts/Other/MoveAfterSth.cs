using UnityEngine;

public class MoveAfterSth : MonoBehaviour
{
    [SerializeField]
    private Transform moveAfter;
    [SerializeField]
    private float offsetX, offsetY, offsetZ;
    [SerializeField]
    private float rotateX, rotateY, rotateZ;
    private void Update()
    {
        //Set the object's position and rotation to those of the object's that it's supposed to move after, with optional offsets
        transform.SetPositionAndRotation(new Vector3(moveAfter.position.x + offsetX, moveAfter.position.y + offsetY, moveAfter.position.z + offsetZ),
        Quaternion.Euler(moveAfter.eulerAngles.x + rotateX, moveAfter.eulerAngles.y + rotateY, moveAfter.eulerAngles.z + rotateZ));
    }
}
