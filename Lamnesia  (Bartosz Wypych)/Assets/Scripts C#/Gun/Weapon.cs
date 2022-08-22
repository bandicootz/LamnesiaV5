using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator anim;
    private AudioSource _AudioSource;

    [Header("Properties")]
    public float range = 100f;
    public int bulletsPerMag = 30;
    public int bulletsLeft = 200;

    public int currentBullets;

    public enum ShootMode { Auto, Semi }
    public ShootMode shootingMode;

    public bool canUse = true;

    [Header("UI")]
    public Text ammoText;

    [Header("Setup")]
    public Transform shootPoint;
    public GameObject hitParticles;
    public GameObject bulletImpact;
    public LineRenderer bulletTrail;

    public ParticleSystem muzzleFlash;

    [Header("Sound Effects")]
    public AudioClip shootSound;

    public float fireRate = 0.1f;
    public float damage = 20f;

    float fireTimer;

    private bool isReloading;
    private bool isAiming;
    private bool shootInput;

    private Vector3 originalPosition;

    [Header("ADS")]
    public Vector3 aimPosition;
    public float aodSpeed = 8f;

    public float spreadFactor = 0.1f;

    private float time = 0f;

    void OnEnable()
    {
        UpdateAmmoText();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        _AudioSource = GetComponent<AudioSource>();

        currentBullets = bulletsPerMag;
        originalPosition = transform.localPosition;

        UpdateAmmoText();
    }

    void Update()
    {
        if (time < 30)
            time += Time.deltaTime;

        if (time >= 0)
        {
            switch (shootingMode)
            {
                case ShootMode.Auto:
                    shootInput = Input.GetButton("Fire1");
                    break;

                case ShootMode.Semi:
                    shootInput = Input.GetButtonDown("Fire1");
                    break;
            }
            if (canUse)
            {
                if (shootInput)
                {
                    if (currentBullets > 0)
                        Fire();
                    else if (bulletsLeft > 0)
                        DoReload();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (currentBullets < bulletsPerMag && bulletsLeft > 0)
                        DoReload();
                }

                if (fireTimer < fireRate)
                    fireTimer += Time.deltaTime;

                AimDownSights();
            }
        }
    }

    void FixedUpdated()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        isReloading = info.IsName("Reload");
        anim.SetBool("Aim", isAiming);
        //if (info.IsName("Fire")) anim.SetBool("Fire", false);
    }

    private void AimDownSights()
    {
        if (Input.GetButton("Fire2") && !isReloading)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aodSpeed);
            isAiming = true;
        }

        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * aodSpeed);
            isAiming = false;
        }
    }

    private void Fire()
    {
        if (fireTimer < fireRate || currentBullets <=0  || isReloading)
            return;

        RaycastHit hit;

        Vector3 shootDirection = shootPoint.transform.forward;
        shootDirection = shootDirection + shootPoint.TransformDirection
        (new Vector3(Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor)));
        //shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
        //shootDirection.y += Random.Range(-spreadFactor, spreadFactor);

        if (Physics.Raycast(shootPoint.position, shootDirection, out hit, range))
        {
            Debug.Log(hit.transform.name + " found! ");

            GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

            Destroy(hitParticleEffect, 2f);
            Destroy(bulletHole, 3f);

            if (hit.transform.GetComponent<HealthController>())
            {
                hit.transform.GetComponent<HealthController>().ApplyDamage(damage);
            }

            SpawnBulletTrail(hit.point);
        }

        anim.CrossFadeInFixedTime("Fire", 0.01f);
        muzzleFlash.Play();
        PlayShootSound();

        //anim.SetBool("Fire", true);
        currentBullets--;

        if (canUse)
        {
            UpdateAmmoText();
        }

        fireTimer = 0.0f;
    }

    private void SpawnBulletTrail(Vector3 hitPoint)
    {
        GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, shootPoint.position, Quaternion.identity);

        LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();

        lineR.SetPosition(0, shootPoint.position);
        lineR.SetPosition(1, hitPoint);

        Destroy(bulletTrailEffect, 1f);
    }

    public void Reload()
    {
        if (bulletsLeft <= 0) return;

        int bulletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;

        UpdateAmmoText();
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

    private void UpdateAmmoText()
    {
        ammoText.text = currentBullets + "/" + bulletsLeft;
    }
}