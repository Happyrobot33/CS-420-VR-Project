using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseController : MonoBehaviour
{
    // The flickering light for the fuse when sparked:
    public GameObject fuseLight;

    // The flicker light's Animator component to call the flicker state change:
    Animator flickerController;

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
        // Get the spark base light's animator:
        flickerController = fuseLight.GetComponent<Animator>();

        // Make the flickering fuse base light initially turned off:
        flickerController.enabled = false;
        fuseLight.SetActive(false);

        //get the fuse trigger collider
        fuseTrigger = GetComponent<Collider>();

        //get the fuse particles
        fuseParticles = GetComponentInChildren<ParticleSystem>();

        // Disable fuse particles from playing right at the start:
        fuseParticles.Stop();

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

        // play the flickering light
        fuseLight.SetActive(true);

        // enable the flicker and play the spark's light animation from the controller:
        flickerController.enabled = true;
        flickerController.Play("FlickerSparkLight");

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

        // stop the flickering light overall:
        flickerController.enabled = false;
        
        fuseLight.SetActive(false);

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
