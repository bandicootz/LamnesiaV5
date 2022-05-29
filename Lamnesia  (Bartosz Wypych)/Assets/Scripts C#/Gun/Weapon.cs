using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;

    public float range = 100f;
    public int bulletsPerMag = 30;
    public int bulletsLeft = 200;

    public int currentBullets;

    public Transform shootPoint;

    public float fireRate = 0.1f;

    float fireTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentBullets = bulletsPerMag;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;
    }

    void FixedUpdated()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("Fire")) anim.SetBool("Fire", false);
    }

    private void Fire()
    {
        if (fireTimer < fireRate) return;

        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + " found! ");
        }

        anim.CrossFadeInFixedTime("Fire", 0.01f);
        //anim.SetBool("Fire", true);
        currentBullets--;
        fireTimer = 0.0f;
    }
}
