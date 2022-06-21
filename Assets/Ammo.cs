using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    float start = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndPrint());
    }
    IEnumerator WaitAndPrint()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().Shooted(); 
        }else if (collision.collider.CompareTag("Spawner"))
        {
            collision.collider.GetComponent<Spawner>().Shooted();
        }
    }
}
