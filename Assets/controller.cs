using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class controller : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject ammo;
    [SerializeField] float ammoSpeed = 5;
    [SerializeField] float rotationSpeed = 5;
    Vector3 diff; 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        diff = Camera.main.transform.position - transform.position;
    }
    [SerializeField] float scrollSpeed = 1;
    Vector3 movement;
    public DynamicJoystick dynamicJoystick;
    void Update()
    {
        //keyboard
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        transform.Rotate(new Vector3(0, horizontalInput * rotationSpeed));
        transform.Translate(Vector3.forward * verticalInput * agent.speed * Time.deltaTime);
        
        //joystick
        float _verticalInput = dynamicJoystick.Vertical;
        float _horizantalInput = dynamicJoystick.Horizontal;
        transform.Rotate(new Vector3(0, _horizantalInput * rotationSpeed));
        transform.Translate(Vector3.forward * _verticalInput * agent.speed * Time.deltaTime);
        //transform.Translate(Vector3.forward * _verticalInput * agent.speed * Time.deltaTime);
        //agent.Move(movement * Time.deltaTime * agent.speed);
        //agent.SetDestination(transform.position + movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
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

    public void Fire()
    {
        GameObject efekt = GameObject.Instantiate(particle, transform);
        efekt.transform.localPosition = transform.GetChild(0).localPosition;
        GameObject _ammo = Instantiate(ammo, transform.GetChild(0));
        _ammo.SetActive(true);
        _ammo.transform.position = ammo.transform.position;
        _ammo.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ammoSpeed);
        _ammo.transform.parent = null;
    }
}
