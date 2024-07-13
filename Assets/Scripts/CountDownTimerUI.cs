using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownTimerUI : MonoBehaviour
{
    private TextMeshProUGUI textCountDownTimer;

    private void Start()
    {
        textCountDownTimer = GetComponent<TextMeshProUGUI>();
        GameHandler.Instance.OnStateChanged += GameHandler_OnStateChanged;
        
        Hide();
    }

    private void GameHandler_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameHandler.Instance.IsOnStartingCountDown())
        {
            Show();
        } else
        {
            Hide();
        }
    }

    private void Update()
    {
        textCountDownTimer.text = Mathf.Ceil(GameHandler.Instance.GetCountDownTimer()).ToString();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
