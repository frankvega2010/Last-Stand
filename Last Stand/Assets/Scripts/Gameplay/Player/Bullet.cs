using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public int damage;
    public float lifespan;

    private float lifespanTimer;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        lifespanTimer += Time.deltaTime;

        if(lifespanTimer >= lifespan)
        {
            Destroy(gameObject);
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "enemy":
            default:
                break;
        }
    }
}
