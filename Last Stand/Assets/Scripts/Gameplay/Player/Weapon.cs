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

    [Header("Model Settings")]
    public Animator animator;
    public GameObject weaponBarrelModel;
    public float maxBarrelSpeed;
    public float barrelFireTime;
    public float barrelSpeedMultiplier;


    private float barrelTorqueTimer;
    private Rigidbody rig;
    private Bullet bullet;
    private float fireRateTimer;
    private bool canShoot;
    private bool isShooting;
    public bool doOnce;
    public bool doOnce2;
    public bool doOnce3;

    // Start is called before the first frame update
    void Start()
    {
        bullet = bulletTemplate.GetComponent<Bullet>();
        bullet.damage = damage;
        bullet.speed = bulletSpeed;
        bullet.direction = transform.forward;

        rig = weaponBarrelModel.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        

        if(isShooting)
        {
            doOnce2 = false;

            barrelTorqueTimer += Time.deltaTime * barrelSpeedMultiplier;

            if(barrelTorqueTimer >= maxBarrelSpeed)
            {
                barrelTorqueTimer = maxBarrelSpeed;
            }

            if(barrelTorqueTimer >= barrelFireTime)
            {
                if (canShoot)
                {
                    Shoot();
                    if(!doOnce)
                    {
                        animator.SetBool("isFiring",true);
                        doOnce = true;
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
        }
        else
        {
            doOnce = false;

            if(!doOnce2)
            {
                animator.SetBool("isFiring", false);
                doOnce2 = true;
            }

            barrelTorqueTimer -= Time.deltaTime * barrelSpeedMultiplier * 3;

            if (barrelTorqueTimer <= 0)
            {
                barrelTorqueTimer = 0;
                animator.SetTrigger("Idle");
            }
        }


        weaponBarrelModel.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, barrelTorqueTimer * Time.deltaTime));

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

        Debug.Log("X " + bulletObject.transform.localPosition.x);
        Debug.Log("Y " + bulletObject.transform.localPosition.y);
        float xDispersion = Random.Range(-0.2f, 0.2f);
        float yDispersion = Random.Range(-0.12f, 0.12f);

        Vector3 dispersion = new Vector3(xDispersion, 0, 0);

        bulletObject.transform.position += dispersion;

        bulletObject.SetActive(true);
        
        //rig.AddRelativeTorque(new Vector3(0, 0, 10));
        //rig.AddTorque(new Vector3(0,0,10));
    }
}
