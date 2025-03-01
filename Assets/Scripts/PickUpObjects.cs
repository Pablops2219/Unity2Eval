using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObjects : MonoBehaviour
{

    public GameObject ObjectToPickUp;
    public GameObject interactionUI;
    public GameObject imagePanel;
    public RawImage objectImage;

    void Start()
    {
        if (interactionUI != null)
    {
        interactionUI.SetActive(false);
    }
    if (imagePanel != null)
    {
        imagePanel.SetActive(false);
    }
    }

    void Update()
    {
        if (ObjectToPickUp != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUp(ObjectToPickUp);
        }
    }

    void PickUp(GameObject obj)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();

        if (meshRenderer != null && meshRenderer.material.mainTexture != null)
        {
            objectImage.texture = meshRenderer.material.mainTexture;
            imagePanel.SetActive(true);
        }

        Destroy(obj);
        interactionUI.SetActive(false);
    }

    public void ShowInteractionMessage(bool show)
    {
        interactionUI.SetActive(show);
    }
}
