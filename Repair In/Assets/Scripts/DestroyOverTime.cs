using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    public float DestroyTime = 1f;

    private void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
