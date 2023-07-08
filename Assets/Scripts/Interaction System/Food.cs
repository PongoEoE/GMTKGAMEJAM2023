using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    [SerializeField] private RectTransform Ping;

    //[SerializeField] float currentHunger = player.getComponent<Hunger>().getHunger();
    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate() {
        if(Vector3.Distance(transform.position, player.position) < 25) {
            Ping.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            Ping.gameObject.SetActive(true);
        } else {
            Ping.gameObject.SetActive(false);
        }
    }

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
