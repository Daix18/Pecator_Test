using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [Header("Menu Components")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsMenuCanva;
    [SerializeField] private GameObject _keyboardControlsCanva;
    [SerializeField] private GameObject _gamepadControlsCanva;

    [Header("Player Components")]
    [SerializeField] private MovimientoJugador _player;
    [SerializeField] private AttackController _playerAttack;
    private bool isPaused;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _keyboardControlsFirst;
    [SerializeField] private GameObject _gamepadControlsFirst;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanva.SetActive(false);
        _keyboardControlsCanva.SetActive(false);
        _gamepadControlsCanva.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.THIS.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        _player.enabled = false;
        _playerAttack.enabled = false;

        OpenMainMenu();
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        _player.enabled = true;
        _playerAttack.enabled = true;

        CloseMenus();
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

    private void CloseMenus()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanva.SetActive(false);
        _keyboardControlsCanva.SetActive(false);
        _gamepadControlsCanva.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
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

    public void OnResumePress() 
    {
        UnPause();
    }
    
    public void OnSettingsBackPress()
    {
        OpenMainMenu();
    }

    public void OnControlsBackPress()
    {
        OpenSettingsMenu();
    }
}
