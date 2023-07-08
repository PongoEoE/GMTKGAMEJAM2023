using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScatterer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] food;
    [SerializeField] GameObject[] hooks;
    [SerializeField] int ammountNormal;
    [SerializeField] int ammountHooked;
    void Start()
    {
        for (int i = 0; i < ammountNormal; i++)
        {
            Vector2 pos = Random.insideUnitCircle*270;
            Vector3 finalPos = new Vector3(pos.x, Random.Range(-2f, 30f), pos.y);
            if(!Physics.CheckSphere(finalPos, 1f)) {
                GameObject currentFood = GameObject.Instantiate(food[Random.Range(0,1)], finalPos, Quaternion.identity);
                currentFood.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
            } else {
                i--;
            } 
        }

        for (int i = 0; i < ammountHooked; i++)
        {
            Vector2 pos = Random.insideUnitCircle*270;
            Vector3 finalPos = new Vector3(pos.x, Random.Range(-2f, 30f), pos.y);
            if(!Physics.CheckSphere(finalPos, 1f)) {
                GameObject currentFood = GameObject.Instantiate(hooks[Random.Range(0,1)], finalPos, Quaternion.identity);
                currentFood.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
            } else {
                i--;
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
