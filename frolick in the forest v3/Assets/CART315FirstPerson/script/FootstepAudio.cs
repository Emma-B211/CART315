
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepAudio : MonoBehaviour
{
    private AudioSource footstepSource;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        footstepSource = GetComponent<AudioSource>();

        footstepSource.loop = true;
        footstepSource.playOnAwake = false;
        footstepSource.Stop();
    }

    void Update()
    {
        bool isWalking = controller.isGrounded &&
                         controller.velocity.magnitude > 0.1f;

        if (isWalking)
        {
            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }
}
