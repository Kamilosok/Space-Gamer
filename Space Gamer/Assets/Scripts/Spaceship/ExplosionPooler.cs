using UnityEngine;

public class ExplosionPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;
    private GameObject[] explosionPool = new GameObject[10];
    private byte counter;
    private void Start()
    {
        counter = 0;
    }
    public void Explode(Transform transform)
    {
        if (explosionPool[9] == null)
        {
            explosionPool[counter] = Instantiate(explosionPrefab, transform);
            explosionPool[counter].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            counter += 1;
        }
        else
        {
            explosionPool[counter].SetActive(true);
            explosionPool[counter].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            explosionPool[counter].transform.position = transform.position;
            counter += 1;
        }

        if (counter == 10)
        {
            counter = 0;
        }
    }

}
