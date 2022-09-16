using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    //private ParticleSystemRenderer ps;
    private ParticleSystem ps;

    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;
    private ParticleSystem.MainModule mainPs;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        mainPs = ps.main;
    }

    private void Update()
    {
        if(ps != null)
        {
            if(playerSpriteRenderer.transform.localScale.x == 1)
            {
                //ps.flip = new Vector3(0, 0, 0);
                mainPs.startRotationY = 0;
            }
            else if (playerSpriteRenderer.transform.localScale.x == -1)
            {
                //ps.flip = new Vector3(1, 0, 0);
                mainPs.startRotationY = 180;
            }
        }
    }
}
