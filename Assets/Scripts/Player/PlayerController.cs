using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    Transform t;
    Rigidbody rb;

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
    private bool lockControls = false;

    [SerializeField] private UnityEngine.Rendering.Volume PPX;

    [Header("Animation")]
    [SerializeField]private Animator myAnimator;
    [SerializeField] private GameObject catcher;

    [SerializeField] private GameObject endScreen;

    [SerializeField] private GameObject fishingGame;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        t = this.transform;
        rb = GetComponent<Rigidbody>();
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

        if(Input.GetMouseButtonDown(0) && !lockControls) {
            Dash();
        }

        timeSinceLastDash += Time.deltaTime;
        float depthWeight = Mathf.InverseLerp(0f, 60f, transform.position.y);
        PPX.weight = Mathf.Lerp(1, 0.7f, depthWeight);
        if(Camera.main.transform.position.y > 60f) {
            PPX.weight = 0f;
        }

        if(transform.position.y > 60f) {
            rb.useGravity = true;
            lockControls = true;
        } else {
            rb.useGravity = false;
            lockControls = false;
        }

        if(fishingGame.activeSelf){
            lockControls = true;
            rb.velocity = Vector3.zero;
        }
        
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
            


            //x y z are screwed up bc I moved the capsule down!!!
            //t.Translate(new Vector3 (-moveX, moveZ, 0) * Time.deltaTime * speed);
            //t.Translate(new Vector3 (0, moveY, 0) * Time.deltaTime * speed, Space.World);
            Vector3 finalVelocity = (transform.up*moveZ + transform.right*-moveX + Vector3.up*moveY)*speed;
            if(!lockControls) {
            rb.velocity = finalVelocity;
            }

            if(finalVelocity.magnitude != 0f && !lockControls) {
                myAnimator.SetFloat("Speed", 1f);
            } else {
                myAnimator.SetFloat("Speed", 0.35f);
            }

            if(timeSinceLastDash < 0.8f) {
                myAnimator.SetFloat("Speed", dashCurve.Evaluate(timeSinceLastDash)+1f);
            }

            if(fishingGame.activeSelf){
                myAnimator.SetFloat("Speed", 3f);
            }

            Vector3 dashFinal = Vector3.Lerp(Vector3.zero, DashVector, dashCurve.Evaluate(timeSinceLastDash));
            if(!lockControls) {
            rb.velocity += dashFinal;
            }
            //transform.position = transform.position + (dashFinal*Time.deltaTime);
        }
    }

    void Dash() {
        if (hunger.getStamina() >= 25) {
            hunger.setStamina(-25);
            timeSinceLastDash = 0f;
            DashVector = transform.up * dashForce;
        }
    }

    public void Catch(){
        Vector3 point = transform.position;
        point.y = 65f;
        Instantiate(catcher, point, Quaternion.identity);
    }

    public void Die(){
        isDead = true;
        hunger.hooked();
        myAnimator.SetTrigger("Starve");
        lockControls = true;
        sensitivity = 0f;
        StartCoroutine(QueueEndScreen());
    }

    IEnumerator QueueEndScreen() {
        yield return new WaitForSeconds(1f);
        Instantiate(endScreen).GetComponent<EndScreen>().SetDemise("Starved!");
    }

    public void SetControlLock(bool _lock) {
        lockControls = _lock;
    }
}
