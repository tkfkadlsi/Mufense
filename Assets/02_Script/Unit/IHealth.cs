using System.Collections;
using UnityEngine;

public enum Debuffs
{
    None = 0,
    Stun = 1,
    Poison = 2,

}

public interface IHealth
{
    public float HP {  get; set; }
    public HPSlider HPSlider { get; set; }
    public void Hit(float damage, int debuff = 0);
    public void Die();
}
