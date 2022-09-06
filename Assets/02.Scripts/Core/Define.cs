using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Define
{
    private static GameObject player = null;
    private static CinemachineVirtualCamera vCam = null;

    public static GameObject Player
    {
        get
        {
            if(player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            return player;
        }
    }

    public static CinemachineVirtualCamera VCam
    {
        get
        {
            if(vCam == null)
            {
                vCam = GameObject.FindWithTag("VCam").GetComponent<CinemachineVirtualCamera>();
            }
            return vCam;
        }
    }
}
