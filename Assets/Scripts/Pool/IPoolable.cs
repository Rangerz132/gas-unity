using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable 
{
    public void Fire();
    public void Release();
    public void SetPoolManager();
}
