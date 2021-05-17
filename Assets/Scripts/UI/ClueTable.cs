using System;
using UnityEngine;
using UnityEngine.UI;

public class ClueTable : MonoBehaviour
{
    private Card[] cards;

    private Card character;

    private Card tool;

    private Card room;

    Toggle[] toggles;

    public void Awake()
    {
        cards = GetComponentsInChildren<Card>();
        toggles = GetComponentsInChildren<Toggle>();
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].CardChosen += OnCardChosen;
        }
    }

    public void SetLeftCards(Card[] left)
    {
        if (left == null)
        {
            return;
        }
        toggles[left[0].GetId()].isOn = true;
        toggles[left[0].GetId()].interactable = false;
        toggles[left[1].GetId()].isOn = true;
        toggles[left[1].GetId()].interactable = false;
        toggles[left[2].GetId()].isOn = true;
        toggles[left[2].GetId()].interactable = false;
    }

    public void SetRightVersion(GameObject[] rightVersion)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].interactable = false;
            toggles[i].isOn = true;
        }
        toggles[rightVersion[0].GetComponent<Card>().GetId()].isOn = false;
        toggles[rightVersion[1].GetComponent<Card>().GetId()].isOn = false;
        toggles[rightVersion[2].GetComponent<Card>().GetId()].isOn = false;
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
