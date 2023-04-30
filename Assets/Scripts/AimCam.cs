using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCam : MonoBehaviour
{
    [SerializeField] private Transform playerAimCamera;
    [SerializeField] private GameObject crossHair;

    private void SetCameraVisibility()
    {
        playerAimCamera.gameObject.SetActive(!isPlayerAimed());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetCameraVisibility();
            crossHair.SetActive(isPlayerAimed());
        }
    }

    public bool isPlayerAimed()
    {
        return playerAimCamera.gameObject.activeSelf;
    }
}
