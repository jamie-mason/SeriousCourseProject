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
    [SerializeField] private GameObject doorPrompt;
    bool canInteract = false;
    bool canFinish = false;
    bool canExit = false;
    bool exited = false;
    bool isInteracting = false;
    int numberOfCluesInteractedWith;
    private GameObject interactedObject;
    private Interactable[] interactables;
    private List<Interactable> interactedObjects;
    [SerializeField] private GameObject exitDoorScene;
    int countCompleted;

    [Header("Clues")]
    [SerializeField] private GameObject xrayClue;
    [SerializeField] private GameObject clipboardClue;
    [SerializeField] private GameObject patientChartClue;
    [SerializeField] private GameObject posterClue;
    [SerializeField] private GameObject vapeDeviceClue;
    [SerializeField] private GameObject microscopeClue;





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
            if (interactableGameobjects[i] != null)
            {
                if (interactableGameobjects[i].GetComponent<ExtraTags>() != null)
                {
                    if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Xray")
                    {

                        interactables[i] = new Xray(interactableGameobjects[i], xrayClue);

                    }
                    else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Clipboard")
                    {
                        interactables[i] = new Clipboard(interactableGameobjects[i], clipboardClue);
                    }
                    else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "VapeDevice")
                    {
                        interactables[i] = new VapeDevice(interactableGameobjects[i], vapeDeviceClue);
                    }
                    else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Poster")
                    {
                        interactables[i] = new Poster(interactableGameobjects[i], posterClue);
                    }
                    else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "Microscope")
                    {
                        interactables[i] = new Microscope(interactableGameobjects[i], microscopeClue);
                    }
                    else if (interactableGameobjects[i].GetComponent<ExtraTags>().InteractableTypeTag == "PatientChart")
                    {
                        interactables[i] = new PatientChart(interactableGameobjects[i], patientChartClue);
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
        }

        trasformScaleRelativeToViewport = new TrasformScaleRelativeToViewport();
        canInteract = false;
        canExit = false;
        canFinish = false;
        isInteracting = false;
        exited = false;
        numberOfCluesInteractedWith = 0;
        interactedObjects = new List<Interactable>();
        microscopeClue.SetActive(false);
        xrayClue.SetActive(false);
        posterClue.SetActive(false);
        patientChartClue.SetActive(false);
        vapeDeviceClue.SetActive(false);
        clipboardClue.SetActive(false);
        doorPrompt.SetActive(false);
        exitDoorScene.SetActive(false);
        countCompleted = 0;


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
                    Interact();

                }
                else
                {
                    isInteracting = false;
                    EndInteraction();

                }
            }
            else
            {
                isInteracting = false;
            }
            if (canExit)
            {
                InitiateGameExit();
            }
        }
        if (!isInteracting && !exited)
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

        for (int i = 0; i < interactables.Length; i++)
        {
            if (interactedObject != null)
            {
                if (interactables[i].checkIfInteractedObject(interactedObject))
                {

                    interactables[i].EnableClue();

                    break;
                }
            }
            else
            {
                break;
            }
        }


    }
    private void EndInteraction()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            if (interactedObject != null)
            {
                if (interactables[i].checkIfInteractedObject(interactedObject))
                {
                    if (interactables[i].getInteracted() == false)
                    {
                        interactedObjects.Add(interactables[i]);
                    }
                    interactables[i].DisableClue();

                    break;
                }
            }
            else
            {
                break;
            }
        }
        CanFinish();
    }
    void CanFinish()
    {
        countCompleted = 0;
        if (!canFinish) {
            for (int i = 0; i < interactables.Length; i++)
            {
                for (int j = 0; j < interactedObjects.Count; j++)
                {
                    if (interactedObjects[j] == interactables[i])
                    {
                        countCompleted++;
                        break;
                    }
                }
            }
            if(countCompleted == interactables.Length)
            {
                canFinish = true;
                Debug.Log("Can now Finish");
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "InteractableObjects")
        {
            interactedObject = collision.gameObject;
            canInteract = true;
        }
        if (collision.tag == "Exit")
        {
            if (canFinish)
            {
                if(!doorPrompt.activeSelf){
                    doorPrompt.SetActive(true);
                }
                canExit = true;
            }
        }
    }
    private void InitiateGameExit()
    {
        Debug.Log("Exited Game");
        exited = true;
        if(!exitDoorScene.activeSelf){
            exitDoorScene.SetActive(true);
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
        if (collision.tag == "Exit")
        {
            if(doorPrompt.activeSelf){
                doorPrompt.SetActive(false);
            }
            canExit = false;
        }

    }
}
