using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log($"{this.name} --Collide with-- {other.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.name} **Trigger with** {other.gameObject.name}");
    }
}
