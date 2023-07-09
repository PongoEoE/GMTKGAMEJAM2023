using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour, IInteractable
{

    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    [SerializeField] private RectTransform Ping;
    [SerializeField] private bool isHooked;
    
    public FishingMinigame fishin;

    private FoodScatterer scatterer;

    public ParticleSystem particles;

    //[SerializeField] float currentHunger = player.getComponent<Hunger>().getHunger();
    private Transform player;
    private float offset;
    private MeshRenderer myMR;
    private AudioSource eatSFX;

    

    private void Start() {
        particles.transform.parent = null;
        transform.GetChild(0).localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        myMR = transform.GetChild(0).GetComponent<MeshRenderer>();
        scatterer = GameObject.FindGameObjectWithTag("Scatterer").GetComponent<FoodScatterer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = Random.Range(0f, 360f);
    }
    private void FixedUpdate() {
        if(Vector3.Distance(transform.position, player.position) < 25 && Mathf.Sin(Time.time*4f)>0.5f && myMR.isVisible) {
            Ping.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            Ping.gameObject.SetActive(true);
        } else {
            Ping.gameObject.SetActive(false);
        }
    }

    private void Update() {
        transform.position += Vector3.up*Mathf.Sin(Time.time+offset)*Time.deltaTime*0.8f;
    }

    public bool Interact (Interactor interactor)
    {
        eatSFX = GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetComponent<AudioSource>();
        eatSFX.Play();

        Debug.Log(message:"Food ate");
        
        Hunger hunger = GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>();
        //Food gives 25% with every food
        hunger.setHunger(25f);
        hunger.setStamina(15f);
        if(isHooked){
            hunger.hooked();
            

            UiManager uifish = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
            uifish.activateFishing();
            

            Debug.Log("Ouch");
            //do stuff
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Catch();
        }
        particles.transform.position = transform.position;
        particles.Play();
        scatterer.Recycle(gameObject);

        return true;
    }
}
