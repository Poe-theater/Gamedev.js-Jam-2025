using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Serializable]
    public struct UiBlock
    {
        private Image iconImage;

        public TextMeshProUGUI blockNameText;
        public TextMeshProUGUI blockQuantityText;
        public Transform iconContainer;
        public Transform iconTemplate;
        public BlockObjectSO blockData;

        public void Initialize(Transform blockTransform)
        {
            if (blockTransform.childCount < 4)
            {
                Debug.LogError("Block transform missing expected children.");
                return;
            }

            blockNameText = blockTransform.GetChild(1).GetComponent<TextMeshProUGUI>();
            iconContainer = blockTransform.GetChild(2);
            if (iconContainer.childCount > 0)
            {
                iconTemplate = iconContainer.GetChild(0);
                iconImage = iconTemplate.GetComponent<Image>();
            }
            blockQuantityText = blockTransform.GetChild(3).GetComponent<TextMeshProUGUI>();
        }

        public void SetBlockData(BlockObjectSO newBlockData)
        {
            blockData = newBlockData;
            if (blockNameText != null)
                blockNameText.text = blockData.name;
            else
                Debug.LogWarning("blockNameText is null.");

            if (iconImage != null)
                iconImage.sprite = blockData.sprite;
            else
                Debug.LogWarning("iconImage is missing on template.");
        }

        public void UpdateQuantity(int quantity)
        {
            if (blockQuantityText != null)
                blockQuantityText.text = quantity.ToString();
            else
                Debug.LogWarning("blockQuantityText is null.");
        }
    }

    #region variable 
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button quitButtonInGame;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private GameObject titleText;

    [SerializeField] private GameObject gameplayUi;
    [SerializeField] private BlockProduction blockProduction;
    [SerializeField] private Transform blocksContainer;
    [SerializeField] private Transform blockTemplate;
    [SerializeField] private BlockListSO blockListSo;
    [SerializeField] private List<UiBlock> uiBlocks;

    [SerializeField] private Image progressBar;
    [SerializeField] private Image blockIconBar;

    #endregion

    #region MonoBehaviour function

    private void Awake()
    {
        if (blockTemplate != null)
            blockTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        ConfigureButtons();
    }

    private void OnDisable()
    {
        blockProduction.OnProgressChanged -= HandleProgressChanged;
        blockProduction.OnProgressBlockChanged -= HandleProgressBlockChanged;
    }

    #endregion

    #region MainMenu
    private void ConfigureButtons()
    {
        startButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            GameManager.Instance.SetGameState(GameState.Playing);
            EnterPlayMode();
        });



        quitButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            GameManager.Instance.SetGameState(GameState.Quit);
        });
        quitButtonInGame.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            GameManager.Instance.SetGameState(GameState.Quit);
        });
        restartButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            RestartGame();
        });
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void EnterPlayMode()
    {
        buttonContainer.SetActive(false);
        titleText.SetActive(false);
        gameplayUi.SetActive(true);
        SubscribeToProduction();
    }

    #endregion

    #region Gameplay ui

    private void SubscribeToProduction()
    {
        blockIconBar.sprite = blockProduction.GetCurrentBlock().sprite;
        blockProduction.OnProgressChanged += HandleProgressChanged;
        blockProduction.OnProgressBlockChanged += HandleProgressBlockChanged;
    }

    [ContextMenu("SetupVisualBlocks")]
    private void SetupVisualBlocks()
    {
        uiBlocks.Clear();
        foreach (var block in blockListSo.blockListSO)
        {
            var newTransform = Instantiate(blockTemplate, blocksContainer);
            newTransform.gameObject.SetActive(true);

            UiBlock uiBlock = new UiBlock();
            uiBlock.Initialize(newTransform);
            uiBlock.SetBlockData(block);
            uiBlocks.Add(uiBlock);
        }
    }

    private void HandleProgressChanged(object sender, float progress)
    {
        progressBar.fillAmount = progress;
    }

    private void HandleProgressBlockChanged(object sender, BlockObjectSO newBlock)
    {
        blockIconBar.sprite = newBlock.sprite;
    }

    public void UpdateVisual(int id, int quantity)
    {
        foreach (var uiBlock in uiBlocks)
        {

            if (uiBlock.blockData.id == id)
            {
                uiBlock.UpdateQuantity(quantity);
                break;
            }
        }
    }

    #endregion
}
