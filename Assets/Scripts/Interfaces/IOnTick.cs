using UnityEngine;

public interface IOnTick
{
    /// <summary>
    /// Called every tick
    /// </summary>
    void OnTick(int tickNumber);
}
