using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCam : MonoBehaviour
{
    [SerializeField] private Transform playerAimCamera;

    private void SetCameraVisibility()
    {
        playerAimCamera.gameObject.SetActive(!isPlayerAimed());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetCameraVisibility();
        }
    }

    private bool isPlayerAimed()
    {
        return playerAimCamera.gameObject.activeSelf;
    }
}
