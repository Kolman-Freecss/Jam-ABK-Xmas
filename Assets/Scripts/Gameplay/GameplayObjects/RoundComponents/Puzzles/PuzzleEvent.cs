#region

using System;

#endregion

namespace Puzzle
{
    public class PuzzleEvent : PuzzleState
    {
        public PuzzleEvent(PuzzleStates state)
            : base(state) { }

        public event Action OnPuzzleInit;
        public event Action OnPuzzleSolved;
        public event Action OnPuzzleFailed;
        public event Action OnPuzzleOnProgress;

        public void InitPuzzle()
        {
            OnPuzzleInit?.Invoke();
        }

        public void PuzzleSolved()
        {
            OnPuzzleSolved?.Invoke();
        }

        public void PuzzleFailed()
        {
            OnPuzzleFailed?.Invoke();
        }

        public void PuzzleInProgress()
        {
            OnPuzzleOnProgress?.Invoke();
        }
    }
}
