using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    //[SerializeField] float currentHunger = player.getComponent<Hunger>().getHunger();

    public bool Interact (Interactor interactor)
    {
        Debug.Log(message:"Food ate");
        
        Hunger hunger = GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>();
        //Food gives 25% with every food
        hunger.setHunger(25f);
        GameObject.Destroy(gameObject);

        return true;
    }


    
}
