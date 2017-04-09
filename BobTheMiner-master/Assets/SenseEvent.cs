using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Timers;


public enum SenseType
{
	Sight,
	Hearing,
	Smell
};

public struct Sense
{
	public string Sender;
	public string Receiver;
	public SenseType senseType;

    public Sense(string s, string r, SenseType st)
    {
        Sender = s;
        Receiver = r;
        senseType = st;
    }
}

public struct Sense2
{
    public GameObject Sender;
    public string Receiver;
    public SenseType senseType;

    public Sense2(GameObject s, string r, SenseType st)
    {
        Sender = s;
        Receiver = r;
        senseType = st;
    }
}

public static class SenseEvent
{
	public static AStar aStar;

	static float SENSE_RANGE = 2.0f;
    public static Collider2D [] agents;
    public static int sightrange = 3;
    

    public static void UpdateSensors()
    {
        hearing();
        sight();
    }
    public static void sight()
    {

        for (int i = 0; i < EntityManager.GetCount(); ++i)
        {
            Agent a1 = EntityManager.GetEntity(i);
           
            agents = Physics2D.OverlapCircleAll(a1.transform.position, sightrange, 1 << LayerMask.NameToLayer("Agents"));
            foreach (Collider2D c in agents)
                    {
                    //Debug.Log(a1.ID + " SAW : " + c.gameObject.ToString());
                     Sense2 sense2 = new Sense2(c.gameObject, a1.ID, SenseType.Sight);
                     a1.HandleSenseEvent2(sense2);
                    } 
            
                }
           }
     
    public static void hearing()
    {   // Agents pairwise check
        for (int i = 0; i < EntityManager.GetCount(); ++i)
        {
            Agent a1 = EntityManager.GetEntity(i);
            for (int j = 0; j < EntityManager.GetCount(); ++j)
            {
                if (i != j)
                {
                    Agent a2 = EntityManager.GetEntity(j);

                    // If close enough
                    if (Vector2.Distance(a1.CurrentPosition, a2.CurrentPosition) < SENSE_RANGE)
                    {
                        // Propogate the sense
                        var a1Pos = new Point() { x = (int)a1.CurrentPosition.x, y = (int)a1.CurrentPosition.y };
                        var a2Pos = new Point() { x = (int)a2.CurrentPosition.x, y = (int)a2.CurrentPosition.y };
                        if (aStar.calculatePath(a1Pos, a2Pos) != null)
                        {

                            // Sense the agent
                            Sense sense = new Sense(a2.ID, a1.ID, SenseType.Hearing);
                            a1.HandleSenseEvent(sense);

                        }
                    }
                }
            }
        }
    }
}
