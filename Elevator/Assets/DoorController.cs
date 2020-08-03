using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator m_animator;

    private bool hodor = false;

    private float timeToCloseTheDoor = 0;

    [SerializeField] private float amountOfTimeRequiredToShutDoor = 10f; 

    void Start()
    {
        m_animator = GetComponent<Animator>();
        timeToCloseTheDoor = amountOfTimeRequiredToShutDoor;
    }

    void Update()
    {
        UpdateCloseTimer();
        UpdateAnimator();
    }

    void UpdateAnimator(){
        m_animator.SetFloat("timeToClose", timeToCloseTheDoor);
    }

    void UpdateCloseTimer(){
        timeToCloseTheDoor -= Time.deltaTime;
        if( hodor ) timeToCloseTheDoor = amountOfTimeRequiredToShutDoor;
    }

    public void OpenTheDoor(){
        timeToCloseTheDoor = amountOfTimeRequiredToShutDoor;
    }

    public void CloseTheDoor(){
        timeToCloseTheDoor = -1;
    }

    public bool AreDoorsClosed(){
        return m_animator.GetCurrentAnimatorStateInfo(0).IsName("DoorClosed");
    }


    void OnTriggerEnter(Collider other) {
        if( other.tag.Contains("MainCamera") ){
            hodor = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if( other.tag.Contains("MainCamera") ){
            hodor = false;
        }
    }
}
