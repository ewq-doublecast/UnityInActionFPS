using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreLabel;

    [SerializeField]
    private SettingsPopup _settingsPopup;

    private int _score;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.EnemyHit, OnEnemyHit);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.EnemyHit, OnEnemyHit);
    }

    private void Start()
    {
        _score = 0;
        _scoreLabel.text = _score.ToString();

        _settingsPopup.Close();
    }

    public void OnOpenSettings()
    {
        _settingsPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("Pointer down");
    }

    private void OnEnemyHit()
    {
        _score++;
        _scoreLabel.text = _score.ToString();
    }
}
