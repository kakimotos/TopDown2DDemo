using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _time;
    private TextMeshProUGUI _timerText;

    private void Awake()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        var minutes = (_time / 60).ToString("00");
        var seconds = (_time % 60).ToString("00");

        _timerText.text = minutes + ":" + seconds;
    }
}
