using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool Interact (Interactor interactor)
    {
        Debug.Log(message:"Food ate");
        GameObject.Destroy(gameObject);

        return true;
    }


}
