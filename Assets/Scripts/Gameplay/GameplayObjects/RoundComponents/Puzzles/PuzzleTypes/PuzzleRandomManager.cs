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


        public GameObject SelectPuzzle(){
            int randomIndex = Random.Range(0, puzzleList.Count);
            GameObject puzzleRandom = Instantiate(puzzleList[randomIndex]);
            puzzleActive.Add(puzzleRandom);
            return puzzleRandom;
        }

        void Update(){
            
        }
    }
}
