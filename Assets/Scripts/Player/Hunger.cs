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
  private float currentRechargeDelay = 0f;
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
        //PlayerDeath.Invoke();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Die();
        currentHunger = 0;
    }

    currentRechargeDelay += Time.deltaTime;
    if(currentRechargeDelay >= staminaRechargeDelay && currentStamina < maxStamina) {
      currentStamina += staminaRecharge*Time.deltaTime;
      currentStamina = Mathf.Min(currentStamina, maxStamina);
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

    public void setStamina (float add)
  {
    currentStamina += add;
    if (add < 0f) {
      currentRechargeDelay = 0f;
    }
  }

  public float getStamina(){
    return currentStamina;
  }

  public void eaten()
  {
    numEaten++;
  }

  public void hooked()
  {
    numHooked++;
  }

  public float getFoodEaten(){
    return numEaten;
  }

  public float getTimesHooked(){
    return numHooked;
  }
}
