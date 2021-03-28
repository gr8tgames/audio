using Gr8tGames.Audio;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomSoundGenerator : MonoBehaviour
{
  public SoundDefinition SoundToSpreadAround;
  public SoundDefinition BackgroundMusic;
  public GameObject VisualRepresentation;
  public float MinSpawnTime = 1.0f;
  public float MaxSpawnTime = 2.0f;
  public float ChanceToSpawnMultiple = 0.2f;
  public Vector2 MinPosition;
  public Vector2 MaxPosition;

  private SoundHandler SoundHandler;
  private Queue<GameObject> VisualPool;
  private List<(AudioSource audio, GameObject visual)> PlayingAudioSources;

  private void Awake()
  {
    SoundHandler = GetComponent<SoundHandler>();
    VisualPool = new Queue<GameObject>();
    PlayingAudioSources = new List<(AudioSource audio, GameObject visual)>();
  }

  private void Start()
  {
    InvokeRepeating(nameof(PlaySoundSomewhere), MinSpawnTime, MinSpawnTime);
    SoundHandler.Play(BackgroundMusic);
    //InvokeRepeating(nameof(ReclaimVisualizations), 0.1f, 0.1f);
  }

  private void ReclaimVisualizations()
  {
    var playingSources = PlayingAudioSources.Where(source => source.audio.isPlaying);
    var finishedSources = PlayingAudioSources.Where(source => !source.audio.isPlaying);
    foreach (var source in finishedSources)
    {
      source.visual.SetActive(false);
      VisualPool.Enqueue(source.visual);
    }
    PlayingAudioSources = playingSources.ToList();
  }

  private void PlaySoundSomewhere()
  {
    var pos = new Vector2(Random.Range(MinPosition.x, MaxPosition.x), Random.Range(MinPosition.y, MaxPosition.y));
    var audio = SoundHandler.Play(SoundToSpreadAround, pos);
    //var visualRep = GetVisualRepFromPool();
    //visualRep.SetActive(true);
    //visualRep.transform.SetPositionAndRotation(pos, Quaternion.identity);
    //PlayingAudioSources.Add((audio, visualRep));
    if (Random.Range(0f, 1f) < ChanceToSpawnMultiple)
    {
      PlaySoundSomewhere();
    }
  }

  private GameObject GetVisualRepFromPool()
  {
    if (VisualPool.Count > 0) return VisualPool.Dequeue();

    return Instantiate(VisualRepresentation);
  }
}
