using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hunger;

public class Food : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    [SerializeField] float currentHunger = Hunger.getHunger();

    public bool Interact (Interactor interactor)
    {
        Debug.Log(message:"Food ate");
        GameObject.Destroy(gameObject);

        return true;
    }


    public void SetHunger(float hunger)
    {
        hunger += ;

    }

}
