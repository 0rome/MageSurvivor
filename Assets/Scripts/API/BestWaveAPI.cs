using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class BestWaveAPI : MonoBehaviour
{
    [Header("API Settings")]
    [SerializeField] private string baseUrl = "http://localhost:5093";

    private string SaveEndpoint => $"{baseUrl}/api/wave";
    private string GetEndpoint(string playerId) => $"{baseUrl}/api/wave/{playerId}";

    #region Public Methods

    public void SendBestWave(string playerId, int bestWave)
    {
        StartCoroutine(SendBestWaveCoroutine(playerId, bestWave));
    }

    public void GetBestWave(string playerId)
    {
        StartCoroutine(GetBestWaveCoroutine(playerId));
    }

    #endregion

    #region Save

    private IEnumerator SendBestWaveCoroutine(string playerId, int bestWave)
    {
        WaveRequest requestData = new WaveRequest
        {
            playerId = playerId,
            bestWave = bestWave
        };

        string json = JsonUtility.ToJson(requestData);

        using (UnityWebRequest request = new UnityWebRequest(SaveEndpoint, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Best wave successfully sent to server.");
                Debug.Log("Server response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("Sending to: " + SaveEndpoint);
                Debug.LogError("Error sending wave: " + request.error);
            }
        }
    }

    #endregion

    #region Get

    private IEnumerator GetBestWaveCoroutine(string playerId)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetEndpoint(playerId)))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player data received: " + request.downloadHandler.text);

                WaveResponse response =
                    JsonUtility.FromJson<WaveResponse>(request.downloadHandler.text);

                Debug.Log($"Best Wave: {response.bestWave}");
            }
            else
            {
                Debug.LogError("Error getting wave: " + request.error);
            }
        }
    }

    #endregion
}

#region DTO Classes

[System.Serializable]
public class WaveRequest
{
    public string playerId;
    public int bestWave;
}

[System.Serializable]
public class WaveResponse
{
    public int id;
    public string playerId;
    public int bestWave;
}

#endregion
