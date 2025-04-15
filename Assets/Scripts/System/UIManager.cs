using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Serializable]
    public struct UiBlock
    {
        public TextMeshProUGUI BlockNameText;
        public TextMeshProUGUI BlockQuantityText;
        public Transform IconContainer;
        public Transform IconTemplate;
        public BlockObjectSO blockData;
        
        private Image _iconImage;

        /// <summary>
        /// Initializes the UI block by getting required component references from the provided transform.
        /// </summary>
        /// <param name="blockTransform">The transform representing the block.</param>
        public void Initialize(Transform blockTransform)
        {
            if (blockTransform.childCount >= 4)
            {
                blockTransform.GetChild(1).TryGetComponent(out BlockNameText);
                blockTransform.GetChild(2).TryGetComponent(out IconContainer);

                if (IconContainer.childCount > 0)
                {
                    IconTemplate = IconContainer.GetChild(0);
                    IconTemplate.TryGetComponent(out _iconImage);
                }
                blockTransform.GetChild(3).TryGetComponent(out BlockQuantityText);
            }
            else
                Debug.LogError("Block transform does not have the expected number of children.");
        }

        /// <summary>
        /// Sets the block data (name and icon) using the provided BlockObjectSO.
        /// </summary>
        /// <param name="blockData">A BlockObjectSO containing block data.</param>
        public void SetBlockData(BlockObjectSO blockData)
        {
            this.blockData = blockData;
            if (BlockNameText != null)
                BlockNameText.text = this.blockData.name;
            else
                Debug.LogWarning("BlockNameText is missing.");

            for (int i = IconContainer.childCount - 1; i >= 0; i--)
            {
                Transform child = IconContainer.GetChild(i);
                if (child != IconTemplate)
                    Destroy(child.gameObject);
            }

            if (_iconImage != null)
                _iconImage.sprite = blockData.sprite;
            else
                Debug.LogWarning("IconImage component is missing on the IconTemplate.");
        }

        /// <summary>
        /// Updates the quantity text displayed in the block.
        /// </summary>
        /// <param name="quantity">The quantity to display.</param>
        public void UpdateQuantity(int quantity)
        {
            if (BlockQuantityText != null) BlockQuantityText.text = quantity.ToString();
            else Debug.LogWarning("BlockQuantityText is missing.");
        }
    }

    [SerializeField] private Transform container;
    [SerializeField] private Transform blockTemplate;
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private List<UiBlock> uiBlocks = new List<UiBlock>();

    /// <summary>
    /// Sets up the visual UI blocks based on the list of block data in the BlockListSO.
    /// This method can be called from the Unity Editor context menu.
    /// </summary>
    [ContextMenu("Setup Visual")]
    private void SetupVisual()
    {
        uiBlocks.Clear();
        Debug.Log("Setting up UI blocks visual.");

        foreach (BlockObjectSO blockData in blockListSO.blockListSO)
        {
            Transform newBlockTransform = Instantiate(blockTemplate, container);
            newBlockTransform.gameObject.SetActive(true);

            UiBlock newUiBlock = new UiBlock();
            newUiBlock.Initialize(newBlockTransform);
            newUiBlock.SetBlockData(blockData);
            uiBlocks.Add(newUiBlock);
        }
    }

    private void Awake()
    {
        if (blockTemplate != null)
            blockTemplate.gameObject.SetActive(false);
    }

    public void UpdateVisual(int id, int quantity)
    {
        foreach (UiBlock ui in uiBlocks)
        {
            if (ui.blockData.id == id)
                ui.UpdateQuantity(quantity);
        }
    }
}
