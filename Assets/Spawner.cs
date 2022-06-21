using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float perSecond = 4;
    int shooted = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy);
        enemy.transform.position = transform.position;
        StartCoroutine(Fade());
    }

    public void Shooted()
    {
        shooted++;
        if(shooted >= 2)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(perSecond);
        Instantiate(enemy);
        enemy.transform.position = transform.position;
        StartCoroutine(Fade());
    }
}
