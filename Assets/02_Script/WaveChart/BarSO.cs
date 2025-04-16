using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bar")]
public class BarSO : ScriptableObject
{
    [SerializeField] private EnemyType One_Beat;
    [SerializeField] private EnemyType Two_Beat;
    [SerializeField] private EnemyType Three_Beat;
    [SerializeField] private EnemyType Four_Beat;
    [SerializeField] private EnemyType Five_Beat;
    [SerializeField] private EnemyType Six_Beat;
    [SerializeField] private EnemyType Seven_Beat;
    [SerializeField] private EnemyType Eight_Beat;
    [SerializeField] private EnemyType Nine_Beat;
    [SerializeField] private EnemyType Ten_Beat;
    [SerializeField] private EnemyType Eleven_Beat;
    [SerializeField] private EnemyType Twelve_Beat;
    [SerializeField] private EnemyType Thirteen_Beat;
    [SerializeField] private EnemyType Fourteen_Beat;
    [SerializeField] private EnemyType Fifteen_Beat;
    [SerializeField] private EnemyType Sixteen_Beat;

    public EnemyType GetEnemyType(int index)
    {
        switch (index)
        {
            case 0: return One_Beat;
            case 1: return Two_Beat;
            case 2: return Three_Beat;
            case 3: return Four_Beat;
            case 4: return Five_Beat;
            case 5: return Six_Beat;
            case 6: return Seven_Beat;
            case 7: return Eight_Beat;
            case 8: return Nine_Beat;
            case 9: return Ten_Beat;
            case 10: return Eleven_Beat;
            case 11: return Twelve_Beat;
            case 12: return Thirteen_Beat;
            case 13: return Fourteen_Beat;
            case 14: return Fifteen_Beat;
            case 15: return Sixteen_Beat;
            default: return EnemyType.None_0;
        }
    }
}
