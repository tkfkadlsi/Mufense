public enum EnemyType
{
    None_0 = 0,
    Normal_1 = 1,
    Blink_2 = 2,
    Cancled_3 = 3,
    HP_UP_10 = 10,
}

public struct EnemyNote
{
    public EnemyType type;
    public float timing;
}
