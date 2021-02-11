using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Source File: ProjectileController.cs
 * Editors: Timothy Garcia
 * Student Number: 300898955
 * Date Modified: 02-14-2021
 * Program: 3109 - Game-Programming(Optional Co-op)
 * 
 * --------------------Revision History--------------------
 *                   Pre-Alpha - 2021.02.14
 * - Projectile for ranged enemy implemented(WIP)
 * - Damage to player functional
 * - Game object destroyed when it collides with player or wall
 */

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject player;
    public Transform playerBody;
    int damage;
    public int damageRange1;
    public int damageRange2;

    public PlayerController playerDamage;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        playerDamage = GameObject.Find("Player").GetComponent<PlayerController>();
        transform.LookAt(playerBody);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        damage = Random.Range(damageRange1, damageRange2);
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currentHealth -= damage;
            Destroy(gameObject);
            Debug.Log("Enemy dealt " + damage + " damage to Player 1");
        }
    }
}
