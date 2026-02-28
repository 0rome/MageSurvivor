using UnityEngine;
using Zenject;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnPaused;
    public event Action OnResumed;

    public void PauseGame()
    {
        OnPaused?.Invoke();
    }
    public void ResumeGame()
    {
        OnResumed?.Invoke();
    }
}
