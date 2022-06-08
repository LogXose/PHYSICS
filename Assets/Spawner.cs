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

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(perSecond);
        Instantiate(enemy);
        enemy.transform.position = new Vector3(transform.position.x, enemy.transform.position.y, transform.position.z);
        StartCoroutine(Fade());
    }
}
