using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
    } 
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().velocity.magnitude >= 0.1f )
        {
            gameObject.GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        if(shot > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
            Destroy(gameObject);
        }
    }
    int shot = 0;
    public void Shooted()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(gameObject.GetComponent<MeshRenderer>().material.color,
            GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>().material.color, 0.3f);
        shot++;
    }
}
