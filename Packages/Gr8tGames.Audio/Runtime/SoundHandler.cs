using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gr8tGames.Audio
{
  public class SoundHandler : MonoBehaviour
  {
    private const float InactiveSoundCheckPeriod = 1.0f;

    public AudioSource Play(SoundDefinition sound, Vector3? position = null)
    {
      var pos = position ?? Vector3.zero;
      var soundGameObject = GetSoundFromPool();
      soundGameObject.SetActive(true);
      soundGameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
      var audio = soundGameObject.GetComponent<AudioSource>();
      audio.clip = sound.Clip;
      audio.volume = sound.Volume;
      audio.pitch = sound.Pitch;
      audio.loop = sound.IsLoop;
      audio.spatialBlend = sound.SpatialBend;
      audio.outputAudioMixerGroup = sound.Output;
      audio.Play();
      Invoke(nameof(ReturnInactiveSoundsToPool), InactiveSoundCheckPeriod);
      return audio;
    }

    public void Stop(AudioSource audio)
    {
      audio.Stop();
      audio.gameObject.SetActive(false);
    }

    private List<GameObject> SoundPool;

    private void Awake()
    {
      SoundPool = new List<GameObject>();
    }

    private GameObject GetSoundFromPool()
    {
      var inactiveSound = SoundPool.FirstOrDefault(s => !s.activeInHierarchy);
      if (inactiveSound != default) return inactiveSound;

      var sound = new GameObject("Sound");
      sound.AddComponent<AudioSource>();
      SoundPool.Add(sound);
      return sound;
    }

    private void ReturnInactiveSoundsToPool()
    {
      SoundPool.ForEach(sound =>
      {
        var audio = sound.GetComponent<AudioSource>();
        var isPlaying = audio.isPlaying;
        if (isPlaying) Invoke(nameof(ReturnInactiveSoundsToPool), InactiveSoundCheckPeriod);
        sound.SetActive(isPlaying);
      });
    }
  }
}
