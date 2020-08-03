using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [Range(0,1)] public int elevatorIndex = 0;
    public DoorController m_door;
    private int currentFloorId = 0;
    private int targetIndex    = 0;

    [SerializeField] CondignationController[] m_condignations;

    HashSet<int> floorsToStopAt = new HashSet<int>();
    bool isMovingUp = false;


    public AudioSource[] audioClips;

    enum States{
        goingUp,
        goingDown,
        waitOnFloor
    }

    void Start()
    {
        foreach( CondignationController cc in m_condignations){
            cc.CloseDoor( elevatorIndex );
        }    
    }

    public int GetCurrentFloorId(){
        return currentFloorId;
    }

    States m_currentState = States.waitOnFloor;

    public void CallOverCondignation( int floorId){
        if( currentFloorId == floorId && m_currentState == States.waitOnFloor ) {
            OpenCurrentdDoor();
        }else{
            floorsToStopAt.Add(floorId);
        }
    }

    public void CloseAllDoors(){
        m_door.CloseTheDoor();
        m_condignations[currentFloorId].CloseDoor(elevatorIndex);
    }

    void FixedUpdate() {
        SelectNextMove();
    }

    bool HasCondignationAboveCurrentToStopAt(){
        foreach( int floor in floorsToStopAt ){
            if( floor > currentFloorId ) return true;
        }
        return false;
    }

    bool HasCondignationBelowCurrentToStopAt(){
        foreach( int floor in floorsToStopAt ){
            if( floor < currentFloorId ) return true;
        }
        return false;
    }

    void HandleMoveUp(){
        if( !HasCondignationAboveCurrentToStopAt() ) {
            m_currentState = States.waitOnFloor;
            return;
        }

        targetIndex = currentFloorId + 1;
        Ride( Vector3.up );
    }

    void HandleMoveDown(){
        if( !HasCondignationBelowCurrentToStopAt() ) {
            m_currentState = States.waitOnFloor;
            return;
        }

        targetIndex = currentFloorId - 1;
        Ride( Vector3.down );
    }


    void SelectNextMove(){

        switch( m_currentState ){
            case States.goingUp: 
                HandleMoveUp();
            break;
            case States.goingDown:
                HandleMoveDown();
            break;
            case States.waitOnFloor:
                if( HasCondignationAboveCurrentToStopAt() ) m_currentState = States.goingUp; 
                if( HasCondignationBelowCurrentToStopAt() ) m_currentState = States.goingDown;
                audioClips[0].Play();

            break;
        }

    }

    private void LockElevatorToCondignation(){
            Vector3 Epos = transform.position; 
            Epos.y = m_condignations[currentFloorId].GetPosition().y;
            transform.Translate( Epos - transform.position );
    }


    private bool AreAllRequiredDoorsClosed(){
        return m_door.AreDoorsClosed() && m_condignations[currentFloorId].AreDoorClosed(elevatorIndex);
    }

    public float speed = 2;

    IEnumerator CloseDoor( ){
        yield return new WaitForSeconds(.1f);
        CloseAllDoors();
    }

    private void Ride( Vector3 direction ){

        if( !AreAllRequiredDoorsClosed() ) {
            LockElevatorToCondignation();
            StartCoroutine( CloseDoor() );
            audioClips[1].Stop();
            return;
        }else{
            if( !audioClips[1].isPlaying ) audioClips[1].Play();
        }
        
        MoveToNextFloor(direction);
    }

    private void OpenCurrentdDoor(){
        m_door.OpenTheDoor();
        m_condignations[currentFloorId].OpenDoor(elevatorIndex);
    }

    private void MoveToNextFloor( Vector3 direction ){
        float distance = Mathf.Abs( m_condignations[targetIndex].GetPosition().y - GetComponent<Transform>().position.y );
        if( distance < 0.5f ){

            currentFloorId = targetIndex;
            LockElevatorToCondignation();

            if( floorsToStopAt.Contains( targetIndex )){
                OpenCurrentdDoor();
                RemoveLastReachedFloor();
                
                audioClips[2].Play();
            }

        }else{
            transform.Translate( direction * speed * Time.deltaTime );
        }
    }

    private void RemoveLastReachedFloor(){
        floorsToStopAt.Remove( targetIndex );
    }

}
