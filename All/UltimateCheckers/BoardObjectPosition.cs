namespace UltimateCheckers
{
    public class BoardObjectPosition
    {
        public BoardPositionLine XPosition { get; internal set; }
        public BoardPositionLine YPosition { get; internal set; }

        public BoardObjectPosition(BoardPositionLine xPosition, BoardPositionLine yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }
}
