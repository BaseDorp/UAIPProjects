using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
    None = 0,
    SawPlayer,
    ReachPlayer,
    LostPlayer,
    NoHealth,
}

public enum FSMStateID
{
    None = 0,
    Patrolling,
    Chasing,
    Attacking,
    Dead,
}

public class AdvancedFSM : FSM
{
    private List<FSMState> fsmStates;
    private FSMStateID currentStateID;
    public FSMStateID CurrentStateID
    {
        get
        {
            return currentStateID;
        }
    }
    private FSMState currentState;
    public FSMState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public AdvancedFSM()
    {
        fsmStates = new List<FSMState>();
    }

    // Adds a new state to the list
    public void AddFSMState (FSMState fsmState)
    {
        // Check for Null reference
        if (fsmState == null)
        {
            Debug.LogError("FSM ERROR: Null reference");
        }

        // First State inserted is also the
        if (fsmStates.Count == 0)
        {
            fsmStates.Add(fsmState);
            currentState = fsmState;
            currentStateID = fsmState.ID;
            return;
        }

        // Add the state to the List
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == fsmState.ID)
            {
                Debug.LogError("FSM Error: Trying to add a state");
                return;
            }
        }

        // If there is no state, add the current one to the list
        fsmStates.Add(fsmState);
    }

    public void DeleteState(FSMStateID fsmState)
    {
        // Check for Null State
        if (fsmState == FSMStateID.None)
        {
            Debug.LogError("FSM Error: null ID");
            return;
        }

        // delete the state if in the list
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == fsmState)
            {
                fsmStates.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM Error: The state passed in was not in the list");
    }

    public void PerformTransition(Transition transition)
    {
        // Check for Null Transition before changing state
        if (transition == Transition.None)
        {
            Debug.LogError("FSM ERROR: Null transition");
            return;
        }

        // Checks if the currentState has the transition passed as argument
        FSMStateID id = currentState.GetOutputState(transition);
        if (id == FSMStateID.None)
        {
            Debug.LogError("FSM ERROR: Current State does not have a target state for this transition");
            return;
        }

        // Update the currentStateID and currentState		
        currentStateID = id;
        foreach (FSMState state in fsmStates)
        {
            if (state.ID == currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }
}
