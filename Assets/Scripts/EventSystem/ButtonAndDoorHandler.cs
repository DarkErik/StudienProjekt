using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAndDoorHandler : MonoBehaviour
{
    [SerializeField] private Performer openPerfomerDoor;
    [SerializeField] private Performer closePerfomerDoor;
    [SerializeField] private Performer pressPerfomerButton;
    [SerializeField] private Performer unpressPerfomerButton;

    [SerializeField] private Trigger doorOpenTrigger;
    [SerializeField] private Trigger doorClosedTrigger;

    [SerializeField] private bool invertButton = false;
    [SerializeField] private bool oneTimePressOnly = false;

    private void Awake()
    {
        if (!invertButton)
        {
            doorOpenTrigger.performer = Util.ArrayAddIfNotContains(doorOpenTrigger.performer, openPerfomerDoor, pressPerfomerButton);
            if (!oneTimePressOnly)
                doorClosedTrigger.performer = Util.ArrayAddIfNotContains(doorClosedTrigger.performer, closePerfomerDoor, unpressPerfomerButton);
        } else
        {
            doorOpenTrigger.performer = Util.ArrayAddIfNotContains(doorOpenTrigger.performer, closePerfomerDoor, pressPerfomerButton);
            if (!oneTimePressOnly)
                doorClosedTrigger.performer = Util.ArrayAddIfNotContains(doorClosedTrigger.performer, openPerfomerDoor, unpressPerfomerButton);
            
        }
    }

    private void Start()
    {
        if (invertButton)
            openPerfomerDoor.OnTap(null);
    }

}
