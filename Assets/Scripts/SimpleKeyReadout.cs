using UnityEngine;
using TMPro;

public class InputKeyReadout : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI readout;
    [SerializeField] private KeyCode key = KeyCode.E;

    private float lastKeyUpTime = -1f; // -1 means "never yet"

    void Update()
    {
        if (!readout) return;

        float sceneTime = Time.time;
        bool isHeld = Input.GetKey(key);
        bool down = Input.GetKeyDown(key);
        bool up = Input.GetKeyUp(key);

        if (up)
            lastKeyUpTime = sceneTime;

        string upInfo =
            (lastKeyUpTime < 0f)
            ? "Last UP: (never)"
            : "Last UP: " + lastKeyUpTime.ToString("F3") + "s";

        string text =
            "<b>Time</b>\n" +
            "Scene Time: " + sceneTime.ToString("F3") + "s\n\n" +

            "<b>Keyboard</b>\n" +
            "Key: " + key + "\n" +
            "Down (this frame): " + down + "\n" +
            "Up (this frame): " + up + "\n" +
            upInfo + "\n" +
            "Current status (held): " + isHeld;

        readout.text = text;
    }
}
