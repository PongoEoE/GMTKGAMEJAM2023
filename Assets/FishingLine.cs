using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    private GameObject line;
    void Start()
    {
        line = transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < 8) {
            line.SetActive(true);
        } else {
            line.SetActive(false);
        }
    }
}
