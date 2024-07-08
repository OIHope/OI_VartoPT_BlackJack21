using UnityEngine;

namespace Assets.Core.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioMusicObject;
        [SerializeField] private AudioSource audioFXObject;


        private static SoundManager _instance;
        public static SoundManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
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
        public void PlayMusic(AudioClip musicClip, float volume, bool loopAudio)
        {
            AudioSource audioSource = audioMusicObject;
            audioSource.clip = musicClip;
            audioSource.volume = volume;
            audioSource.loop = loopAudio;
            audioSource.Play();
        }
    }
}