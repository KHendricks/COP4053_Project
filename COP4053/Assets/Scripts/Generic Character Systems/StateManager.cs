using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager<T> {
    IState<T> state;
    Dictionary<string, IState<T>> states;
    IState<T> requestedState;

    // Constructor; Takes state machine owner and array
    // of state names, sets owner for each and adds states
    // to dictionary.
    public StateManager()
    {
        states = new Dictionary<string, IState<T>>();
    }
	
    public void Update (T owner) 
    {
        if (requestedState != state)
        {
            if (state != null) state.OnExit(owner);
            state = requestedState;
            state.OnEnter(owner);
        }
        else 
        {
            if (state != null)
                state.Update(owner);
        }
	}

    public void Add(string name, IState<T> state) 
    {
        if(states.ContainsKey(name)) 
            Debug.Log("State already added: " + name);
        else
            states.Add(name, state);
    }

    public void Switch(string state) {
        if(states.ContainsKey(state))
        {
            requestedState = states[state];
        }
    }
}
