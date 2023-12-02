namespace Puzzle
{
    public class PuzzleState
    {
        PuzzleStates state;

        public PuzzleState(PuzzleStates state)
        {
            this.state = state;
        }
    }

    public enum PuzzleStates
    {
        INITIAL,
        SOLVED,
        UNSOLVED
    }
}
