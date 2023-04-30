using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageThrowing : MonoBehaviour
{
    [SerializeField] private float throwStrenght = 30f;
    [SerializeField] private float coolDownTimer = 1f;
    [SerializeField] private GameObject package;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform packageSpawnPoint;

    private float timer;

    void Start()
    {

    }

    void Update()
    {
        if (timer >= coolDownTimer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ThrowPackage();
                timer = 0;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void ThrowPackage()
    {
        Vector3 spawnPosition = packageSpawnPoint.position;
        GameObject newPackage = Instantiate(package, spawnPosition, Quaternion.identity);
        Rigidbody packageRigidBody = newPackage.GetComponent<Rigidbody>();

        Vector3 forceDirection = Vector3.Scale(cameraTransform.transform.forward, new Vector3(throwStrenght, throwStrenght, throwStrenght));
        packageRigidBody.AddForce(forceDirection, ForceMode.Impulse);
    }
}
