using System.Collections.Generic;
using Puzzle;
using UnityEngine;

public class ElectricityPuzzle : PuzzleController
{
    [Header("Puzzle Variables")]
    [SerializeField]
    List<bool> correctSwitchPositions = new List<bool>();
    List<bool> playerSwitchPositions = new List<bool>();

    public ElectricityPuzzle() : base()
    {
        base.StepsToFail = correctSwitchPositions.Count;
        base.TotalSteps = correctSwitchPositions.Count;

        // Initialize player switch positions
        for (int i = 0; i < correctSwitchPositions.Count; i++)
        {
            playerSwitchPositions.Add(false);
        }
    }

    public void ToggleSwitch(int switchIndex)
    {
        // Toggle the switch position
        playerSwitchPositions[switchIndex] = !playerSwitchPositions[switchIndex];

        // Check if the puzzle is solved
        PuzzleProgress(playerSwitchPositions[switchIndex] == correctSwitchPositions[switchIndex]);
    }

    public override void PuzzleProgress(bool isStepCorrect)
    {
        base.UpdateProgress(isStepCorrect);
    }

    public override void OnPuzzleInteract()
    {
        throw new System.NotImplementedException();
    }
}