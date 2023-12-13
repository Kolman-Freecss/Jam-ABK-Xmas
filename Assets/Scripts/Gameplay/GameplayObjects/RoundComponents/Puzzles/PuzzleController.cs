using UnityEngine;
using UnityEngine.Events;

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

        #region Events
        public UnityEvent onPuzzleSolved = new UnityEvent();
        public UnityEvent onPuzzleFailed = new UnityEvent();
        public UnityEvent onPuzzleOnProgress = new UnityEvent();
        
        #endregion


        public PuzzleController()
        {
            this.CurrentProgress = 0;
            this.CurrentSteps = 0;
            this.puzzleEvent = new PuzzleEvent(PuzzleStates.INITIAL);
            this.puzzleEvent.OnPuzzleSolved += () => OnPuzzleChanged(PuzzleStates.SOLVED);
            this.puzzleEvent.OnPuzzleFailed += () => OnPuzzleChanged(PuzzleStates.UNSOLVED);
            this.puzzleEvent.OnPuzzleOnProgress += () => OnPuzzleChanged(PuzzleStates.ONPROGRESS);
        }

        /// <summary>
        /// The function "OnPuzzleChanged" initializes a new PuzzleState object with the given state
        /// parameter.
        /// </summary>
        /// <param name="PuzzleStates">PuzzleStates is an enumeration type that represents different
        /// states of a puzzle. It is used as the default value for the state parameter in the
        /// OnPuzzleChanged method.</param>
        public void OnPuzzleChanged(PuzzleStates state = PuzzleStates.INITIAL)
        {
            puzzleState = new PuzzleState(state);
        }

        public void OnPuzzleSolved(){
            onPuzzleSolved?.Invoke();
        }

        public void OnPuzzleFailed(){
            onPuzzleFailed?.Invoke();
        }

        /// <summary>
        /// The function calculates the progress percentage based on the current progress and total
        /// steps.
        /// </summary>
        /// <returns>
        /// The method is returning a float value representing the progress percentage.
        /// </returns>
        public float GetProgressPercentage()
        {
            return (float)CurrentProgress / TotalSteps * 100;
        }

        public abstract void OnPuzzleInteract();

       /// <summary>
       /// The function updates the progress of a puzzle, either by initializing it or by updating it
       /// based on whether the current step is correct or not.
       /// </summary>
       /// <param name="isStepCorrect">A boolean value indicating whether the current step in the puzzle
       /// is correct or not.</param>
        public virtual void PuzzleProgress(bool isStepCorrect){
            if (isFirstStep)
            {
                isFirstStep = false;
                OnPuzzleChanged(PuzzleStates.ONPROGRESS);
                onPuzzleOnProgress?.Invoke();
            } else {
                UpdateProgress(isStepCorrect);
            }
        }

        /// <summary>
        /// The function "UpdateProgress" handles the progress update based on whether the step is
        /// correct or incorrect.
        /// </summary>
        /// <param name="isStepCorrect">A boolean value indicating whether the step is correct or
        /// not.</param>
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

        /// <summary>
        /// The function increments the current progress and checks if the puzzle is solved, then
        /// triggers events accordingly.
        /// </summary>
        private void HandleCorrectStep()
        {
            CurrentProgress++;
            if (CurrentProgress == TotalSteps)
            {
                OnPuzzleChanged(PuzzleStates.SOLVED);
                onPuzzleSolved?.Invoke();
            }
        }

        /// <summary>
        /// The function increments the current steps and checks if it has reached the maximum number of
        /// steps allowed, triggering a puzzle failure if so.
        /// </summary>
        private void HandleIncorrectStep()
        {
            CurrentSteps++;
            if (CurrentSteps >= StepsToFail)
            {
                OnPuzzleChanged(PuzzleStates.UNSOLVED);
                onPuzzleFailed?.Invoke();
            }
        }
    }
}
