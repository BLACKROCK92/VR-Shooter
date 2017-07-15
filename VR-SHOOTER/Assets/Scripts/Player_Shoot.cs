using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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

    private float nextFire;
    private RaycastHit hit;
    private SteamVR_TrackedController controller;
    private ParticleSystem Fire;

    #endregion

    void Start()
    {

        shotgunAudio = FindObjectOfType<AudioSource>();
        fpsCam = FindObjectOfType<Camera>();
        controller = transform.parent.parent.GetComponent<SteamVR_TrackedController>();
        Fire = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void Update()
    {

        if (controller.triggerPressed && Time.time > nextFire)
        {
            Fire.Play();
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            //laserLine.SetPosition(0, shotgunEnd.position);
            if (Physics.Raycast(rayOrigin, (shotgunEnd.transform.right) * -1, out hit, weaponRange))
            {

                //laserLine.SetPosition(1, hit.point);
            }
            else
            {
                //laserLine.SetPosition(1, rayOrigin + ((shotgunEnd.transform.right*-1) * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        shotgunAudio.Play();
        print("ShotgunAudio");
        //laserLine.enabled = true;
        yield return shotgunDuration;
        //laserLine.enabled = false;
    }

}
