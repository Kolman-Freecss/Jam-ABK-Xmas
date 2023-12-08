namespace Puzzle
{
    public class PuzzleState
    {
        public PuzzleStates state { get; set; }
        

        public PuzzleState(PuzzleStates state)
        {
            this.state = state;
        }

        
    }

    public enum PuzzleStates
    {
        INITIAL,
        ONPROGRESS,
        SOLVED,
        UNSOLVED
    }
}
