using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFail : MonoBehaviour
{
    [SerializeField] LayerMask obstacleLayer;

    public delegate void PlayerFailAction();
    public static PlayerFailAction OnPlayerFail;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Use bitwise operation to find layermask instead of hardcoding
        if (hit.gameObject.layer == 7)
        {
            OnPlayerFail?.Invoke();
        }
    }

}
