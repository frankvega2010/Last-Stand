using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "bullet":
                //Set Active false on the pool.
                int damage = collision.gameObject.transform.GetComponentInParent<Bullet>().damage;
                Destroy(collision.transform.parent.gameObject);
                TakeDamage(damage);
                break;
            default:
                break;
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
