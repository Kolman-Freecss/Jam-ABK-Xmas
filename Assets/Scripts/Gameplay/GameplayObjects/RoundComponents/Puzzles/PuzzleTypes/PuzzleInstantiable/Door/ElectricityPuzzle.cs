using System;
using System.Collections.Generic;
using System.Linq;
using Puzzle;
using UnityEngine;

public class ElectricityPuzzle : PuzzleController
{
    [Header("Puzzle Variables")]
    [SerializeField]
    List<bool> correctSwitchPositions = new List<bool>();
    List<bool> playerSwitchPositions = new List<bool>();

    void Awake()
    {
        // Initialize player switch positions
        for (int i = 0; i < correctSwitchPositions.Count; i++)
        {
            playerSwitchPositions.Add(false);
        }
        TotalSteps = 0;
        for (int i = 0; i < correctSwitchPositions.Count; i++)
        {
            if (correctSwitchPositions[i])
            {
                TotalSteps++;
            }
        }

        StepsToFail = correctSwitchPositions.Count / 2;
        Debug.Log("Puzzle steps: " + TotalSteps);
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
        Debug.Log("Puzzle progress: " + isStepCorrect);
        UpdateProgress(isStepCorrect);
    }

    public override void HandleCorrectStep()
    {
        if (correctSwitchPositions.SequenceEqual(playerSwitchPositions))
        {
            OnPuzzleChanged(PuzzleStates.SOLVED);
            onPuzzleSolved?.Invoke(houseController);
        }
    }

    public override void OnPuzzleInteract()
    {
        throw new System.NotImplementedException();
    }
}
