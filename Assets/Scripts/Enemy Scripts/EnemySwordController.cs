using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordController : MonoBehaviour
{
    public Rigidbody rb;
    int damage;
    public int damageRange1;
    public int damageRange2;

    public PlayerController playerDamage;

    // Start is called before the first frame update
    void Start()
    {
        playerDamage = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        damage = Random.Range(damageRange1, damageRange2);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currentHealth -= damage;
            Debug.Log("Knight dealt " + damage + " to Player.");
        }
    }
}
