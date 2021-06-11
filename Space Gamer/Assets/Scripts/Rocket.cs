using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private IEnumerator coroutine;
    private Transform parentTransform;
    private ExplosionPooler explosionPooler;

    private void Awake()
    {
        if(coroutine!=null)
            StopCoroutine(coroutine);
        parentTransform = transform.parent;
        explosionPooler = transform.parent.GetChild(1).GetComponent<ExplosionPooler>();
        transform.parent = null;
    }
    private void OnEnable()
    {
        transform.position = parentTransform.position;
        transform.rotation = parentTransform.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = transform.right * 50;
        coroutine = setFalse();
        StartCoroutine(coroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the object didn't trigger with the Player layer clone a particle explosion, and disable the object
        if (other.gameObject.layer != 8)
        {
            explosionPooler.Explode(transform);
            gameObject.SetActive(false);
        }
    }
    //If the rocket didn't hit anything, disable it
    IEnumerator setFalse()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        StopCoroutine(coroutine);
    }
}
