using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public delegate void CardChoice(Card card);

public enum CardType
{
    Room, Tool, Character
}

[RequireComponent(typeof(UIInputReciever))]
[RequireComponent(typeof(CardInputHandler))]
public class Card : Button
{
    public CardChoice CardChosen;

    private InputReciever reciever;

    [SerializeField] private int id;
    [SerializeField] private new string name;
    [SerializeField] private CardType type;

    protected override void Awake()
    {
        reciever = GetComponent<UIInputReciever>();
        onClick.AddListener(() => reciever.OnInputRecieved());
    }

    public void SetTableSelectionState(bool isSelected)
    {
        Debug.Log(name + " is selected = " + isSelected);
        if (isSelected)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public int GetId()
    {
        return id;
    }

    public CardType GetCardType()
    {
        return type;
    }

    public string GetName()
    {
        return name;
    }

    public void OnCardSelected()
    {
        CardChosen?.Invoke(this);
    }

    public override bool Equals(object other)
    {
        if (other.GetType() != typeof(Card))
        {
            return false;
        }
        return id == ((Card)other).GetId();
    }

    public override int GetHashCode()
    {
        return id;
    }
}
