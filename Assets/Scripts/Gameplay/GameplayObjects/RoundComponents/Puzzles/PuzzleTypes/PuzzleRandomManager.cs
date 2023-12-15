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
        GameObject puzzleActive;
        int electricityPuzzleCount = 0;
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
            if (puzzleActive == null)
            {
                int randomIndex = Random.Range(0, puzzleList.Count);
                GameObject puzzleRandom;
                if (puzzleList[randomIndex].name.Contains("Electricity"))
                {
                    electricityPuzzleCount++;
                    switch (electricityPuzzleCount)
                    {
                        case 1:
                            puzzleRandom = Instantiate(puzzleList[1], canvas.transform);
                            break;
                        case 2:
                            puzzleRandom = Instantiate(puzzleList[2], canvas.transform);
                            break;
                        case 3:
                            puzzleRandom = Instantiate(puzzleList[3], canvas.transform);
                            break;
                        default:
                            puzzleRandom = Instantiate(puzzleList[randomIndex], canvas.transform);
                            break;
                    }
                }
                else
                {
                    puzzleRandom = Instantiate(puzzleList[randomIndex], canvas.transform);
                }
                puzzleActive = puzzleRandom;
                return puzzleRandom;
            }
            return null;
        }

        public void DestroyPuzzle(GameObject puzzle)
        {
            puzzleActive = null;
            Destroy(puzzle);
        }
    }
}
