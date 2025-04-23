using UnityEngine;
using UnityEngine.UI;

public class UiMainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private GameObject title;

    private void Start()
    {
        SetButton();
    }

    private void SetButton()
    {
        startButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            GameManager.Instance.SetGameState(GameState.Playing);
            OnPlay();
        });

        optionsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            GameManager.Instance.SetGameState(GameState.Options);

        });

        quitButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClick();
            GameManager.Instance.SetGameState(GameState.Quit);
        });
    }

    private void OnPlay()
    {
        buttonContainer.SetActive(false);
        title.SetActive(false);
    }

}
