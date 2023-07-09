using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameraPos;
    private float timeStarted;
    private Vector3 startPos;
    private Quaternion startRot;
    [SerializeField] private AnimationCurve lerpCurve;
    [SerializeField] private GameObject endScreen;
    void Start()
    {
        PullCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PullCamera() {
        timeStarted = Time.time;
        startPos = Camera.main.transform.position;
        startRot = Camera.main.transform.rotation;
        Camera.main.transform.parent = null;
        StartCoroutine(CameraCoroutine());
    }

    IEnumerator CameraCoroutine()
    {
        Debug.Log("Coroutine started...");
        while (Time.time - timeStarted < 1f)

        {
            Camera.main.transform.position = Vector3.Lerp(startPos, cameraPos.position, lerpCurve.Evaluate(Time.time - timeStarted));
            Camera.main.transform.rotation = Quaternion.Lerp(startRot, cameraPos.rotation, lerpCurve.Evaluate(Time.time - timeStarted));
            Debug.Log(lerpCurve.Evaluate(timeStarted - Time.time));
            yield return null;
        }
        Debug.Log("Coroutine ended");
    }

    public void QueueEndScreen(){
        Instantiate(endScreen);
    }
}
