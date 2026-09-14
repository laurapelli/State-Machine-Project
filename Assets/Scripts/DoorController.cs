using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float minRot = 0.0f;     //Close
    public float maxRot = -100.0f;     //Open
    private float currentRot = 0.0f;
    public float angularVelocity =90.0f;    
    public DoorState currentSate = DoorState.None;

    public enum DoorState
    {
        None = -1,
        Closed = 0,
        Opnened,
        Closing,
        Opening,
        DoorStates
    }
    void Start()
    {
        currentRot = minRot;
        currentSate = DoorState.Closed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTransitions();
        UpdateStates();
    }
    private void UpdateTransitions()
    {
        switch (currentSate)
        {
            case DoorState.Closed:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentSate = DoorState.Opening;
                }
                break;
            case DoorState.Opnened:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(DoorState.Closing);
                }
                break;
            case DoorState.Closing:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(DoorState.Opening);
                }
                if(currentRot >= minRot)
                {
                    currentRot = minRot;
                    ChangeState(DoorState.Closed);
                }
                break;
            case DoorState.Opening:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ChangeState(DoorState.Closing);
                }
                if (currentRot <= maxRot)
                {
                    currentRot = maxRot;
                    ChangeState(DoorState.Opnened);
                }
                break;
            default:
                Debug.LogError("Invalid state");
                break;
        }
    }

    private void ChangeState(DoorState newState)
    {
        //if(newState == DoorState.None || newState >= DoorState.DoorStates)
        //{
        //    Debug.LogError("Invalid new state");
        //}
        Debug.Assert(newState != DoorState.None && newState < DoorState.DoorStates, "Invalid new state");
        if (newState != currentSate)
        {
            currentSate = newState;
        }
    }
    private void UpdateStates()
    {
        switch (currentSate)
        {
            
            case DoorState.Closing:
                //Calculate the angle of rotation
                currentRot += angularVelocity * Time.deltaTime;
                transform.rotation =  Quaternion.AngleAxis(currentRot, transform.up);
                break;
            case DoorState.Opening:
                //Calculate the angle of rotation
                currentRot -= angularVelocity * Time.deltaTime;
                transform.rotation = Quaternion.AngleAxis(currentRot, transform.up);
                break;
        }
    }
}
