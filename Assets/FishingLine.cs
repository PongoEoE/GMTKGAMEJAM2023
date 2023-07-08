using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    private GameObject line;
    private LineRenderer fLine;
    void Start()
    {
        line = transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fLine = line.GetComponent<LineRenderer>();
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
            float lp = Mathf.InverseLerp(0, 15, Vector3.Distance(tpos, ppos));
            fLine.widthMultiplier = Mathf.Lerp(0.07f, 0, lp);
        } else {
            line.SetActive(false);
        }
    }
}
