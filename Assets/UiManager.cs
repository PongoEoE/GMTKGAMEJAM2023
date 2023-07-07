using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RawImage hungerUI;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHunger(float hunger) {
        hungerUI.rectTransform.sizeDelta = new Vector2((hunger/100) * 250f, 25f);
    }
}
