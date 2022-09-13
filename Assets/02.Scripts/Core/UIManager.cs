using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField]
    private GameObject playerOutHpbar;
    [SerializeField]
    private GameObject playerInHpbar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayerOutHpbar(int currentHp, int maxHp)
    {
        Vector3 scale = playerOutHpbar.transform.localScale;
        scale.x = (float)currentHp / (float)maxHp;
        scale.x = Mathf.Clamp(scale.x, 0f, 1f);
        playerOutHpbar.transform.localScale = scale;

        //transform.DOKill();
        //StopAllCoroutines();
        StartCoroutine(PlayerInHpbar(currentHp, maxHp));
    }

    private IEnumerator PlayerInHpbar(int currentHp, int maxHp)
    {
        yield return new WaitForSeconds(0.5f);

        float hp = (float)currentHp / (float)maxHp;
        hp = Mathf.Clamp(hp, 0f, 1f);
        playerInHpbar.transform.DOScaleX(hp, 0.3f);
        //Vector3 scale = playerInHpbar.transform.localScale;
        //scale.x = Mathf.Lerp(scale.x, (float)currentHp / (float)maxHp, 0.7f);
        //scale.x = Mathf.Clamp(scale.x, 0f, 1f);
        //playerInHpbar.transform.localScale = scale;


    }
}
