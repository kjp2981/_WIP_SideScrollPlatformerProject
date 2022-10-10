using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NaughtyAttributes;
using DG.Tweening;

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
        RectTransform panelRect = statusPanel.GetComponent<RectTransform>();
        if(statusPanel.activeSelf == true)
        {
            statusPanel.transform.DOKill();

            panelRect.localScale = new Vector3(1, 1, 1);

            panelRect.DOScale(new Vector3(0, 0, 1), 0.3f).OnComplete(() => statusPanel.SetActive(false));
        }
        else
        {
            statusPanel.transform.DOKill();

            hpText.text = player.realStatus.hp.ToString();
            meleeAttackText.text = player.realStatus.meleeAttack.ToString();
            rangeAttackText.text = player.realStatus.rangeAttack.ToString();
            defenceText.text = player.realStatus.defence.ToString();
            criticalRateText.text = player.realStatus.criticalRate.ToString();
            criticalDamageText.text = player.realStatus.criticalDamage.ToString();
            panelRect.localScale = new Vector3(0, 0, 1);
            statusPanel.SetActive(true);

            panelRect.DOScale(new Vector3(1, 1, 1), 0.3f);
        }
    }
}
