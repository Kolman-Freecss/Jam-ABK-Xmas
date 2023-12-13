using System.Collections.Generic;
using Puzzle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WiresPuzzle : PuzzleController
{
    public WiresPuzzle() : base()
    {
        //base.StepsToFail = correctSwitchPositions.Count;
        //base.TotalSteps = correctSwitchPositions.Count;

        // Initialize player switch positions
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
