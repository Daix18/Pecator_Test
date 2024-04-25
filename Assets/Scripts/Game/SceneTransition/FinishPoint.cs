using UnityEngine;

public class FinishPoint : TriggerInteraction
{
    public enum SpawnPointAt
    {
        None,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
        Sixteen,
        Seventeen,
        Eighteen,
    }

    [Header("Spawn To")]
    [SerializeField] private SpawnPointAt spawnPointAt;
    [SerializeField] private SceneField _sceneToLoad;

    [Space(10f)]
    [Header("This Finish Point")]
    public SpawnPointAt CurrentFinishPointPosition;

    public override void Interact()
    {
        SceneSwapController.SwapScene(_sceneToLoad, spawnPointAt);
    }
}
