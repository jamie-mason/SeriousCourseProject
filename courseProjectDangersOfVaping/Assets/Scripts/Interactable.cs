using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable
{
    protected GameObject InteractableObject;
    protected bool hasInteracted;
    public Interactable()
    {
        this.InteractableObject = null;
        hasInteracted = false;
    }

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
    public virtual void EnableClue()
    {

    }
    public virtual void DisableClue()
    {
        Interacted();
    }


}

public class Xray : Interactable
{
    private GameObject clue;
    public Xray(GameObject interactableObject, GameObject clue) : base(interactableObject)
    {
        this.clue = clue;
    }
    public Xray(Interactable xray, GameObject clue) : base(xray)
    {
        this.clue = clue;
    }
    public Xray(Xray xray) : base(xray)
    {
        this.InteractableObject = xray.InteractableObject;
        this.hasInteracted = xray.hasInteracted;
        this.clue = xray.clue;
    }

    public override void EnableClue()
    {
        clue.SetActive(true);


    }
    public override void DisableClue()
    {
        clue.SetActive(false);
        Interacted();

    }
}

public class Clipboard : Interactable
{
    private GameObject clue;

    public Clipboard(GameObject interactableObject, GameObject clue) : base(interactableObject)
    {
        this.clue = clue;

    }
    public Clipboard(Interactable clipboard, GameObject clue) : base(clipboard)
    {
        this.clue = clue;

    }
    public Clipboard(Clipboard clipboard) : base(clipboard)
    {
        this.InteractableObject = clipboard.InteractableObject;
        this.hasInteracted = clipboard.hasInteracted;
        this.clue = clipboard.clue;
    }

    public override void EnableClue()
    {
        clue.SetActive(true);


    }
    public override void DisableClue()
    {
        clue.SetActive(false);
        Interacted();

    }
}
public class Poster : Interactable
{
    private GameObject clue;

    public Poster(GameObject interactableObject, GameObject clue) : base(interactableObject)
    {
        this.clue = clue;

    }
    public Poster(Interactable poster, GameObject clue) : base(poster)
    {
        this.clue = clue;

    }
    public Poster(Poster poster) : base(poster)
    {
        this.InteractableObject = poster.InteractableObject;
        this.hasInteracted = poster.hasInteracted;
        this.clue = poster.clue;
    }

    public override void EnableClue()
    {
        clue.SetActive(true);


    }
    public override void DisableClue()
    {
        clue.SetActive(false);
        Interacted();

    }
}

public class Microscope : Interactable
{
    private GameObject clue;

    public Microscope(GameObject interactableObject, GameObject clue) : base(interactableObject)
    {
        this.clue = clue;

    }
    public Microscope(Interactable microscope, GameObject clue) : base(microscope)
    {
        this.clue = clue;

    }
    public Microscope(Microscope microscope) : base(microscope)
    {
        this.InteractableObject = microscope.InteractableObject;
        this.hasInteracted = microscope.hasInteracted;
        this.clue = microscope.clue;
    }

    public override void EnableClue()
    {
        clue.SetActive(true);


    }
    public override void DisableClue()
    {
        clue.SetActive(false);
        Interacted();

    }
}

public class PatientChart : Interactable
{
    private GameObject clue;

    public PatientChart(GameObject interactableObject, GameObject clue) : base(interactableObject)
    {
        this.clue = clue;
    }
    public PatientChart(Interactable patientChart, GameObject clue) : base(patientChart)
    {
        this.clue = clue;

    }
    public PatientChart(PatientChart patientChart) : base(patientChart)
    {
        this.InteractableObject = patientChart.InteractableObject;
        this.hasInteracted = patientChart.hasInteracted;
        this.clue = patientChart.clue;

    }

    public override void EnableClue()
    {
        clue.SetActive(true);


    }
    public override void DisableClue()
    {
        clue.SetActive(false);
        Interacted();

    }
}

public class VapeDevice : Interactable
{
    private GameObject clue;

    public VapeDevice(GameObject interactableObject, GameObject clue) : base(interactableObject)
    {
        this.clue = clue;

    }
    public VapeDevice(Interactable vapeDevice, GameObject clue) : base(vapeDevice)
    {
        this.clue = clue;

    }
    public VapeDevice(VapeDevice vapeDevice, GameObject clue) : base(vapeDevice)
    {
        this.InteractableObject = vapeDevice.InteractableObject;
        this.hasInteracted = vapeDevice.hasInteracted;
        this.clue = vapeDevice.clue;
    }

    public override void EnableClue()
    {
        clue.SetActive(true);


    }
    public override void DisableClue()
    {
        clue.SetActive(false);
        Interacted();

    }
}