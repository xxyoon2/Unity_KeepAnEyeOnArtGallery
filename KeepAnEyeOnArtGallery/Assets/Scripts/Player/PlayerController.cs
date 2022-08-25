using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string RotateAxisName = "Horizontal";
    public string MoveAxisName = "Vertical";
    public string CCTVKeyName = "CCTVOnOff";
    public string ObjectInteractionName = " In";

    public float X { get; private set; }
    public float Z { get; private set; }
    public bool CanCCTVOn { get; private set; }
    public bool CanInteract { get; private set; }
    void Update()
    {
        X = Z = 0f;
        CanCCTVOn = CanInteract = false;
        
        X = Input.GetAxis(RotateAxisName);
        Z = Input.GetAxis(MoveAxisName);
        CanCCTVOn = Input.GetButtonDown(CCTVKeyName);
        CanInteract = Input.GetButtonDown(ObjectInteractionName);
    } 
}
