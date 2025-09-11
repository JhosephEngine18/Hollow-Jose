using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{

    [SerializeField]GameObject JumpSound;
    GameObject Sound;
    private void OnEnable()
    {
        PlayerController.Sounds += SoundsState;
        Destroy(Sound, 2);
    }

    private void OnDisable()
    {
        PlayerController.Sounds -= SoundsState;
    }

    

    void SoundsState(int state)
    {
        switch(state)
        {
            case 0:
                Sound = JumpSound;
                Instantiate(Sound);
                break;

                case 1:

                break;
        }

    }
    
}
