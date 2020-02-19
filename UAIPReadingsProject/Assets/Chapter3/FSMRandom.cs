using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMRandom : MonoBehaviour
{
    public enum FSMState
    {
        Chase,
        Flee
    }

    public int chaseProbabiilty = 80;
    public int fleeProbabiilty = 20;

    public ArrayList statesPoll = new ArrayList();

    // initialization
    void Start()
    {
        for (int i = 0; i < chaseProbabiilty; i++)
        {
            statesPoll.Add(FSMState.Chase);
        }

        for (int i = 0; i < fleeProbabiilty; i++)
        {
            statesPoll.Add(FSMState.Flee);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomState = Random.Range(0, statesPoll.Count);
            Debug.Log(statesPoll[randomState].ToString());
        }
    }
}
