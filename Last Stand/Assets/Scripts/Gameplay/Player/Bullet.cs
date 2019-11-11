using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public int damage;
    public float lifespan;
    public float shapeSizeTimeMultiplier;

    private bool canChangeSize;
    private float shapeSizeTimer;
    private float lifespanTimer;
    private Vector3 originalScale;
    // Start is called before the first frame update
    private void Start()
    {
        canChangeSize = true;
        originalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        lifespanTimer += Time.deltaTime;

        if(canChangeSize)
        {
            shapeSizeTimer += Time.deltaTime * shapeSizeTimeMultiplier;
        }

        if (lifespanTimer >= lifespan)
        {
            Destroy(gameObject);
        }

        if(canChangeSize)
        {
            if (shapeSizeTimer <= originalScale.x)
            {
                transform.localScale = new Vector3(lifespanTimer, lifespanTimer, lifespanTimer);
            }
            else
            {
                canChangeSize = false;
                transform.localScale = originalScale;
            }
        }


        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "stage":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
