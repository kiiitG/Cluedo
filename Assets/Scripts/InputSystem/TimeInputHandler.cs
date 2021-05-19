using Photon.Pun;
using System;
using UnityEngine;

class TimeInputHandler : MonoBehaviour, IInputHandler
{
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        TimeManager.Nullify();
    }

    public void Update()
    {
        if (TimeManager.TimeOut())
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
