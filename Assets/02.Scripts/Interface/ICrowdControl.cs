using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CC
{
    Faint, // 기절
    Fear, // 공포
    Poison, // 독
    Silence, // 침묵
    Fascination, // 매혹
}

public interface ICrowdControl
{
    public CC cc { get; }

    public bool isCC { get; }

    public void CCAction(CC cc);
}
