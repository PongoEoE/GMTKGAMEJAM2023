using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI demise;
    public TextMeshProUGUI time;
    public TextMeshProUGUI food;
    public TextMeshProUGUI hooked;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Hunger hunger = GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>();
        time.text = "Time Alive: " + Mathf.Round(GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>().currentTime).ToString();
        food.text = "Food Eaten: " + hunger.getFoodEaten().ToString();
        hooked.text = "Hooks Escaped: " + (hunger.getTimesHooked()-1f).ToString();
        GameObject.FindGameObjectWithTag("UiManager").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDemise(string text) {
        demise.text = text;
    }
}
