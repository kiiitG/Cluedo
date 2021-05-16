using System;
using UnityEngine;
using System.Collections.Generic;

public class ClueTable : MonoBehaviour
{
    private Card[] cards;

    private Card character;

    private Card tool;

    private Card room;

    public void Awake()
    {
        cards = GetComponentsInChildren<Card>();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].CardChosen += OnCardChosen;
        }
    }

    private void OnCardChosen(Card card)
    {
        if (card.GetCardType() == CardType.Character)
        {
            if (character != null)
            {
                character.SetTableSelectionState(false);
            }
            card.SetTableSelectionState(true);
            character = card;
        }
        else if (card.GetCardType() == CardType.Tool)
        {
            if (tool != null)
            {
                tool.SetTableSelectionState(false);
            }
            card.SetTableSelectionState(true);
            tool = card;
        }
        else if (card.GetCardType() == CardType.Room)
        {
            if (room != null)
            {
                room.SetTableSelectionState(false);
            }
            card.SetTableSelectionState(true);
            room = card;
        }
    }
 
    public int[] GetVersion()
    {
        if (character == null || tool == null || room == null)
        {
            print("one of the version values is null");
            throw new ArgumentException();
        }
        int[] res = new int[] { character.GetId(), tool.GetId(), room.GetId() };
        character.SetTableSelectionState(false);
        character = null;
        tool.SetTableSelectionState(false);
        tool = null;
        room.SetTableSelectionState(false);
        room = null;
        return res;
    }
}
