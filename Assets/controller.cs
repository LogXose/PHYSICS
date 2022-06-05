using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class controller : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject particle;
    Vector3 diff; 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        diff = Camera.main.transform.position - transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }else if (Input.GetMouseButtonDown(1))
        {
            GameObject efekt = GameObject.Instantiate(particle, transform);
            efekt.transform.localPosition = transform.GetChild(0).localPosition;
        }
    }

    private void LateUpdate()
    {
        Camera camera = Camera.main;
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + diff, Time.deltaTime * 0.125f);
    }
}
