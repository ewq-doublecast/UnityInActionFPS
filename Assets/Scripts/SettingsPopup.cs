using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField]
    private Slider _speedSlider;

    private void Start()
    {
        _speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }    

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSubmitName(string name)
    {
        Debug.Log($"Name: {name}");
    }

    public void OnSpeedValue(float speed)
    {
        Debug.Log($"Speed: {speed}");
        PlayerPrefs.SetFloat("speed", speed);
        Messenger<float>.Broadcast(GameEvent.SpeedChanged, speed);
    }
}
