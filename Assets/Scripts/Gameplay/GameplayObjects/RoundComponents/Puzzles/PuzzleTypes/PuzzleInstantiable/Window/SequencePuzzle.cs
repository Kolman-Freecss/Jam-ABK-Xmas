using System.Collections.Generic;
using System.Linq;
using Puzzle;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SequencePuzzle : PuzzleController
{
    [Header("General Variables")]
    [SerializeField]
    int stepsToFail;

    [SerializeField]
    int totalSteps;

    [Header("Puzzle Variables")]
    [SerializeField]
    int firstNumber;

    [SerializeField]
    TextMeshProUGUI text;
    TMP_InputField text1;

    List<int> correctSequence = new List<int>();
    List<int> playerSequence = new List<int>();

    private void Awake()
    {
        StepsToFail = stepsToFail;
        TotalSteps = 1;

        PuzzleProgress(true);
        firstNumber = Random.Range(1, 15);

        correctSequence.Add(firstNumber == 0 ? 1 : firstNumber);
        correctSequence = Enumerable.Range(1, StepsToFail).Select(x => Fibonacci(x, firstNumber)).ToList();
        text.text = "| ";
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (i != correctSequence.Count - 1)
            {
                text.text += correctSequence[i] + " | ";
            }
        }
    }


    public int Fibonacci(int n, int firstNumber)
    {
        if (n <= 1)
            return firstNumber;
        else
        {
            int fib = 0;
            int a = firstNumber;
            int b = firstNumber;
            for (int i = 2; i <= n; i++)
            {
                fib = a + b;
                a = b;
                b = fib;
            }
            return fib;
        }
    }

    public void EnterNumber()
    {
        text1 = GetComponentInChildren<TMP_InputField>();
        try
        {
            playerSequence.Add(int.Parse(text1.text));
            foreach (int i in correctSequence)
            {
                Debug.Log(i);
            }
            Debug.Log(
                "Player sequence" + playerSequence[0] + "Correct sequence" + correctSequence[correctSequence.Count - 1]
            );
            UpdateProgress(playerSequence[playerSequence.Count - 1] == correctSequence[correctSequence.Count - 1]);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    public override void OnPuzzleInteract()
    {
        throw new System.NotImplementedException();
    }
}
