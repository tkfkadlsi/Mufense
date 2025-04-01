public enum EnemyType
{
    None,
    Normal,
    Blink,
    Cancled,
}

public struct EnemyNote
{
    public EnemyType type;
    public float timing;
}
