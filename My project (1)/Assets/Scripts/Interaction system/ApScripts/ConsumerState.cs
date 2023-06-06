using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerState : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    //For the player
    //When the player is in range of the object that holds this script and they press the interaction button (E) the following code block gets executed
    public bool Interact(Interactor interactor)
    {

        if (_prompt == "that")
        {
           
        }
        else
        {

        }
        return true;
    }

    //For the AI
    //The AI uses the same logic but instead of it having to press a button
    //I made it so it checks for a minimum distance to the object and if met then execute the code in the block below
    //This can be seen in the AI script
    public bool InteractAI(littleTroublemakerMS interactor)
    {
        if (_prompt == "that")
        {

        }
        else
        {

        }
        return true;
    }
}
