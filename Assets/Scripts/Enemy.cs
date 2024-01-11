using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{this.name} I'm collide with {other.name}");
        Destroy(this.gameObject);
    }
}
