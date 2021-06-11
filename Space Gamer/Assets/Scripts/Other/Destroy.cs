using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField]
    float time;
    private void Start()
    {
        //Destroy the object after a certaing time
        Destroy(gameObject, time);
    }
}
