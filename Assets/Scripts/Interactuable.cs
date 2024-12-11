using System;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactuable : MonoBehaviour
{
    private void Reset()
    {
        this.GetComponent<Collider>().enabled = true;
    }

    public abstract void Interact();
}