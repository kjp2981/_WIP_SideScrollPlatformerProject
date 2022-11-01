using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private List<Laser> laserList = new List<Laser>();

    private List<Animator> laserAnimatorList = new List<Animator>();

    public void CreateLaser(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            Laser laser = PoolManager.Instance.Pop("Laser") as Laser;
        }
    }
}
