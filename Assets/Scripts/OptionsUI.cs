using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicEffectButton;
    [SerializeField] private Button closeEffectButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicEffectText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform rebindingScreen;

    private void Awake()
    {
        Instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.IncreaseVolume();
            UpdateVisual();
        });

        musicEffectButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            UpdateVisual();
        });

        closeEffectButton.onClick.AddListener(() =>
        {
            Hide();
        });

        moveUpButton.onClick.AddListener(() => { BindingNewKey(GameInput.Binding.MoveUp); });
        moveDownButton.onClick.AddListener(() => { BindingNewKey(GameInput.Binding.MoveDown); });
        moveLeftButton.onClick.AddListener(() => { BindingNewKey(GameInput.Binding.MoveLeft); });
        moveRightButton.onClick.AddListener(() => { BindingNewKey(GameInput.Binding.MoveRight); });
        interactButton.onClick.AddListener(() => { BindingNewKey(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { BindingNewKey(GameInput.Binding.InteractAlt); });
        pauseButton.onClick.AddListener(() => { BindingNewKey(GameInput.Binding.Pause); });

    }
    private void Start()
    {
        Hide();
        HideRebindingScreen();
        UpdateVisual();
        GameHandler.Instance.OnGameUnPaused += GameHandler_OnGameUnPaused;
    }

    private void GameHandler_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effect : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicEffectText.text = "Music Effect : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetKeyBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetKeyBindingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetKeyBindingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetKeyBindingText(GameInput.Binding.MoveRight);
        interactText.text = GameInput.Instance.GetKeyBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetKeyBindingText(GameInput.Binding.InteractAlt);
        pauseText.text = GameInput.Instance.GetKeyBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
    }

    public void ShowRebindingScreen() { 
        rebindingScreen.gameObject.SetActive(true);
    }

    public void HideRebindingScreen() {
        rebindingScreen.gameObject.SetActive(false);
    }

    public void BindingNewKey(GameInput.Binding binding)
    {
        ShowRebindingScreen();
        GameInput.Instance.RebindingKey(binding, () =>
        {
            HideRebindingScreen();
            UpdateVisual();
        });
    }

}
