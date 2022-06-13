using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;
    private AudioSource _AudioSource;

    public float range = 100f;
    public int bulletsPerMag = 30;
    public int bulletsLeft = 200;

    public int currentBullets;

    public Transform shootPoint;
    public ParticleSystem muzzleFlash;
    public AudioClip shootSound;

    public float fireRate = 0.1f;

    float fireTimer;

    private bool isReloading;

    void Start()
    {
        anim = GetComponent<Animator>();
        _AudioSource = GetComponent<AudioSource>();

        currentBullets = bulletsPerMag;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (currentBullets > 0)
                Fire();
            else if(bulletsLeft > 0)
                DoReload();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(currentBullets < bulletsPerMag && bulletsLeft > 0)
            DoReload();
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;
    }

    void FixedUpdated()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        isReloading = info.IsName("Reload");
        //if (info.IsName("Fire")) anim.SetBool("Fire", false);
    }

    private void Fire()
    {
        if (fireTimer < fireRate || currentBullets <=0  || isReloading)
            return;

        RaycastHit hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + " found! ");
        }

        anim.CrossFadeInFixedTime("Fire", 0.01f);
        muzzleFlash.Play();
        PlayShootSound();

        //anim.SetBool("Fire", true);
        currentBullets--;
        fireTimer = 0.0f;
    }

    public void Reload()
    {
        if (bulletsLeft <= 0) return;

        int bulletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
    }

    private void DoReload()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (isReloading) return;

        anim.CrossFadeInFixedTime("Reload", 0.01f);
    }

    private void PlayShootSound()
    {
        _AudioSource.PlayOneShot(shootSound);
        //_AudioSource.clip = shootSound;
       // _AudioSource.Play();
    }
}
