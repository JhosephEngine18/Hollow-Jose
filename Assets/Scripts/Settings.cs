using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    [Header("Options")]

    [SerializeField, Range(0f, 1f)] float MusicVolume;

    [Header("Essentials")]
    [SerializeField, Tooltip("Add BackGround music for your level")] AudioSource Music;
    [SerializeField] AudioReverbFilter Filter;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Filter.GetComponent<AudioReverbFilter>();
        Filter.enabled = false;
    }

    private void OnEnable()
    {
        PlayerCollisions.Colliding += CheckFilter;
    }
    private void OnDisable()
    {
        PlayerCollisions.Colliding -= CheckFilter;
    }

    private void FixedUpdate()
    {
        CheckMusic();
    }
    void Update()
    {

    }

    void CheckMusic()
    {
        Music.volume = MusicVolume;
    }

    void CheckFilter(bool Colliding)
    {
        if (Colliding)
        {
            Filter.enabled = true;
            Filter.dryLevel = -10000f;
        }
        else
        {
            Filter.enabled = false;
            Filter.dryLevel = 0f;
        }
    }

}
