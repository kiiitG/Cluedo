using System;
using UnityEngine;

[RequireComponent(typeof(UIInputReciever))]
public class CardInputHandler : MonoBehaviour, IInputHandler
{
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        selectedObject.GetComponent<Card>().OnCardSelected();
    }
}
