
using UnityEngine;

public enum MazeDirection
{
   North,
   East,
   South,
   West
}

public static class MazeDirections
{
    public const int Count = 4;

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)RandomValue.Range(0, Count);
        }
    }

    public static IntVector2[] vector =
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0-1),
        new IntVector2(-1,0)
    };
    
    public static IntVector2 ToIntVector2(MazeDirection direction)
    {
        return vectors[(int)direction];
    }
}
