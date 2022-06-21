using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float health = 100;
    public const float maxHealth = 100;
    public const float minHealth = 0;
    public float ammoValue = 0.5f;
    [SerializeField] TextMeshPro healthText;
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
        ArrangeScale();

        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(health > ammoValue)
            Fire();
        }
        Scrolling();
        
    }
    [SerializeField] float camSpeed = 0.7f;
    public void Movement()
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
    }
    public void Scrolling()
    {
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
    private void LateUpdate()
    {
        /*Camera camera = Camera.main;
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + diff, Time.deltaTime * camSpeed);*/
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
        DecreaseHealth(ammoValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.transform.localScale.magnitude);
        if (collision.collider.CompareTag("Loot"))
        {
            IncreaseHealth(collision.collider.transform.localScale.magnitude);
            Destroy(collision.collider.gameObject);
        }else if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().Shooted();
            collision.collider.GetComponent<Enemy>().Shooted();
            collision.collider.GetComponent<Enemy>().Shooted();
            collision.collider.GetComponent<Enemy>().Shooted();
            collision.collider.GetComponent<Enemy>().Shooted();
            DecreaseHealth(maxHealth / 2);
        }
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        healthText.text = "%" + health.ToString("0.0");
    }

    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health < minHealth) health = minHealth;
        healthText.text = "%" + health.ToString("0.0");
    }
    
    public void ArrangeScale()
    {
        float ratio = health / maxHealth;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * ratio, Time.deltaTime * 5f);
    }

    public void Death()
    {
        Debug.Log("Death");
    }
    
}
