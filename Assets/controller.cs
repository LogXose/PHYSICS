using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class controller : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject ammo;
    [SerializeField] float speed = 5;
    Vector3 diff; 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        diff = Camera.main.transform.position - transform.position;
    }
    [SerializeField] float scrollSpeed = 1;
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
            GameObject _ammo = Instantiate(ammo, transform.GetChild(0));
            _ammo.SetActive(true);
            _ammo.transform.position = ammo.transform.position;
            _ammo.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
            _ammo.transform.parent = null;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float min = 3;
            float max = 18;
            float now = Camera.main.orthographicSize;
            Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                Camera.main.orthographicSize = Mathf.Lerp(now, min, scrollSpeed * Time.deltaTime);
            }
            else
            {
                Camera.main.orthographicSize = Mathf.Lerp(now, max, scrollSpeed * Time.deltaTime);
            }
        }
    }
    [SerializeField] float camSpeed = 0.7f;
    private void LateUpdate()
    {
        Camera camera = Camera.main;
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + diff, Time.deltaTime * camSpeed);
    }
}
