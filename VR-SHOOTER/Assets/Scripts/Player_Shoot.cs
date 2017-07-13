using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{

    #region Atributos

    public int shotgunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform shotgunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotgunDuration = new WaitForSeconds(0.07f);
    private AudioSource shotgunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    private RaycastHit hit;

    #endregion

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        shotgunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            laserLine.SetPosition(0, shotgunEnd.position);
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        shotgunAudio.Play();
        laserLine.enabled = true;
        yield return shotgunDuration;
        laserLine.enabled = false;
    }
}
