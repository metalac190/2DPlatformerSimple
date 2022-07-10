using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinTrigger : TriggerVolume
{
    protected override void TriggerEntered(GameObject enteredObject)
    {
        PlayerCharacter player = enteredObject.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("WIN");
        }
    }
}
