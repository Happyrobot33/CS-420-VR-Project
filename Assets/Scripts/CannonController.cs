using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    //prefab for cannon ball to be fired
    public GameObject cannonBallPrefab;

    //location to spawn fired cannon ball
    public Transform cannonBallSpawn;

    //force to apply to cannon ball
    public float cannonBallForce = 1000f;

    //audio effect for cannon firing
    public AudioSource cannonAudio;

    //particle effect for cannon firing
    public ParticleSystem cannonParticles;

    // Start is called before the first frame update
    void Start() 
    {
        // Assure that barrel end cannon particles don't play at the start:
        cannonParticles.Stop();
    }

    // Update is called once per frame
    void Update() { }

    public void FireCannon()
    {
        //instantiate the cannon ball
        GameObject cannonBall = Instantiate(
            cannonBallPrefab,
            cannonBallSpawn.position,
            cannonBallSpawn.rotation
        );

        //get the rigidbody of the cannon ball
        Rigidbody cannonBallRigidbody = cannonBall.GetComponent<Rigidbody>();

        //add force to the cannon ball
        cannonBallRigidbody.AddForce(cannonBallSpawn.forward * cannonBallForce);

        //play the cannon audio
        cannonAudio.Play();

        //stop the particles
        cannonParticles.Stop();
        //play the cannon particles
        cannonParticles.Play();
    }
}
