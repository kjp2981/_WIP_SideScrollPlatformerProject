using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : PoolableMono
{
    private ParticleSystem ps;
    ParticleSystem.MainModule main;

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

    public override void Reset()
    {

    }
}
