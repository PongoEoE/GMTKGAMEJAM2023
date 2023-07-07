using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpSpin : MonoBehaviour
{
    // Start is called before the first frame update
    private float addTime;
    private float spinSpeed;
    void Start()
    {
        addTime = Random.Range(0, 0.5f);
        spinSpeed = Random.Range(0.8f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = transform.localEulerAngles + Vector3.up*Mathf.PerlinNoise(Time.time+ addTime, 0.5f*(Time.time+addTime))*Time.deltaTime*75f*spinSpeed;
    }
}
