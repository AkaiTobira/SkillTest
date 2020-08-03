using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public float moveSpeed = 2.0f;
    
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;
    
    public GameObject dot;


    void Update ()
    {
        MouseAiming();
        KeyboardMovement();
    
        HandleClick();
    }
    
    void HandleClick(){
        if( !Input.GetButtonDown("Fire1") ) return;
    
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f))
 
        {
            if( hit.transform != null ){
                
                IPressButton buttonToPress = hit.transform.GetComponent<IPressButton>();
                if( buttonToPress != null ){
                    buttonToPress.PressButton();
                }
            }
        }
    }



    void MouseAiming ()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX   += Input.GetAxis("Mouse Y") * turnSpeed;
    
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
    
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }
    
    void KeyboardMovement ()
    {
        Vector3 dir = new Vector3(0, 0, 0);
    
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
    
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
