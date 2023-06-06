using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Tutorial : https://www.youtube.com/watch?v=THmW4YolDok&ab_channel=DanPos

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    //array that holds all of the instances in the hitbox area that are on layer Interactable (i.e. interactable objects)
    private readonly Collider[] _colliders = new Collider[3];

    //Number of found interactable objects in the hitbox area
    [SerializeField] private int _numFound;

    //Refference to the interactable constructor inside the script of an interactable object
    private IInteractable _interactable;

    private void Update()
    {
        //Hitbox in a shape of a sphere that detects the interactable object
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            //Check if interactable exists
            if (_interactable != null)
            {
                //Check if the prompt ui is displaying, if not display prompt
                if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);

                //Check for a key press (E) and execute the Interact code block in the objects script.
                if (Keyboard.current.eKey.wasPressedThisFrame) _interactable.Interact(this);
            }
        }
        else
        {
            //Reset values to avoid Refference Errors
           if (_interactable != null) _interactable = null;
           if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
        }

    }

    //For debugging only, draws the outline of the interaction hitbox sphere
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
