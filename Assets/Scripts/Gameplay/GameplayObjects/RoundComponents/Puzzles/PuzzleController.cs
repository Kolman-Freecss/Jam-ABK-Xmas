using UnityEngine;

namespace Puzzle
{
    public abstract class PuzzleController : MonoBehaviour
    {
        private PuzzleEvent puzzleEvent;
        public PuzzleState puzzleState { get; set; }
        public int CurrentProgress { get; set; }
        public int TotalSteps { get; set; }
        public int StepsToFail { get; set; }
        public int CurrentSteps { get; set; }
        public bool isFirstStep = true;

        public PuzzleController()
        {
            this.CurrentProgress = 0;
            this.CurrentSteps = 0;
            this.puzzleEvent = new PuzzleEvent(PuzzleStates.INITIAL);
            this.puzzleEvent.OnPuzzleSolved += () => OnPuzzleChanged(PuzzleStates.SOLVED);
            this.puzzleEvent.OnPuzzleFailed += () => OnPuzzleChanged(PuzzleStates.UNSOLVED);
            this.puzzleEvent.OnPuzzleOnProgress += () => OnPuzzleChanged(PuzzleStates.ONPROGRESS);
        }

        public void OnPuzzleChanged(PuzzleStates state = PuzzleStates.INITIAL)
        {
            puzzleState = new PuzzleState(state);
        }

        public float GetProgressPercentage()
        {
            return (float)CurrentProgress / TotalSteps * 100;
        }

        public abstract void OnPuzzleInteract();

        public virtual void PuzzleProgress(bool isStepCorrect){
            if (isFirstStep)
            {
                isFirstStep = false;
                OnPuzzleChanged(PuzzleStates.ONPROGRESS);
            } else {
                UpdateProgress(isStepCorrect);
            }
        }

        public void UpdateProgress(bool isStepCorrect)
        {
            if (isStepCorrect)
            {
                HandleCorrectStep();
            }
            else
            {
                HandleIncorrectStep();
            }
        }

        private void HandleCorrectStep()
        {
            CurrentProgress++;
            if (CurrentProgress == TotalSteps)
            {
                OnPuzzleChanged(PuzzleStates.SOLVED);
            }
        }

        private void HandleIncorrectStep()
        {
            CurrentSteps++;
            if (CurrentSteps >= StepsToFail)
            {
                OnPuzzleChanged(PuzzleStates.UNSOLVED);
            }
        }
    }
}
