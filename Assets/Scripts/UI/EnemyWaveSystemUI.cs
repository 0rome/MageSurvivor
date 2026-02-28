using TMPro;
using UnityEngine;
using Zenject;

public class EnemyWaveSystemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveCountdownText;

    [Inject] private EnemyWavesSystem enemyWavesSystem;

    private void OnEnable()
    {
        enemyWavesSystem.OnWaveCountdown += UpdateCountdown;
        enemyWavesSystem.OnWaveStarted += ShowWaveStarted;
    }

    private void UpdateCountdown(int seconds)
    {
        if (seconds > 0)
            waveCountdownText.text = $"Next wave in: {seconds}";
    }

    private void ShowWaveStarted(int waveNumber)
    {
        waveCountdownText.text = $"Wave {waveNumber} started!";
        Invoke(nameof(ClearText), 1f);
    }

    private void ClearText()
    {
        waveCountdownText.text = "";
    }

    private void OnDisable()
    {
        if (enemyWavesSystem == null) return;

        enemyWavesSystem.OnWaveCountdown -= UpdateCountdown;
        enemyWavesSystem.OnWaveStarted -= ShowWaveStarted;
    }
}
