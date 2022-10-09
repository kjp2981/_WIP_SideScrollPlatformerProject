using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NaughtyAttributes;

public class CharacterInfo : MonoBehaviour, IPointerDownHandler
{
    private Player player;

    [SerializeField]
    private GameObject statusPanel;

    [SerializeField, BoxGroup("")] private Text hpText;
    [SerializeField, BoxGroup("")] private Text meleeAttackText;
    [SerializeField, BoxGroup("")] private Text rangeAttackText;
    [SerializeField, BoxGroup("")] private Text defenceText;
    [SerializeField, BoxGroup("")] private Text criticalRateText;
    [SerializeField, BoxGroup("")] private Text criticalDamageText;

    private void Start()
    {
        player = Define.Player.GetComponent<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(statusPanel.activeSelf == true)
        {
            statusPanel.SetActive(false);
        }
        else
        {
            hpText.text = player.realStatus.hp.ToString();
            meleeAttackText.text = player.realStatus.meleeAttack.ToString();
            rangeAttackText.text = player.realStatus.rangeAttack.ToString();
            defenceText.text = player.realStatus.defence.ToString();
            criticalRateText.text = player.realStatus.criticalRate.ToString();
            criticalDamageText.text = player.realStatus.criticalDamage.ToString();
            statusPanel.SetActive(true);
        }
    }
}
