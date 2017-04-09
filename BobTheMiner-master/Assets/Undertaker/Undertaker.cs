using Completed;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Undertaker : MovingAgent<Undertaker> {
	#region implemented abstract members of Agent
	public override string ID {
		get {
			return "Undertaker";
		}
	}

	public override void Update () {

	}
	#endregion

	public override bool UpdateStateMachine()
	{

		if (base.UpdateStateMachine ())
			return true;

		this.stateMachine.Update();
		MessageDispatcher.DispatchDelayedMessages(ID);

		return true;
	}
    public bool _sensed = false;
    public bool sensed
    {
        set
        {
            this.GetComponent<Animator>().SetBool("sensed", value);
            _sensed = value;
        }
        get
        {
            return _sensed;
        }
    }
    public void Awake()
	{
		
	}

	//Protected, virtual functions can be overridden by inheriting classes.
	protected override void Start()
	{
		base.Start ();

		this.stateMachine = new StateMachine<Undertaker>();
		this.stateMachine.Init(this, HangoutInTheOffice.Instance, UndertakersGlobalState.Instance);

		InvokeRepeating("UpdateStateMachine", 1, 1);
		EntityManager.RegisterAgent (this);
	}
}
