using UnityEngine;
using UnityEngine.UI;

public class ClueTableItem : Button
{
    private InputReciever reciever;

    protected override void Awake()
    {
        reciever = GetComponent<UIInputReciever>();
        onClick.AddListener(() => reciever.OnInputRecieved());
    }
}
