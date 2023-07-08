using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform t;

    [Header("Player Rotation")]
    public float sensitivity = 1;

    //clamp vars

    public float rotationMin;
    public float rotationMax;


    float rotationX;
    float rotationY;

    [Header("Player Movement")]
    public float speed = 1;
    public float dashForce = 30f;
    public AnimationCurve dashCurve;
    private float timeSinceLastDash = 1f;
    float moveX;
    float moveY;
    float moveZ;

    private Vector3 DashVector;
    private Hunger hunger;

    [Header("Animation")]
    [SerializeField]private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        t = this.transform;
        hunger = gameObject.GetComponent<Hunger>();

        //this causes the cursor to disappear -- remove if stupid
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();

        //debug
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetMouseButtonDown(0)) {
            Dash();
        }

        timeSinceLastDash += Time.deltaTime;
    }

    void LookAround()
    {
        //mouse input
        rotationX += Input.GetAxis("Mouse X")*sensitivity;  
        rotationY += Input.GetAxis("Mouse Y")*sensitivity;

        rotationY = Mathf.Clamp(rotationY, rotationMin, rotationMax);

        //setting rotation input every update
        t.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
    }

    void Move() 
    {
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
            moveZ = Input.GetAxis("Forward");
            
            if(moveX != 0 || moveY != 0 || moveZ != 0) {
                myAnimator.SetFloat("Speed", 1f);
            } else {
                myAnimator.SetFloat("Speed", 0.35f);
            }

            if(timeSinceLastDash < 0.8f) {
                myAnimator.SetFloat("Speed", dashCurve.Evaluate(timeSinceLastDash)+1f);
            }

            //x y z are screwed up bc I moved the capsule down!!!
            t.Translate(new Vector3 (-moveX, moveZ, 0) * Time.deltaTime * speed);
            t.Translate(new Vector3 (0, moveY, 0) * Time.deltaTime * speed, Space.World);
            Vector3 dashFinal = Vector3.Lerp(Vector3.zero, DashVector, dashCurve.Evaluate(timeSinceLastDash));
            transform.position = transform.position + (dashFinal*Time.deltaTime);
        }
    }

    void Dash() {
        if (hunger.getStamina() >= 25) {
            hunger.setStamina(-25);
            timeSinceLastDash = 0f;
            DashVector = transform.up * dashForce;
        }
    }
}
