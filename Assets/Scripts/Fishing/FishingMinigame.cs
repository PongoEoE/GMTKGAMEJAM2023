using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMinigame : MonoBehaviour
{

    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;
    [SerializeField] Transform fish;

    [SerializeField] private Animator crab;
    [SerializeField] private Animator net;
    // Start is called before the first frame update

    [SerializeField]float fishPosition;
    float fishDestination;
    float fishTimer;

    [SerializeField] float timerMultiplicator = 3f;

    float fishSpeed;

    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform hook;

    float hookPosition;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float fishSize = 1f;
    [SerializeField] float hookPower = 5f;
    float hookProgress;
    float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = .005f;
    [SerializeField] float hookProgressDegradationPower = 6f;

    [SerializeField] Image hookSpriteRenderer;
    [SerializeField] Transform progressBarContainer;

    bool pause = false;
    
    [SerializeField] float failTimer = 10f;

    public bool gotAway;
    private void Update()
    {
        if(pause) {return;}

 
        Fish();
        Hook();
        progressCheck();
    }
    private void Start()
    {
  
        pause = false;
        gameObject.SetActive(false);
        Resize();
    }

    void OnEnable()
    {
        
        if (gotAway == true)
        {
            failTimer = 10f;
            fishPosition = .5f;
            hookProgress = 0f;
            //progressBarContainer.localScale.y= 1;
        }
    }



    private void progressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        } else {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;

            failTimer -= Time.deltaTime;
            if (failTimer < 0f)
            {
                Lose();
                gotAway = true;
            }
        }

        if (fishPosition == 1 || fishPosition == 0 )
        {
            hookProgress += hookPower * Time.deltaTime;

        }

        if(fishPosition == 0) {
            crab.SetBool("Action", true);
        } else {
            crab.SetBool("Action", false);
        }

        if(fishPosition == 1) {
            net.SetBool("Action", true);
        } else {
            net.SetBool("Action", false);
        }

        if (hookProgress >= 1f)
        {
            Win();
            gotAway = false;
        }
        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }

    private void Win()
    {
        pause = true;
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.Catch();
        gameObject.SetActive(false);
        Debug.Log("Caught");
    }

    private void Lose()
    {
        pause = true;
        
        gameObject.SetActive(false);
        //++ to the num of hooks and lived
        Debug.Log("Not Caught");
    }
    private void Resize()
    {
        // Bounds b = hookSpriteRenderer.bounds;
        // float ySize = b.size.y;
        // Vector3 ls = hook.localScale;
        // float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        // ls.y = (distance / ySize * hookSize);
        // hook.localScale = ls;    

    }

    void Hook()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;

            fishDestination = UnityEngine.Random.value;
        }

        hookPosition = Mathf.SmoothDamp(hookPosition, fishDestination, ref fishSpeed, smoothMotion);

        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);






        //Mouse click 1
        // if (Input.GetMouseButton(0))
        // {
        //     hookPullVelocity += hookPullPower * Time.deltaTime;
        // }

        // hookPullVelocity -= hookGravityPower * Time.deltaTime;

        // hookPosition += hookPullVelocity;

        // if(hookPosition - hookSize / 2 <0f && hookPullVelocity < 0f)
        // {
        //     hookPullVelocity = 0f;
        // }

        // if(hookPosition + hookSize / 2 >= 1f && hookPullVelocity > 1f)
        // {
        //     hookPullVelocity = 0f;
        // }

        // hookPosition = Mathf.Clamp(hookPosition, hookSize / 2 ,1 - hookSize/2);
        // hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
    }
    void Fish()
    {
        // fishTimer -= Time.deltaTime;
        // if (fishTimer < 0f)
        // {
        //     fishTimer = UnityEngine.Random.value * timerMultiplicator;

        //     fishDestination = UnityEngine.Random.value;
        // }

        // fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);

        // fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
                
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }

        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        fishPosition += hookPullVelocity;

        if(fishPosition - fishSize / 2 <0f && hookPullVelocity < 0f)
        {
            hookPullVelocity = 0f;
        }

        if(fishPosition + fishSize / 2 >= 1f && hookPullVelocity > 1f)
        {
            hookPullVelocity = 0f;
        }

        fishPosition = Mathf.Clamp(fishPosition, fishSize / 2 ,1 - fishSize/2);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }


    public bool didGetAway()
    {
        return gotAway;
    }

    public void lived()
    {
        pause = false;
    }
        
    



}
