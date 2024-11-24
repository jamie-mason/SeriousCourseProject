using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    TrasformScaleRelativeToViewport trasformScaleRelativeToViewport;
    [SerializeField] private GameObject interactionPrompt;
    bool canInteract = false;
    bool isInteracting = false;
    private GameObject interactedObject;
    private Interactable[] interactables;


    [SerializeField] private GameObject[] interactableGameobjects;


    private void Start()
    {
        if (MovementSpeed < 0f)
        {
            MovementSpeed = 1f;
        }
        interactables = new Interactable[interactableGameobjects.Length];
        for (int i = 0; i < interactableGameobjects.Length; i++)
        {
            if (interactableGameobjects[i].GetComponent<ExtraTags>() != null)
            {
                if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Xray")
                {
                    interactables[i] = new Xray(interactableGameobjects[i]);
                }
                else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Clipboard")
                {
                    interactables[i] = new Clipboard(interactableGameobjects[i]);
                }
                else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "VapeDevice")
                {
                    interactables[i] = new VapeDevice(interactableGameobjects[i]);
                }
                else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Poster")
                {
                    interactables[i] = new Poster(interactableGameobjects[i]);
                }
                else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Microscope")
                {
                    interactables[i] = new Microscope(interactableGameobjects[i]);
                }
                else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "PatientChart")
                {
                    interactables[i] = new PatientChart(interactableGameobjects[i]);
                }
                else
                {
                    interactables[i] = new Interactable(interactableGameobjects[i]);
                }
            }
            else
            {
                interactables[i] = new Interactable(interactableGameobjects[i]);
            }
        }

        trasformScaleRelativeToViewport = new TrasformScaleRelativeToViewport();
        canInteract = false;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canInteract)
            {
                if (!isInteracting)
                {
                    isInteracting = true;
                }
                else
                {
                    isInteracting = false;
                }
            }
            else
            {
                isInteracting = false;
            }
        }
        if (!isInteracting)
        {
            if (trasformScaleRelativeToViewport == null)
            {
                trasformScaleRelativeToViewport = new TrasformScaleRelativeToViewport();
            }
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(horizontalInput, verticalInput);

            if (direction.sqrMagnitude > 0.2f)
            {
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion characterTargetRotation = Quaternion.Euler(0f, 0f, targetAngle);
                transform.rotation = characterTargetRotation;

            }
            Vector3 newPosition = transform.position + (Vector3)(direction * MovementSpeed * Time.deltaTime);
            newPosition = KeepInViewport(newPosition);

            transform.position = newPosition;
            Vector3 targetRotation;


            if (transform.eulerAngles.z < 180 && transform.eulerAngles.z >= 0)
            {
                targetRotation = new Vector3(0, 0, -90);
            }
            else
            {
                targetRotation = new Vector3(0, 0, 90);
            }
            interactionPrompt.transform.localEulerAngles = targetRotation;
            setInteractPromptActive();
        }
        else
        {
            interactionPrompt.GetComponent<Canvas>().enabled = false;
            Interact();

        }

    }

    void setInteractPromptActive()
    {
        interactionPrompt.GetComponent<Canvas>().enabled = canInteract;
    }
    private Vector3 KeepInViewport(Vector3 position)
    {
        var size = trasformScaleRelativeToViewport.getViewportSize(Camera.main, GetComponent<SpriteRenderer>());
        // Get the camera's viewport boundaries (from 0 to 1)
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);

        // Clamp the viewport position to the range of 0 to 1
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0f + size.x / 2f, 1f - size.x / 2f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0f + size.y / 2f, 1f - size.y / 2f);

        // Convert the clamped viewport position back to world space
        return Camera.main.ViewportToWorldPoint(viewportPos);


    }

    private void Interact()
    {

        Interactable temp;

        foreach (Interactable interactable in interactables)
        {
            if (interactedObject != null)
            {
                if (interactable.checkIfInteractedObject(interactedObject))
                {
                    if(interactable is Xray)
                    {
                        temp = new Xray(interactable);
                        break;
                    }
                    else if (interactable is Clipboard)
                    {
                        temp = new Clipboard(interactable);
                        break;
                    }
                    else if (interactable is PatientChart)
                    {
                        temp = new PatientChart(interactable);
                        break;
                    }
                    else if (interactable is Poster)
                    {
                        temp = new Poster(interactable);
                        break;
                    }
                    else if (interactable is Microscope)
                    {
                        temp = new Microscope(interactable);
                        break;
                    }
                    else if (interactable is VapeDevice)
                    {
                        temp = new VapeDevice(interactable);
                        break;
                    }
                    else
                    {
                        temp = new Interactable(interactable);
                        interactable.Interacted();
                        isInteracting = false;
                        return;
                    }
                }
            }

        }

    }

    bool CanFinish()
    {
        foreach(Interactable interactable in interactables)
        {
            if(interactable.getInteracted() == false)
            {
                return false;
            }
        }
        return true;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "InteractableObjects")
        {
            interactedObject = collision.gameObject;
            canInteract = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "InteractableObjects")
        {
            interactedObject = null;

            canInteract = false;
        }

    }
}
