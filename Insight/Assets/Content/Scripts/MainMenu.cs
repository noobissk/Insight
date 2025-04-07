using System.Collections.Generic;
using System.Linq;
using GameProgression;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform _levelSelect;
    [SerializeField] private PlayAudioClip _audioClipPlayer;
    [SerializeField] private AudioClip _buttonPressAudio;
    private List<TMP_Text> _levelButtons;

    void Start()
    {
        _levelButtons = _levelSelect.GetComponentsInChildren<TMP_Text>().ToList();
        _levelButtons.RemoveAt(_levelButtons.Count-1);

        GameProgressHandler.LoadProgress();

        for (int i = 7; i > GameProgressHandler.currentUnlockedStage-1; i--)
        {
            _levelButtons[i].text = "X";
        }
    }

    public void OpenLevelSelect()
    {
        _levelSelect.gameObject.SetActive(true);
        Instantiate(_audioClipPlayer).PlayAudio(_buttonPressAudio);
    }
    public void CloseLevelSelect()
    {
        _levelSelect.gameObject.SetActive(false);
        Instantiate(_audioClipPlayer).PlayAudio(_buttonPressAudio);
    }


    public void OpenLevel(int i)
    {
        if (i < GameProgressHandler.currentUnlockedStage)
            SceneManager.LoadScene("Level_" + (i+1).ToString());
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
