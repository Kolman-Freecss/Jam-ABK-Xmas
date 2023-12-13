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

        [SerializeField]
        GameObject canvas;
        List<GameObject> puzzleActive = new List<GameObject>();
        public static PuzzleRandomManager Instance { get; set; }

        void Awake()
        {
            ManageSingleton();
        }
        private void ManageSingleton()
        {
            if (Instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public GameObject SelectPuzzle()
        {
            int randomIndex = Random.Range(0, puzzleList.Count);
            GameObject puzzleRandom = Instantiate(puzzleList[randomIndex], canvas.transform);
            puzzleActive.Add(puzzleRandom);
            return puzzleRandom;
        }

        public void DestroyPuzzle(GameObject puzzle)
        {
            puzzleActive.Remove(puzzle);
            Destroy(puzzle);
        }
    }
}
