using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    [SerializeField] int SetFramerate;
    [SerializeField] TextMeshProUGUI FPS;
    private float deltaTime = 0.0f;

    private void Awake()
    {
        Application.targetFrameRate = SetFramerate;
        FPS.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        deltaTime += ((Time.unscaledDeltaTime - deltaTime) * 0.1f); // Smoothed delta time
        float fps = Mathf.Round(1.0f / deltaTime);
        FPS.text = "FPS: " + fps;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
}
