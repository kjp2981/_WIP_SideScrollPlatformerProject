using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CC
{
    None, // �ƹ��͵� �ƴ� ����
    Faint, // ����
    Fear, // ����
    Poison, // ��
    Silence, // ħ��
    Fascination, // ��Ȥ
    Ability, // ��ų ��� ��
}

public interface ICrowdControl
{
    public CC cc { get; }

    public bool isCC { get; }

    public void CCAction(CC cc);
}
