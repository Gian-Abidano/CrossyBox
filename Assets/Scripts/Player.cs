using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stepScore;
    [SerializeField, Range(0.01f,1f)] float moveDuration = 0.3f;
    [SerializeField, Range(0.01f,1f)] float jumpPower = 0.5f;
    private float backLimit;
    private float leftLimit;
    private float rightLimit;
    [SerializeField] private int maxTravel;
    public int MaxTravel {get => maxTravel;}
    [SerializeField] private int currentTravel;
    public int CurrentTravel {get => currentTravel;}
    public bool isDead { get => this.enabled == false; }

    public void SetUp(int minZPos, int extent)
    {
        backLimit = minZPos - 1;
        leftLimit = -(extent + 1);
        rightLimit = extent + 1;       
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            Debug.Log("Forward");
            direction += new Vector3(0,0,1);
            // direction += new Vector3(1,0,0);
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            Debug.Log("Back");
            direction += new Vector3(0,0,-1);
            // direction += new Vector3(-1,0,0);
        }

        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Left");
            direction += new Vector3(-1,0,0);
            // direction += new Vector3(0,0,1);
        }
        
        if(Input.GetKey(KeyCode.D))
        {
            Debug.Log("Right");            
            direction += new Vector3(1,0,0);
            // direction += new Vector3(0,0,-1);
        }

        if(direction == Vector3.zero)
            return;

        if(IsJumping() == false)
        {  
            Move(direction);
            Debug.Log("Done");
        }
    }
    
    private void Move(Vector3 playerDirection)
    {   
        Vector3 TargetPosition = transform.position + playerDirection;

        transform.LookAt(TargetPosition);

        var MoveSeq = DOTween.Sequence(transform);
        MoveSeq.Append(transform.DOMoveY(jumpPower ,moveDuration/2));
        MoveSeq.Append(transform.DOMoveY(0 ,moveDuration/2));

        if (TargetPosition.z <= backLimit || 
            TargetPosition.x <= leftLimit ||
            TargetPosition.x >= rightLimit)
            return;

        if(Tree.AllTreePos.Contains(TargetPosition))
            return;
        
        transform.DOMoveX(TargetPosition.x, moveDuration, true);
        transform.DOMoveZ(TargetPosition.z, moveDuration, true).OnComplete(UpdateTravel);
    }

    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }
    
    private void UpdateTravel()
    {
        currentTravel = (int) this.transform.position.z;
        if(currentTravel > maxTravel)
            maxTravel = currentTravel;
        
        stepScore.text = "STEP : " + maxTravel.ToString();
    }

    private void OnTriggerEnter(Collider other) {

        var car = other.GetComponent<Car>();
        if(other.tag == "Car")
        {
            Debug.Log("Collision");
            AnimateCrash(car);
        }
    }

    private void AnimateCrash(Car car)
    {
        transform.DOScaleY(0.1f,0.2f);
        transform.DOScaleX(1.2f,0.2f);
        transform.DOScaleZ(1.2f,0.2f);
        this.enabled = false;
    }
}
