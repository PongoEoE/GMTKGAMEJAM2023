using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpClusterer : MonoBehaviour
{
    [SerializeField] private GameObject[] kelp;
    [SerializeField] private float radius;
    [SerializeField] private int count;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle*radius;
            GameObject currentKelp = GameObject.Instantiate(kelp[Random.Range(0,2)], transform.position + new Vector3(offset.x, 0, offset.y), Quaternion.identity, transform);
            RaycastHit hit;
            Physics.Raycast(currentKelp.transform.position, Vector3.down, out hit, 100f);
            currentKelp.transform.position = hit.point;
            currentKelp.transform.localScale=Vector3.one*Random.Range(2f, 3f);
            currentKelp.transform.localEulerAngles = new Vector3(-90f, 0f, Random.Range(0f, 360f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
