using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    void Start()
    {
    }

    //when the target is hit
    private void OnCollisionEnter(Collision collision)
    {
        //if the collider is the projectile
        if (collision.collider.CompareTag("CannonBall"))
        {
            //increment the target count
            WristUIController.IncrementCountBy(1);
            //destroy the target
            Destroy(gameObject);
        }
    }
}
