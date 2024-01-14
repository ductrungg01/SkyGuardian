using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private Transform parentTransform;
    
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log($"{this.name} I'm collide with {other.name}");

        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentTransform;
        
        Destroy(this.gameObject);
    }
}
