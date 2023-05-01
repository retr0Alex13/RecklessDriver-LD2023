using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    [SerializeField] ParticleSystem crashParticle;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(crashParticle, collision.GetContact(0).point, Quaternion.identity);
    }
}
