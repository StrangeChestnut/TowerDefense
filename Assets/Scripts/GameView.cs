using System;
using Objects;
using ScriptableObjects;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public GameController game;
    public GameViewController controller;
    
    [SerializeField] private Text _score;
    [SerializeField] private Text _timer;
    [SerializeField] private Text _wave;
    [SerializeField] private Text _hp;

    private void OnEnable()
    {
        controller = new GameViewController();
        controller.OnOpen(this);
        game.StartGame(this);
    }

    private void OnDisable()
    {
        controller.OnClose(this);
    }

    public void UpdateScore(int value) => Set(_score, value);

    public void UpdateTimer(int value)
    {
        if (!_timer.gameObject.activeSelf) 
            _timer.gameObject.SetActive(true);
        
        Set(_timer, value);
        
        if (value == 0)
            _timer.gameObject.SetActive(false);
        
    }

    public void UpdateWave(int value) => Set(_wave, value);
    public void UpdateCastleHealth(float value) => Set(_hp, (int) value);

    private static void Set(Text text, int value)
    {
        text.text = value.ToString();
    }
}