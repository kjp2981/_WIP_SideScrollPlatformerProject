using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CC
{
    Faint, // ����
    Fear, // ����
    Poison, // ��
    Silence, // ħ��
    Fascination, // ��Ȥ
}

public interface ICrowdControl
{
    public CC cc { get; }

    public bool isCC { get; }

    public void CCAction(CC cc);
}
