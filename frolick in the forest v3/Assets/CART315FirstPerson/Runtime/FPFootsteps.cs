
using System;
using UnityEngine;

namespace Cart315.FirstPerson
{
    [RequireComponent(typeof(FPController), typeof(AudioSource))]
    public class FPFootsteps : MonoBehaviour
    {
        [Header("Footstep Sounds")]
        [Tooltip("Leave empty for silent movement. Drag your audio clips here.")]
        [SerializeField] private AudioClip[] _sounds = Array.Empty<AudioClip>();

        [Header("Footstep Settings")]
        [Tooltip("Horizontal metres travelled between steps.")]
        [SerializeField, Min(0.1f)] private float _stepDistance = 1.8f;

        [SerializeField, Range(0, 1)] private float _volume = 0.35f;

        [Tooltip("Prevents another footstep from playing until the previous one finishes.")]
        [SerializeField] private bool _waitForPreviousStep = true;

        public event Action FootstepPlayed;

        private FPController _player;
        private AudioSource _audioSource;
        private float _distance;
        private int _lastClip = -1;

        private void Awake()
        {
            _player = GetComponent<FPController>();
            _audioSource = GetComponent<AudioSource>();

            // Prevent automatic playback and looping.
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _audioSource.Stop();
        }

        private void LateUpdate()
        {
            // Only play footsteps while actively walking on the ground.
            if (!_player.isActiveAndEnabled ||
                !_player.IsGrounded ||
                !_player.IsInputMoving)
            {
                _distance = 0f;
                return;
            }

            // Avoid overlapping footsteps if enabled.
            if (_waitForPreviousStep && _audioSource.isPlaying)
                return;

            _distance += _player.GroundDistanceThisFrame;

            float requiredDistance = Mathf.Max(0.1f, _stepDistance);

            if (_distance < requiredDistance)
                return;

            _distance %= requiredDistance;

            if (_sounds == null || _sounds.Length == 0)
                return;

            // Select a random valid clip.
            int first = UnityEngine.Random.Range(0, _sounds.Length);
            int selected = -1;

            for (int i = 0; i < _sounds.Length; i++)
            {
                int index = (first + i) % _sounds.Length;

                if (_sounds[index] == null)
                    continue;

                selected = index;

                if (index != _lastClip)
                    break;
            }

            if (selected < 0)
                return;

            // Play one individual footstep.
            _audioSource.PlayOneShot(_sounds[selected], _volume);

            _lastClip = selected;

            FootstepPlayed?.Invoke();
        }

        private void OnDisable()
        {
            _distance = 0f;

            if (_audioSource != null)
                _audioSource.Stop();
        }
    }
}
