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
    float moveX;
    float moveY;
    float moveZ;

    // Start is called before the first frame update
    void Start()
    {
        t = this.transform;

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
            t.Translate(new Vector3 (moveX, moveZ, 0) * Time.deltaTime * speed);
            t.Translate(new Vector3 (0, moveY, 0) * Time.deltaTime * speed, Space.World);

        }
    }
}
