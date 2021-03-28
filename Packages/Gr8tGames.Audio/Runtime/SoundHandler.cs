using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gr8tGames.Audio
{
  public class SoundHandler : MonoBehaviour
  {
    private GameObject SoundPrefab;
    private GameObject SoundParent;
    private List<AudioSource> SoundPool;

    private void Awake()
    {
      SoundPrefab = new GameObject("SoundPrefab", typeof(AudioSource));
      SoundPrefab.hideFlags = HideFlags.HideAndDontSave;
      SoundParent = new GameObject("Sound");
      SoundParent.transform.SetParent(gameObject.transform);
      SoundPool = new List<AudioSource>();
    }

    public AudioSource Play(SoundDefinition sound, Vector3? position = null)
    {
      var pos = position ?? Vector3.zero;
      var audio = GetSoundFromPool();
      var soundGameObject = audio.gameObject;
      soundGameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
      soundGameObject.name = sound.Clip.name;
      audio.pitch = sound.Pitch;
      audio.loop = sound.IsLoop;
      audio.spatialBlend = sound.SpatialBend;
      audio.outputAudioMixerGroup = sound.Output;
      if(sound.IsLoop)
      {
        audio.clip = sound.Clip;
        audio.volume = sound.Volume;
        audio.Play();
      }
      else
      {
        audio.PlayOneShot(sound.Clip, sound.Volume);
      }
      return audio;
    }

    public void Stop(AudioSource audio)
    {
      audio.Stop();
    }

    private AudioSource GetSoundFromPool()
    {
      var inactiveSound = SoundPool.FirstOrDefault(s => !s.isPlaying);
      if (inactiveSound != default) return inactiveSound;

      var newSound = Instantiate(SoundPrefab, SoundParent.transform);
      var audio = newSound.GetComponent<AudioSource>();
      SoundPool.Add(audio);
      return audio;
    }
  }
}
