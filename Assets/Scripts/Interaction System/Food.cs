using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour, IInteractable
{

    public GameObject player;
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    //[SerializeField] float currentHunger = player.getComponent<Hunger>().getHunger();

    public bool Interact (Interactor interactor)
    {
        Debug.Log(message:"Food ate");
        GameObject.Destroy(gameObject);

        return true;
    }


    public void SetHunger(float hunger)
    {
        //hunger += ;

    }

}
