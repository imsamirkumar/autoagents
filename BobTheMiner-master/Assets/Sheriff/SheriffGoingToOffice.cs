using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffGoingToOffice : State<Sheriff>
{

    static readonly SheriffGoingToOffice instance = new SheriffGoingToOffice();

    public static SheriffGoingToOffice Instance
    {
        get
        {
            return instance;
        }
    }

    public override void Enter(Sheriff agent)
    {
        if (agent.location != Locations.sheriffOffice)
        {
            Debug.Log(agent.ID + ": Going To My Office");

            agent.ChangeLocation(Locations.sheriffOffice);
        }
    }

    public override void Execute(Sheriff agent)
    {
        if (agent.location == Locations.sheriffOffice)
        {
            Debug.Log(agent.ID + ": No Sleeping In Office");

            agent.stateMachine.RevertToPreviousState();
        }
    }

    public override void Exit(Sheriff agent)
    {
        // shouldn't really happen
    }
}
