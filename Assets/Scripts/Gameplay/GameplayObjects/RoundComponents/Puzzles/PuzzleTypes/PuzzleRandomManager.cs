using System.Collections;
using System.Collections.Generic;
using Puzzle;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleRandomManager : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> puzzleList = new List<GameObject>();
        List<GameObject> puzzleActive = new List<GameObject>();
        public static PuzzleRandomManager Instance { get; set; }


        public void SelectPuzzle(){
            int randomIndex = Random.Range(0, puzzleList.Count);
            puzzleActive.Add(Instantiate(puzzleList[randomIndex]));
        }

        void Update(){
            
        }
    }
}
