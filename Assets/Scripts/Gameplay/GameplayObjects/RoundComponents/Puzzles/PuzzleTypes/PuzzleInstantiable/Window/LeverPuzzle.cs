using System.Collections.Generic;
using Puzzle;
using UnityEngine;
using UnityEngine.Serialization;

public class LeverPuzzle : PuzzleController
{
    [FormerlySerializedAs("correctSwitchPositions")]
    [Header("Puzzle Variables")]
    [SerializeField]
    List<bool> correctLeverPositions = new List<bool>();
    List<bool> playerLeverPositions = new List<bool>();

    public LeverPuzzle() : base()
    {
        base.StepsToFail = correctLeverPositions.Count;
        base.TotalSteps = correctLeverPositions.Count;

        // Initialize player switch positions
        for (int i = 0; i < correctLeverPositions.Count; i++)
        {
            playerLeverPositions.Add(false);
        }
    }

    public void ToggleSwitch(int switchIndex)
    {
        // Toggle the switch position
        playerLeverPositions[switchIndex] = !playerLeverPositions[switchIndex];
        if (switchIndex + 1 < playerLeverPositions.Count)
        {
            playerLeverPositions[switchIndex + 1] = !playerLeverPositions[switchIndex + 1];
        }

        if (switchIndex - 1 > 0)
        {
            playerLeverPositions[switchIndex - 1] = !playerLeverPositions[switchIndex - 1];
        }

        // Check if the puzzle is solved
        PuzzleProgress(playerLeverPositions[switchIndex] == correctLeverPositions[switchIndex]);
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
