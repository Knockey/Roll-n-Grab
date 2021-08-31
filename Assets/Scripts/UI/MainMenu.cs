using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _exitButon;
    [SerializeField] private CreditsPanel _creditsPanel;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _creditsButton.onClick.AddListener(OnCreditsButtonClick);
        _exitButon.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _creditsButton.onClick.RemoveListener(OnCreditsButtonClick);
        _exitButon.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnStartButtonClick()
    {
        Game.Load();
    }

    private void OnCreditsButtonClick()
    {
        _creditsPanel.gameObject.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
