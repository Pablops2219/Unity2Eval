using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpObjects pickUpScript = other.GetComponentInParent<PickUpObjects>();
            if (pickUpScript)
            {
                pickUpScript.ObjectToPickUp = this.gameObject;
                pickUpScript.ShowInteractionMessage(true); // Muestra el mensaje
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpObjects pickUpScript = other.GetComponentInParent<PickUpObjects>();
            if (pickUpScript)
            {
                pickUpScript.ObjectToPickUp = null;
                pickUpScript.ShowInteractionMessage(false); // Oculta el mensaje al alejarse
            }
        }
    }
}
