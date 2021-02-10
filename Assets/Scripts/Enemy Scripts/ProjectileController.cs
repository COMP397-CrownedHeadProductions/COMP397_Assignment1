using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject player;
    public int damage;

    public PlayerController playerDamage;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDamage = GameObject.Find("Player").GetComponent<PlayerController>();
        //player = GameObject.FindWithTag("Player");
        //player2 = GameObject.FindWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        damage = Random.Range(15, 25);
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponentInChildren<PlayerController>().health -= damage;
            Destroy(gameObject);
            Debug.Log("Enemy dealt " + damage + " damage to Player 1");
        }
    }
}
