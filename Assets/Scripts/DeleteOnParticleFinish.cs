using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnParticleFinish : MonoBehaviour
{
    public ParticleSystem particleSystem;
    
    //wait for the particle system to finish, then destroy the game object
    void Update()
    {
        if (particleSystem)
        {
            //wait for no particles to be alive
            if (!particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
