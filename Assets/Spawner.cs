using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float perSecond = 4;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy);
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(perSecond);
        Instantiate(enemy);
        StartCoroutine(Fade());
    }
}
