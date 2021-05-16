using UnityEngine;
using UnityEngine.Events;

public class UIInputReciever : InputReciever
{
    [SerializeField] UnityEvent onClick;

    public override void OnInputRecieved()
    {
        foreach (var handler in inputHandlers)
        {
            handler.ProcessInput(new Vector3(), gameObject, () => onClick.Invoke());
        }
    }
}
