using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource playerAudioSrc;
    public AudioSource sfxAudioSrc;
    public AudioClip heavyBreathingClip;
    public AudioClip jumpClip;

    private StarterAssets.StarterAssetsInputs _input;
    private CharacterController _controller;
    private bool isRunning = false;
    private bool isJumping = false;

    void Start()
    {

        _input = GetComponent<StarterAssets.StarterAssetsInputs>();
        _controller = GetComponent<CharacterController>();

        if (playerAudioSrc == null)
        {
            Debug.LogError("Add Player Audio source to audiohandler");
        }
        if (sfxAudioSrc == null)
        {
            Debug.LogError("Add sfx Audio source to audiohandler");
        }
        playerAudioSrc.loop = false;
        playerAudioSrc.playOnAwake = false;

        sfxAudioSrc.loop = false;
        sfxAudioSrc.playOnAwake = false;
    }

    void Update()
    {
        HandleBreathingSound();
        HandleJumpSound();
    }
    private void HandleJumpSound()
    {
        if (_input.jump && _controller.isGrounded && !isJumping)
        {
            isJumping = true;
            PlaySfx(jumpClip);
        }
        else if (!_input.jump)
        {
            isJumping = false;
        }
    }
    private void HandleBreathingSound()
    {
        bool isMoving = _controller.velocity.magnitude > 0.1f;
        bool isSprinting = _input.sprint && isMoving;

        if (isSprinting && !isRunning)
        {
            isRunning = true;
            PlayPlayerSound(heavyBreathingClip);
        }
        else if (!isSprinting)
        {
            isRunning = false;
            playerAudioSrc.Stop();
        }
    }

    private void PlayPlayerSound(AudioClip clip)
    {
        if (playerAudioSrc.clip != clip)
        {
            playerAudioSrc.Stop();
            playerAudioSrc.clip = clip;
        }
        if (!playerAudioSrc.isPlaying && clip)
        {
            playerAudioSrc.Play();
        }
    }
    private void PlaySfx(AudioClip clip)
    {
        if (sfxAudioSrc.clip != clip)
        {
            sfxAudioSrc.Stop();
            sfxAudioSrc.clip = clip;
        }
        if (!sfxAudioSrc.isPlaying && clip)
        {
            sfxAudioSrc.Play();
        }
    }
}
