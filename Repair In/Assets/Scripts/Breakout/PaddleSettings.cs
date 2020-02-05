using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ2020/Breakout/PaddleSettings")]
public class PaddleSettings : ScriptableObject
{
    [SerializeField] private float speed;

    public float GetSpeed()
    {
        return speed;
    }
}
