using UnityEngine;
using System.Collections;
using System;

public class SheriffsGlobalState : State<Sheriff>
{

    static readonly SheriffsGlobalState instance = new SheriffsGlobalState();

    public static SheriffsGlobalState Instance
    {
        get
        {
            return instance;
        }
    }

    public override void Enter(Sheriff agent)
    {

    }

    public override void Execute(Sheriff agent)
    {

    }

    public override void Exit(Sheriff agent)
    {
        // shouldn't really happen
    }

    public override bool OnSenseEvent(Sheriff agent, Sense sense)
    {
        if (sense.Sender == "Outlaw" && !(EntityManager.GetEntity(sense.Sender) as Outlaw).isDead)
        {
            MessageDispatcher.DispatchMessage(0, "Outlaw", agent.ID, MessageTypes.SheriffEncountered);
            return true;
        }

        return false;
    }
    public override bool OnSenseEvent2(Sheriff agent, Sense2 sense2)
    {
        if (sense2.Sender == GameObject.FindWithTag("Undertaker")) 
        {
            Debug.Log(agent.ID + " Saw " + sense2.Sender + " By Sense:" + sense2.senseType);
            Debug.Log(agent.ID +": Hope You Doing Your Job Properly " + sense2.Sender);
            return true;
        }
        return false;
    }
}