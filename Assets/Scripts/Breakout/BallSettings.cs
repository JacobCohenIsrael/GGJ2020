using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ2020/Breakout/BallSettings")]
public class BallSettings : ScriptableObject
{
    [SerializeField] private Vector2 initialForce;

    public Vector2 GetInitialForce()
    {
        return initialForce;
    }
}
