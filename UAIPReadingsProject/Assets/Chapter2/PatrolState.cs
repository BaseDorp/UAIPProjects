using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : FSMState
{  
    public PatrolState(Transform[] wp)
    {
        waypoints = wp;
        stateID = FSMStateID.Patrolling;

        curRotSpeed = 1.0f;
        curSpeed = 100.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        // Check the distance from player tank
        // When the distance is near, trasitino to chase state
        if (Vector3.Distance(npc.position, player.position) <= 300.0f)
        {
            Debug.Log("Switch to Chase State");
            npc.GetComponent<NPCTankController>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        // Find another random patrol point if the current point is reached
        if (Vector3.Distance(npc.position, destPos) <= 100.0f)
        {
            Debug.Log("Reached the destination");
            FindNextPoint();
        }

        // Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);

        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curSpeed);

        // Go Forward
        npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }


}
