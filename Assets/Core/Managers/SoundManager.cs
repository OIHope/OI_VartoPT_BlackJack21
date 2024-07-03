using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource audioFXObject;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
    public void PlaySoundFX(AudioClip audioClip, Transform spawnTransform, float volume, bool deTune)
    {
        AudioSource audioSource = Instantiate(audioFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = deTune ? Random.Range(0.8f, 1.2f) : 1f;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }
}
