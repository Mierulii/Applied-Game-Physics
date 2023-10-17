using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    public bool freeze = false;
    [SerializeField] Rigidbody[] ragdolls;
    [SerializeField] Collider[] colliders;


    void Start()
    {
        ragdolls = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    // Enables/Disables the ragdoll effect
    public void SetRagdollEnabled(bool enabled)
    {
        // Freeze/Unfreeze the rigidbodies
        foreach (Rigidbody rb in ragdolls)
        {
            rb.isKinematic = enabled;
        }

        /*
         * Enables/Disables the colliders; WIP
        foreach (Collider coll in colliders)
        {
            coll.enabled = !enabled;
        }
        */
    }

    private void Update()
    {
        SetRagdollEnabled(freeze);
    }
}