using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("General Settings")]
    public int damage;
    public int bulletSpeed;
    public float fireRate;

    [Header("Assign Components")]
    public GameObject bulletTemplate;
    public GameObject spawnPoint;

    private Bullet bullet;
    private float fireRateTimer;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        bullet = bulletTemplate.GetComponent<Bullet>();
        bullet.damage = damage;
        bullet.speed = bulletSpeed;
        bullet.direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot)
        {
            if(Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            fireRateTimer += Time.deltaTime;

            if (fireRateTimer >= fireRate)
            {
                canShoot = true;
                fireRateTimer = 0;
            }
        }
        
    }

    public void Shoot()
    {
        canShoot = false;
        bullet.damage = damage;
        bullet.speed = bulletSpeed;
        bullet.direction = transform.forward;
        GameObject bulletObject = Instantiate(bulletTemplate);
        bulletObject.transform.rotation = transform.rotation;
        bulletObject.transform.position = spawnPoint.transform.position;

        float xDispersion = Random.Range(-0.2f,0.2f);
        float yDispersion = Random.Range(-0.2f, 0.2f);

        Vector3 dispersion = new Vector3(xDispersion, yDispersion, 0);

        bulletObject.transform.position += dispersion;

        bulletObject.SetActive(true);
    }
}
