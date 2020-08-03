using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondignationController : MonoBehaviour
{
    [SerializeField] DoorController[] m_door;

    public void CloseDoor( int elevatorIndex){
        m_door[elevatorIndex].CloseTheDoor();
    }

    public void OpenDoor( int elevatorIndex){
        m_door[elevatorIndex].OpenTheDoor();
    }

    public bool AreDoorClosed( int elevatorIndex ){
        return m_door[elevatorIndex].AreDoorsClosed();
    }

    public Vector3 GetPosition(){
        return GetComponent<Transform>().position;
    }

}
