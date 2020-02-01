using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifetime = 5f;

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
