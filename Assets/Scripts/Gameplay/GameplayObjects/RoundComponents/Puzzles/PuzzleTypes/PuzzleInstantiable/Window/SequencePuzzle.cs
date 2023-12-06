using System.Collections.Generic;
using System.Linq;
using Puzzle;
using Unity.VisualScripting;
using UnityEngine;

public class SequencePuzzle : PuzzleController
{
    [Header("General Variables")]
    [SerializeField]
    int totalSteps;

    [SerializeField]
    int stepsToFail;

    [Header("Puzzle Variables")]
    [SerializeField]
    int firstNumber;

    List<int> correctSequence = new List<int>();
    List<int> playerSequence = new List<int>();

    void Start()
    {
        StepsToFail = stepsToFail;
        TotalSteps = totalSteps;

        PuzzleProgress(true);
        firstNumber = Random.Range(1, 15);

        // Generate the Fibonacci sequence
        if (firstNumber == 0 || firstNumber == 1)
            correctSequence.Add(1);
        correctSequence.Add(firstNumber == 0 ? 1 : firstNumber);
        correctSequence = Enumerable.Range(1, totalSteps).Select(x => Fibonacci(x)).ToList();
        Debug.Log(correctSequence.Count);
    }

    void Update() { }

    public override void OnPuzzleInteract()
    {
        EnterNumber(1);
        if (puzzleState.state == PuzzleStates.SOLVED)
        {
            Destroy(gameObject);
        }
    }

    private static int Fibonacci(int n)
    {
        return n <= 1 ? n : Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public void EnterNumber(int number)
    {
        playerSequence.Add(number);
        foreach (int i in correctSequence)
        {
            Debug.Log(i);
        }
        PuzzleProgress(playerSequence[playerSequence.Count - 1] == correctSequence[playerSequence.Count - 1]);
    }
}
