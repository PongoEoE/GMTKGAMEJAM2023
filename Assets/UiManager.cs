using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RawImage hungerUI;
    [SerializeField] private RawImage staminaUI;
    private Hunger hunger;
    void Start()
    {
        hunger = GameObject.FindGameObjectWithTag("Player").GetComponent<Hunger>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetHunger(hunger.getHunger());
    }

    public void SetHunger(float hunger) {
        hungerUI.rectTransform.sizeDelta = new Vector2((hunger/100) * 250f, 25f);
    }

    public void SetStamina(float stamina) {
        staminaUI.rectTransform.sizeDelta = new Vector2((stamina/100) * 250f, 25f);
    }
}
