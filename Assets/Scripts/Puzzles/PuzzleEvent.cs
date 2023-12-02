namespace Puzzle
{
    class PuzzleEvent : PuzzleState
    {
        PuzzleEvent();

        public event Action OnPuzzleInit;
        public event Action OnPuzzleSolved;
        public event Action OnPuzzleFailed;


        public void InitPuzzle()
        {
            OnPuzzleInit?.Invoke();s

        public void PuzzleSolved()
        {
            OnPuzzleSolved?.Invoke();
        }

        public void PuzzleFailed(){
            OnPuzzleFailed?.Invoke();
        }
    }
}
