using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Menu Components")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsMenuCanva;
    [SerializeField] private GameObject _keyboardControlsCanva;
    [SerializeField] private GameObject _gamepadControlsCanva;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _keyboardControlsFirst;
    [SerializeField] private GameObject _gamepadControlsFirst;

    // Start is called before the first frame update
    void Start()
    {
        _settingsMenuCanva.SetActive(false);
        _keyboardControlsCanva.SetActive(false);
        _gamepadControlsCanva.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenMainMenu()
    {
        _mainMenuCanvas.SetActive(true);
        _settingsMenuCanva.SetActive(false);
        _keyboardControlsCanva.SetActive(false);
        _gamepadControlsCanva.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    private void OpenSettingsMenu()
    {
        _mainMenuCanvas.SetActive(false);
        _keyboardControlsCanva.SetActive(false);
        _gamepadControlsCanva.SetActive(false);
        _settingsMenuCanva.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }

    private void OpenKeyBoardControlsMenu()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanva.SetActive(false);
        _keyboardControlsCanva.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_keyboardControlsFirst);
    }

    private void OpenGamepadControlsMenu()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanva.SetActive(false);
        _keyboardControlsCanva.SetActive(false);
        _gamepadControlsCanva.SetActive(true);

        EventSystem.current.SetSelectedGameObject(_gamepadControlsFirst);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void OnSettingsPress()
    {
        OpenSettingsMenu();
    }

    public void OnKeyboardControlsPress()
    {
        OpenKeyBoardControlsMenu();
    }

    public void OnGamepadControlsPress()
    {
        OpenGamepadControlsMenu();
    }

    public void OnSettingsBackPress()
    {
        OpenMainMenu();
    }

    public void OnControlsBackPress()
    {
        OpenSettingsMenu();
    }

    public void OnApplicationQuit()
    {
        QuitGame();
    }
}
