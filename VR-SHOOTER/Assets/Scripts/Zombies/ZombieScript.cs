using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    //The target player
    public Transform player;
    //At what distance will the enemy walk towards the player?
    public float walkingDistance = 10.0f;
    //In what time will the enemy complete the journey between its position and the players position
    public float smoothTime = 10.0f;
    //Vector3 used to store the velocity of the enemy
    private Vector3 smoothVelocity = Vector3.zero;
    //Call every frame
    Animator _anim;

    public Collider atakZone;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        atakZone.enabled = false;
    }


    void Update()
    {
        //Look at the player
        transform.LookAt(player);
        //Calculate distance between player
        float distance = Vector3.Distance(transform.position, player.position);
        //If the distance is smaller than the walkingDistance
        if (distance < walkingDistance)
        {
            //print(distance);
            //Move the enemy towards the player with smoothdamp
            transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
        }

        if (distance < 2)
        {
            _anim.SetBool("atak", true);
            atakZone.enabled = true;
        }
        else
        {
            atakZone.enabled = false;
            _anim.SetBool("atak", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player"){
            print("HP--");
        }
    }
}
