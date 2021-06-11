using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private IEnumerator coroutine;
    private void Awake()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        transform.parent = null;
    }
    private void OnEnable()
    {
        coroutine = setFalse();
        StartCoroutine(coroutine);
    }
    IEnumerator setFalse()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        StopCoroutine(coroutine);
    }
}
