using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : PoolableMono
{
    private ParticleSystem ps;
    ParticleSystem.MainModule main;

    [SerializeField]
    private float initSpeed = 7;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
    }

    public void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }

    public void SetLocalScaleX(float value)
    {
        Vector3 pos = this.transform.localScale;
        pos.x = value;
        this.transform.localScale = pos;
    }

    //public void SetSpeed(float speed)
    //{
    //    main.startSpeed = speed;
    //}

    public override void Reset()
    {
        //SetSpeed(initSpeed);
    }
}
