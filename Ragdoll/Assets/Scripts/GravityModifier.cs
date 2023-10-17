using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityModifier : MonoBehaviour
{
    public float gravityStrength = 9.807f;
    
    void Start()
    {
        // Modifies the gravity
        Physics.gravity = new Vector3(0.0f, -gravityStrength, 0.0f);
    }
}