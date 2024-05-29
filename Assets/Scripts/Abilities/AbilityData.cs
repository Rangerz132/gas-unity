using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData 
{
    public GameObject User { get; private set; }
    public IEnumerable<GameObject> targets;
    public Vector3 targetedPoints;

    public AbilityData(GameObject user)
    {
        this.User = user;
    }

    public void StartCoroutine(IEnumerator coroutine) {
        User.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
    }
}
