using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    Slash slash;

    private void Start()
    {
        slash = GetComponentInParent<Slash>();
    }

    public void EndAnimation()
    {
        slash.Pooling();
    }
}
