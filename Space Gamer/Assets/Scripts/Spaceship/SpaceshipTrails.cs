using UnityEngine;
using System.Collections;

public class SpaceshipTrails : MonoBehaviour
{
    private IEnumerator coroutine;
    private TrailRenderer trail;
    private byte i;
    private void Start()
    {
        trail = gameObject.GetComponent<TrailRenderer>();
    }
    public void EnableTrail()
    {
        //Make the trail's time longer
        trail.time = 0.1f;
    }
    public void DisableTrail()
    {
        //Set a value and start a coroutine
        i = 100;
        coroutine = TurnOff();
        StartCoroutine(coroutine);
    }
    private IEnumerator TurnOff()
    {
        //Over the next 100 Updates make the trail slowly disappear
        while (i > 0)
        {
            i -= 1;
            trail.time -= 0.001f;
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(coroutine);
    }
}
