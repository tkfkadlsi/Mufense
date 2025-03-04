using System.Collections;
using UnityEngine;

public interface IHealth
{
    public float HP { get; set; }

    public void Die(bool drop = false);
    public IEnumerator Hited();
}
