using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfoPanel.UI
{
    public class UIInfoDescription : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text name;
        [SerializeField]
        private TMP_Text description;
        
        public void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false);
            name.text = "";
            description.text = "";
        }

        public void SetDescription(Sprite sprite, string itemName,
            string itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            name.text = itemName;
            description.text = itemDescription;
        }
    }
}