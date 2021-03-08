using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordController : MonoBehaviour
{
    int damage;
    public int damageRange1;
    public int damageRange2;
    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("Player dealt " + damage + " to ranged enemy.");
        }
    }
}
