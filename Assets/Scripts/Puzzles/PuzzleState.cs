namespace Puzzle
{

    public class PuzzleState
    {
        PuzzleStates state;

        PuzzleState(PuzzleStates state)
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
