using System;
using UnityEngine;

public class BoardInputHandler : MonoBehaviour, IInputHandler
{
    private Board board;

    public void Awake()
    {
        board = GetComponent<Board>();
    }

    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        Vector3 cell = Convert(inputPosition);
        board.OnCellSelected(cell);
    }

    private Vector3 Convert(Vector3 position)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(worldPosition.x, worldPosition.y, 0);
    }
}
