using UnityEngine;

public class RocketPooler : MonoBehaviour
{
    [SerializeField]
    private GameObject rocketPrefab;
    private GameObject[] rocketPool = new GameObject[10];
    private byte counter;
    private Transform parentTransform;
    private void Start()
    {
        counter = 0;
        parentTransform = transform.parent;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (rocketPool[9] == null)
            {
                rocketPool[counter] = Instantiate(rocketPrefab, parentTransform);
                rocketPool[counter].name = counter.ToString();
                counter += 1;
            }
            else
            {
                rocketPool[counter].SetActive(true);
                counter += 1;
            }

            if (counter == 10)
            {
                counter = 0;
            }

        }
    }
}
