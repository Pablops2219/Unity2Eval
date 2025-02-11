using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("¡El Player ha tocado el objeto: " + gameObject.name + "!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("¡El Player ha entrado en el trigger de: " + gameObject.name + "!");
        }
    }
}
