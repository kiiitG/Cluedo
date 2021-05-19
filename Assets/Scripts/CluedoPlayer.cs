using UnityEngine;

interface CluedoPlayer
{
    void RollTheDice();
    void GoOnCell(Vector3Int cell, string cellType);
    void MakeSuggestion();
    void MakeAccusation();
    void GoThroughPassage();
    void Answer();
}
