using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Model;

// This class is applied to a button that represents an Item in the View. It is a visual representation of the item
// when it is visible in the store. The class holds a link to the original Item, it sets the icon of the button to the one specified in the Item data,
// and it enables or disables the checkbox to indicate if the Item is selected.
public class ItemContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject checkMark = null;

    [SerializeField]
    private Text nameText = null;

    [SerializeField]
    private Text costText = null;

    [SerializeField]
    private Text quantityText = null;

    private Item item;

    public Item GetItem()
    {
        return item;
    }

    public void Initialize(Item item, bool isSelected)
    {
        // Store item
        this.item = item;

        // Set checkmark visibility
        checkMark.SetActive(isSelected);

        // Set button image
        Image image = GetComponentInChildren<Image>();
        Sprite sprite = SpriteCache.Get(item.GetComponent<DrawableComponent>().IconName);

        nameText.text = item.Name;
        costText.text = item.Cost.ToString();
        quantityText.text = item.Quantity.ToString() + 'x';

        if (sprite != null)
            image.sprite = sprite;
    }
}
