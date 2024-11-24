using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable
{
    protected GameObject InteractableObject;
    protected bool hasInteracted;

    public Interactable(GameObject interactableObject)
    {
        this.InteractableObject = interactableObject;
        hasInteracted = false;
    }
    public Interactable(Interactable interactable)
    {
        this.InteractableObject = interactable.InteractableObject;
        this.hasInteracted = interactable.hasInteracted;
    }

    public void Interacted()
    {
        hasInteracted = true;
    }
    public void resetInteracted()
    {
        hasInteracted = false;
    }
    public bool getInteracted()
    {
        return hasInteracted;
    }
    public GameObject getInteractableGameObject()
    {
        return InteractableObject;
    }
    public bool checkIfInteractedObject(GameObject obj)
    {
        if (obj == InteractableObject)
        {
            return true;
        }

        return false;
    }
}

public class Xray : Interactable
{
    public Xray(GameObject interactableObject) : base(interactableObject)
    {

    }
    public Xray(Interactable xray) : base(xray)
    {

    }
    public Xray(Xray xray) : base(xray)
    {

    }
    private void completed(bool status)
    {
        status = hasInteracted;
    }
    public void EnableClue()
    {
        completed(true);

    }
}

public class Clipboard : Interactable
{
    public Clipboard(GameObject interactableObject) : base(interactableObject)
    {

    }
    public Clipboard(Interactable clipboard) : base(clipboard)
    {

    }
    public Clipboard(Clipboard clipboard) : base(clipboard)
    {

    }
    private void completed(bool status)
    {
        status = hasInteracted;
    }
    public void EnableClue()
    {
        completed(true);

    }
}
public class Poster : Interactable
{
    public Poster(GameObject interactableObject) : base(interactableObject)
    {

    }
    public Poster(Interactable poster) : base(poster)
    {

    }
    public Poster(Poster poster) : base(poster)
    {

    }
    private void completed(bool status)
    {
        status = hasInteracted;
    }
    public void EnableClue()
    {
        completed(true);

    }
}

public class Microscope : Interactable
{
    public Microscope(GameObject interactableObject) : base(interactableObject)
    {

    }
    public Microscope(Interactable microscope) : base(microscope)
    {

    }
    public Microscope(Microscope microscope) : base(microscope)
    {

    }
    private void completed(bool status)
    {
        status = hasInteracted;
    }
    public void EnableClue()
    {
        completed(true);

    }
}

public class PatientChart : Interactable
{
    public PatientChart(GameObject interactableObject) : base(interactableObject)
    {

    }
    public PatientChart(Interactable patientChart) : base(patientChart)
    {

    }
    public PatientChart(PatientChart patientChart) : base(patientChart)
    {

    }
    private void completed(bool status)
    {
        status = hasInteracted;
    }
    public void EnableClue()
    {
        completed(true);
    }
}

public class VapeDevice : Interactable
{
    public VapeDevice(GameObject interactableObject) : base(interactableObject)
    {

    }
    public VapeDevice(Interactable vapeDevice) : base(vapeDevice)
    {

    }
    public VapeDevice(VapeDevice vapeDevice) : base(vapeDevice)
    {

    }
    private void completed(bool status)
    {
        status = hasInteracted;
    }
    public void EnableClue()
    {
        completed(true);

    }
}
