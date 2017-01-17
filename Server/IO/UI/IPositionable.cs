namespace Server.IO.UI
{
    public interface IPositionable
    {
        int Top { get; }
        int Bottom { get; }
        int Left { get; }
        int Right { get; }
    }
}