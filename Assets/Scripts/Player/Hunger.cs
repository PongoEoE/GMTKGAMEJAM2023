using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Hunger : MonoBehaviour
{


    //https://www.youtube.com/watch?v=hsJs7dvzgMM

  [SerializeField] private float numEaten;
  [SerializeField] private float numHooked;  

  [Header("Hunger")]
  [SerializeField] private float maxHunger = 100f;
  [SerializeField] private float hungerRate = 1f;
  [SerializeField] float currentHunger;
  public float hungerPercent => currentHunger / maxHunger;

  [Header("Stamina")]
  [SerializeField] private float maxStamina = 100f;
  [SerializeField] private float staminaRate = 1f;
  [SerializeField] private float staminaRecharge = 2f;
  [SerializeField] private float staminaRechargeDelay = 1f;
  private float currentStamina;
  private float currentStaminaDelayCounter;
  public float staminaPercent => currentStamina / maxStamina;

  [Header("Player References")]
  //[SerializeField] private StarterAssetsInputs playerInput;

  public static UnityAction PlayerDeath;


  private void Start() 
  {
    {
        currentHunger = maxHunger;
        currentStamina = maxStamina;
    }
  }

  private void Update() 
  {
    //change max hunger & hunger rate to change how long you will last
    currentHunger -= hungerRate * Time.deltaTime;

    if (currentHunger <= 0)
    {
        PlayerDeath.Invoke();
        currentHunger = 0;
    }
  }

  public float getHunger(){
    return this.currentHunger;
  }

  public void setHunger(float add)
  {
    eaten();
    if (currentHunger + add > 100)
    {
      currentHunger = 100f;
    } else {
      currentHunger += add;
    }
    
  }

  public void eaten()
  {
    numEaten++;
  }

  public void hooked()
  {
    numHooked++;
  }
}
