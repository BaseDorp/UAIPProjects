using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Touch : Sense
{
    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //Check the aspect
            if (aspect.aspectName == aspectName)
            {
                print("Enemy Touch Detected");
            }
        }
    }
}
