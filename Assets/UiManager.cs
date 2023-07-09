using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RawImage hungerUI;
    [SerializeField] private RawImage staminaUI;
    private Hunger hunger;

    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    public GameObject fishin;
    void Start()
    {
        hunger = GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetHunger(hunger.getHunger());
        SetStamina(hunger.getStamina());
    }

    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        timerText.text = currentTime.ToString("0.00");
    }
    public void SetHunger(float hunger) {
        hungerUI.rectTransform.sizeDelta = new Vector2((hunger/100) * 250f, 25f);
    }

    public void SetStamina(float stamina) {
        staminaUI.rectTransform.sizeDelta = new Vector2((stamina/100) * 250f, 15f);
    }

    public void activateFishing()
    {
        fishin.SetActive(true);
        fishin.GetComponent<FishingMinigame>().lived();
    }




}
