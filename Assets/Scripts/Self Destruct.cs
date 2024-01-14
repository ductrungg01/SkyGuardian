using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float timeTillDestruct = 5.0f;
    void Start()
    {
        Destroy(this, timeTillDestruct);
    }
}
