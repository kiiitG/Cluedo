using UnityEngine;

public abstract class InputReciever : MonoBehaviour
{
    protected IInputHandler[] inputHandlers;

    public abstract void OnInputRecieved();

    public void Awake()
    {
        inputHandlers = GetComponents<IInputHandler>();
    }
}
