namespace Puzzle
{
    public class PuzzleController
    {
        private PuzzleEvent puzzleEvent;
        private PuzzleState puzzleState;

        public PuzzleController(PuzzleEvent puzzleEvent)
        {
            this.puzzleEvent = puzzleEvent;
            this.puzzleEvent.OnPuzzleInit += OnPuzzleInitialized;
            this.puzzleEvent.OnPuzzleSolved += () => OnPuzzleChanged(PuzzleStates.SOLVED);
            this.puzzleEvent.OnPuzzleFailed += () => OnPuzzleChanged(PuzzleStates.UNSOLVED);
        }

        private void OnPuzzleInitialized()
        {
            puzzleState = new PuzzleState(PuzzleStates.INITIAL);
        }

        private void OnPuzzleChanged(PuzzleStates state)
        {
            puzzleState = new PuzzleState(state);
        }
    }
}
