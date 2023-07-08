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
        Vector3 ppos = player.position;
        ppos.y = 0f;
        Vector3 tpos = transform.position;
        tpos.y = 0f;

        if(Vector3.Distance(tpos, ppos) < 15f) {
            line.SetActive(true);
        } else {
            line.SetActive(false);
        }
    }
}
