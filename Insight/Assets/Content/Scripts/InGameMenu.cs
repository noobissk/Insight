using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [Header("Game over")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TMP_Text _gameOverTextWon;
    [SerializeField] private TMP_Text _gameOverTextLost;
    [SerializeField] private GameObject _nextLevelButton;
    [Header("Action menus")]
    [SerializeField] private RectTransform _actionMenu1;
    [SerializeField] private RectTransform _actionMenu1_offPosition;
    [SerializeField] private RectTransform _actionMenu2;
    [SerializeField] private RectTransform _actionMenu2_offPosition;
    [SerializeField] private float _actionMenuAnimationTime = 0.5f;
    [SerializeField] private AnimationCurve _actionMenuAnimationCurve;
    [Header("Audio")]
    [SerializeField] private AudioClip _openMenuAudio;
    [SerializeField] private AudioClip _startLevelAudio;
    [SerializeField] private AudioClip _gameOverLostAudio;
    [SerializeField] private AudioClip _gameOverWonAudio;
    [SerializeField] private PlayAudioClip _audioPlayerPrefab;
    private Vector2 _actionMenu1_onPoint, _actionMenu2_onPoint;
    public bool isActionMenuOpen { get; private set; }
    private float _actionMenuTimer;

    private void Start()
    {
        Time.timeScale = 1;
        _actionMenu1_onPoint = _actionMenu1.anchoredPosition;
        _actionMenu2_onPoint = _actionMenu2.anchoredPosition;
        
        _actionMenuTimer = _actionMenuAnimationTime;
        _actionMenu1.anchoredPosition = _actionMenu1_offPosition.anchoredPosition;
        _actionMenu2.anchoredPosition = _actionMenu2_offPosition.anchoredPosition;
    }

    public void ToggleActionMenu(bool i_useAudio = true)
    {
        if (!Mathf.Approximately(_actionMenuTimer, _actionMenuAnimationTime))
            return;
        if (i_useAudio)
            Instantiate(_audioPlayerPrefab).PlayAudio(_openMenuAudio);

        isActionMenuOpen = !isActionMenuOpen;
        _actionMenuTimer = 0;
    }

    private void Update()
    {
        if (!_gameOverScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
            return;
        }
        if (Mathf.Approximately(_actionMenuTimer, _actionMenuAnimationTime))
            return;

        _actionMenuTimer = Mathf.Clamp(_actionMenuTimer + Time.deltaTime, 0, _actionMenuAnimationTime);

        if (isActionMenuOpen)
        {
            _actionMenu1.anchoredPosition = Vector2.Lerp(_actionMenu1_offPosition.anchoredPosition, _actionMenu1_onPoint, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
            _actionMenu2.anchoredPosition = Vector2.Lerp(_actionMenu2_offPosition.anchoredPosition, _actionMenu2_onPoint, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
        }
        else
        {
            _actionMenu1.anchoredPosition = Vector2.Lerp(_actionMenu1_onPoint, _actionMenu1_offPosition.anchoredPosition, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
            _actionMenu2.anchoredPosition = Vector2.Lerp(_actionMenu2_onPoint, _actionMenu2_offPosition.anchoredPosition, _actionMenuAnimationCurve.Evaluate(_actionMenuTimer/_actionMenuAnimationTime));
        }
    }

    public void ShowGameOver(string i_args = "")
    {
        _gameOverScreen.SetActive(true);
        if (i_args == "")
        {
            Instantiate(_audioPlayerPrefab).PlayAudio(_gameOverWonAudio);
            _gameOverTextWon.gameObject.SetActive(true);
            _nextLevelButton.SetActive(true);
            return;
        }
        Instantiate(_audioPlayerPrefab).PlayAudio(_gameOverLostAudio);
        _gameOverTextLost.gameObject.SetActive(true);
        _gameOverTextLost.text = i_args;
    }

    public void ShowPauseMenu()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeInHierarchy);
        Instantiate(_audioPlayerPrefab).PlayAudio(_openMenuAudio);
        Time.timeScale = _pauseMenu.activeInHierarchy ? 0 : 1;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Main_menu");
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        int lvlID = int.Parse(SceneManager.GetActiveScene().name.Replace("Level_", ""));
        string newSceneName = "Level_" + (lvlID+1).ToString();
        if (lvlID == 8)
            return;
        SceneManager.LoadScene(newSceneName);
    }
    public void PlayStartLevelAudio()
    {
        Instantiate(_audioPlayerPrefab).PlayAudio(_startLevelAudio);
    }
}
