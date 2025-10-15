using UnityEngine;
using UnityEngine.UI;

public class VolumeTogglePause : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource; 

    [Header("Icons")]
    public Image volumeOnImage;  
    public Image volumeOffImage; 

    [Header("Opties")]
    [Tooltip("Als true: wanneer het geluid gestopt was (niet playing) en je unmuted, start dan afspelen.")]
    public bool resumeIfStopped = false;

    private bool isMuted = false;
    private bool wasPlayingBeforeMute = false;

    void Start()
    {
        if (audioSource == null)
            Debug.LogWarning("VolumeTogglePause: audioSource niet ingesteld.");

        UpdateIcons();
    }

    
    public void ToggleVolume()
    {
        if (audioSource == null) return;

        isMuted = !isMuted;

        if (isMuted)
        {
            
            wasPlayingBeforeMute = audioSource.isPlaying;
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
        else
        {
            
            if (wasPlayingBeforeMute)
            {
                audioSource.UnPause();
            }
            else if (resumeIfStopped && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        UpdateIcons();
    }

    void UpdateIcons()
    {
        if (volumeOnImage != null)  volumeOnImage.enabled  = !isMuted;
        if (volumeOffImage != null) volumeOffImage.enabled = isMuted;
    }

    
    public void SetMuted(bool mute)
    {
        if (isMuted == mute) return;
        ToggleVolume();
    }
}