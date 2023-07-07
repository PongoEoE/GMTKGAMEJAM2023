using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private void  OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player") && other.GetComponent<FirstPersonMovement>() != null) 
       {
        FirstPersonMovement movement = other.GetComponent<FirstPersonMovement>();
       }
    }

    private void  OnTriggerExit(Collider other)
    {
        
    }


}
