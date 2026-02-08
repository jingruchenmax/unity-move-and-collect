using UnityEngine;
using TMPro;

public class SimpleMouseReadout : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI readout;

    private float lastLeftUpTime = -1f;  
    private float lastRightUpTime = -1f;  

    private Vector3 lastMousePos;

    void Start()
    {
        lastMousePos = Input.mousePosition;
    }

    void Update()
    {
        if (!readout) return;

        float sceneTime = Time.time;

        Vector3 mouseScreen = Input.mousePosition;                 // pixels
        Vector3 mouseDelta = mouseScreen - lastMousePos;           // pixels since last frame
        lastMousePos = mouseScreen;
        Vector3 mouseWorld = Vector3.zero;
        bool hasMainCamera = (Camera.main != null);

        if (hasMainCamera)
        {
            mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 0f; // keep it 2D
        }

        bool leftDown = Input.GetMouseButtonDown(0);
        bool leftHeld = Input.GetMouseButton(0);
        bool leftUp = Input.GetMouseButtonUp(0);

        bool rightDown = Input.GetMouseButtonDown(1);
        bool rightHeld = Input.GetMouseButton(1);
        bool rightUp = Input.GetMouseButtonUp(1);

        if (leftUp) lastLeftUpTime = sceneTime;
        if (rightUp) lastRightUpTime = sceneTime;

        string leftUpInfo =
            (lastLeftUpTime < 0f)
            ? "Last Left UP: (never)"
            : "Last Left UP: " + lastLeftUpTime.ToString("F3") + "s";

        string rightUpInfo =
            (lastRightUpTime < 0f)
            ? "Last Right UP: (never)"
            : "Last Right UP: " + lastRightUpTime.ToString("F3") + "s";

        string worldInfo =
            (!hasMainCamera)
            ? "World: (no Main Camera found)"
            : "World: " + mouseWorld.x.ToString("F2") + ", " + mouseWorld.y.ToString("F2");

        string text =
            "<b>Time</b>\n" +
            "Scene Time: " + sceneTime.ToString("F3") + "s\n\n" +

            "<b>Mouse Position</b>\n" +
            "Screen (px): " + (int)mouseScreen.x + ", " + (int)mouseScreen.y + "\n" +
            worldInfo + "\n" +
            "Delta (px): " + (int)mouseDelta.x + ", " + (int)mouseDelta.y + "\n\n" +

            "<b>Mouse Buttons</b>\n" +
            "Left  - Down: " + leftDown + "  Held: " + leftHeld + "  Up: " + leftUp + "\n" +
            leftUpInfo + "\n" +
            "Right - Down: " + rightDown + " Held: " + rightHeld + " Up: " + rightUp + "\n" +
            rightUpInfo;

        readout.text = text;
    }
}
