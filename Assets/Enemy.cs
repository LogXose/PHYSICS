using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject particle;
    [SerializeField] GameObject LootPackage;
    [SerializeField] GameObject Loot;

    public void CreateLoot()
    {
        int count = Random.Range(1, 5);
        for (int i = 0; i < count; i++)
        {
            GameObject loot = Instantiate(Loot, LootPackage.transform);
            loot.transform.position = LootPackage.transform.position;
            loot.transform.localScale *= Random.Range(0.3f, 0.5f);
        }
    }
    void Start()
    {
        gameObject.GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        CreateLoot();
    } 
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (shot > 1)
        {
            particle.gameObject.SetActive(true);
            particle.transform.parent = null;
            LootPackage.gameObject.SetActive(true);
            LootPackage.transform.parent = null;
            Destroy(gameObject);
          
        }
    }
    int shot = 0;
    public void Shooted()
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.Lerp(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color,
            GameObject.FindGameObjectWithTag("Player").GetComponent<MeshRenderer>().material.color, 0.3f);
        GetComponent<Animator>().SetTrigger("Hit");
        shot++;
    }
}
