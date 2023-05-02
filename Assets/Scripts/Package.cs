using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] AudioClip packagehit;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(packagehit);
        Instantiate(crashParticle, collision.GetContact(0).point, Quaternion.identity);
    }
}
