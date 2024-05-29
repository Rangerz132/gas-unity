using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData 
{
    public GameObject User { get; private set; }
    public IEnumerable<GameObject> targets;

    public AbilityData(GameObject user)
    {
        this.User = user;
    }
}
