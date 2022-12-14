using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAndDoorHandler : MonoBehaviour
{
    public static readonly string BUTTON_PRESSED_FLAG = "buttonpressed#";

    [SerializeField] private Performer openPerfomerDoor;
    [SerializeField] private Performer closePerfomerDoor;
    [SerializeField] private Performer pressPerfomerButton;
    [SerializeField] private Performer unpressPerfomerButton;
    [SerializeField] private SetFlagPerfomer setFlagPerformer;

    [SerializeField] private Trigger doorOpenTrigger;
    [SerializeField] private Trigger doorClosedTrigger;

    [SerializeField] private bool invertButton = false;
    [SerializeField] private bool oneTimePressOnly = false;

    [SerializeField] private bool savePressAsFlag = false;
    [SerializeField] private string flagName = "name";


    private void Awake()
    {
        if (!invertButton)
        {
            doorOpenTrigger.performer = Util.ArrayAddIfNotContains(doorOpenTrigger.performer, openPerfomerDoor, pressPerfomerButton);
            if (savePressAsFlag)
            {
                setFlagPerformer.flagName = BUTTON_PRESSED_FLAG + flagName;
                doorOpenTrigger.performer = Util.ArrayAddIfNotContains(doorOpenTrigger.performer, setFlagPerformer);
            }
            if (!oneTimePressOnly)
                doorClosedTrigger.performer = Util.ArrayAddIfNotContains(doorClosedTrigger.performer, closePerfomerDoor, unpressPerfomerButton);
        } else
        {
            doorOpenTrigger.performer = Util.ArrayAddIfNotContains(doorOpenTrigger.performer, closePerfomerDoor, pressPerfomerButton);
            if (savePressAsFlag)
            {
                setFlagPerformer.flagName = BUTTON_PRESSED_FLAG + flagName;
                doorClosedTrigger.performer = Util.ArrayAddIfNotContains(doorClosedTrigger.performer, setFlagPerformer);
            }

            if (!oneTimePressOnly)
                doorClosedTrigger.performer = Util.ArrayAddIfNotContains(doorClosedTrigger.performer, openPerfomerDoor, unpressPerfomerButton);
            
        }
    }

    private void Start()
    {
        if (savePressAsFlag && PlayerData.instance.IsFlagSet(BUTTON_PRESSED_FLAG + flagName) && !invertButton)
        {
            openPerfomerDoor.OnTap(null);
        }
        

        if (invertButton)
        {
            if (savePressAsFlag && PlayerData.instance.IsFlagSet(BUTTON_PRESSED_FLAG + flagName))
            {
                closePerfomerDoor.OnTap(null);
            } else 
                openPerfomerDoor.OnTap(null);
        }
        
    }

}
