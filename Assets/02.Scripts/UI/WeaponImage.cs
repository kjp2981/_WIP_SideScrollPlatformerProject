using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponImage : MonoBehaviour
{

    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private WeaponType type;
    public WeaponType Type => type;

    public void ChangeItemImage(Sprite image)
    {
        itemImage.color = Color.white;
        itemImage.sprite = image;
    }
}
