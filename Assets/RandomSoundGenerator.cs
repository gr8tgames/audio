using Gr8tGames.Audio;
using UnityEngine;

public class RandomSoundGenerator : MonoBehaviour
{
  public SoundDefinition SoundToSpreadAround;
  public SoundDefinition BackgroundMusic;
  public GameObject VisualRepresentation;
  public float MinSpawnTime = 1.0f;
  public float MaxSpawnTime = 2.0f;
  public Vector2 MinPosition;
  public Vector2 MaxPosition;

  private SoundHandler SoundHandler;

  private void Awake()
  {
    SoundHandler = GetComponent<SoundHandler>();
  }

  private void Start()
  {
    PlaySoundSomewhere();
    SoundHandler.Play(BackgroundMusic);
  }

  private void PlaySoundSomewhere()
  {
    var pos = new Vector2(Random.Range(MinPosition.x, MaxPosition.x), Random.Range(MinPosition.y, MaxPosition.y));
    SoundHandler.Play(SoundToSpreadAround, pos);
    var visual = Instantiate(VisualRepresentation, pos, Quaternion.identity);
    Destroy(visual, 1f);
    Invoke(nameof(PlaySoundSomewhere), Random.Range(MinSpawnTime, MaxSpawnTime));
  }
}
