public class Define
{
    public static float XBoundary = 5f;
    public static UnityEngine.Vector3 LeftCornerPosition = new UnityEngine.Vector3(-5f, 1.25f, 0);
    public static UnityEngine.Vector3 RightCornerPosition = new UnityEngine.Vector3(5f, 1.25f, 0);
    public static UnityEngine.LayerMask TouchLayer = 1 << 9;

    public enum GameState : byte
    {
        Ready,
        Play,
        End,
    }

    public enum SoundType : byte
    {
        Bgm,
        MaxCount
    }

    public enum BlockType : byte
    {
        Pingpong,
        None
    }

    public enum ColorType : byte
    {
        Red,
        Blue
    }

    public enum PingpongType : byte
    {
        Plus,
        Multiply,
        None
    }
}
