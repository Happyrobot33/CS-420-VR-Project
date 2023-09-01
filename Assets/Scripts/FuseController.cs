using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseController : MonoBehaviour
{
    //fuse trigger collider
    Collider fuseTrigger;

    //delay before cannon ball is fired
    public float fuseDelay = 1f;

    //particle system to turn on for fuse
    ParticleSystem fuseParticles;

    //audio source to play for fuse
    AudioSource fuseAudio;

    //Cannon controller to fire
    public CannonController cannonController;

    // Start is called before the first frame update
    void Start()
    {
        //get the fuse trigger collider
        fuseTrigger = GetComponent<Collider>();

        //get the fuse particles
        fuseParticles = GetComponentInChildren<ParticleSystem>();

        //get the fuse audio
        fuseAudio = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update() { }

    //detect when a torch enters the fuse trigger
    void OnTriggerEnter(Collider other)
    {
        //check if the other collider is a torch
        if (other.CompareTag("Torch"))
        {
            //start the fuse
            StartFuse();
        }
    }

    void StartFuse()
    {
        //turn on the fuse particles
        fuseParticles.Play();

        //play the fuse audio
        fuseAudio.Play();

        //start the fuse
        StartCoroutine(Fuse());

        //disable the fuse trigger
        fuseTrigger.enabled = false;
    }

    void StopFuse()
    {
        //turn off the fuse particles
        fuseParticles.Stop();

        //stop the fuse audio
        fuseAudio.Stop();

        //enable the fuse trigger
        fuseTrigger.enabled = true;
    }

    IEnumerator Fuse()
    {
        //wait for the fuse delay
        yield return new WaitForSeconds(fuseDelay);

        //fire the cannon
        cannonController.FireCannon();

        //stop the fuse
        StopFuse();
    }
}
