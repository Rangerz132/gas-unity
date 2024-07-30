using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineImpulseSource impulseSource;

    void Start()
    {

    }
}
