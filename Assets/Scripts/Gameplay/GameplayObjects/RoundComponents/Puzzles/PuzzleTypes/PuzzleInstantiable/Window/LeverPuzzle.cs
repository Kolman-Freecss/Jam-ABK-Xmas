using System.Collections.Generic;
using Puzzle;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LeverPuzzle : PuzzleController
{
    [FormerlySerializedAs("correctSwitchPositions")]
    [Header("Puzzle Variables")]
    [SerializeField]
    List<bool> correctLeverPositions = new List<bool>();
    List<bool> playerLeverPositions = new List<bool>();
    Toggle[] checks;

    void Awake()
    {
        checks = GetComponentsInChildren<Toggle>();
        // Initialize player switch positions
        for (int i = 0; i < correctLeverPositions.Count; i++)
        {
            Debug.Log(i);
            playerLeverPositions.Add(false);
        }

        base.TotalSteps = 0;
        for (int i = 0; i < correctLeverPositions.Count; i++)
        {
            if (correctLeverPositions[i])
            {
                base.TotalSteps++;
            }
        }
        base.StepsToFail = TotalSteps * 2;
    }

    public LeverPuzzle() : base()
    {
        
    }

    public void ToggleSwitch(int switchIndex)
    {
        Debug.Log("Player before actualization" + playerLeverPositions.Count);
        // Toggle the switch position
        playerLeverPositions[switchIndex] = !playerLeverPositions[switchIndex];
        if (switchIndex + 1 < playerLeverPositions.Count)
        {
            Debug.Log("checks" + checks.Length);
            Debug.Log("player" + playerLeverPositions.Count);
            playerLeverPositions[switchIndex + 1] = !playerLeverPositions[switchIndex + 1];
            checks[switchIndex + 1].isOn = playerLeverPositions[switchIndex + 1];
        }

        if (switchIndex - 1 > 0)
        {
            playerLeverPositions[switchIndex - 1] = !playerLeverPositions[switchIndex - 1];
            checks[switchIndex - 1].isOn = playerLeverPositions[switchIndex - 1];
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
