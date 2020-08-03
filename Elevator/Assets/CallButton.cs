using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallButton : MonoBehaviour, IPressButton
{

    [SerializeField] private ElevatorController m_elevator = null;
    [SerializeField] private int m_currentFloor = -1;

    private bool m_isPressed;

    [SerializeField] Material inactive;
    [SerializeField] Material active;

    public void PressButton(){
        m_elevator.CallOverCondignation( m_currentFloor );
        gameObject.GetComponent<Renderer>().material = active;
        m_isPressed = true;
    }

    void Update() {
        if( m_isPressed ){
            Debug.Log( m_elevator.GetCurrentFloorId() == m_currentFloor );
            if( m_elevator.GetCurrentFloorId() == m_currentFloor  ){
                m_isPressed = false;
                gameObject.GetComponent<Renderer>().material = inactive;
            }
        }

    }


}
