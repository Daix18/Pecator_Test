using UnityEngine;

public class FinishPoint : TriggerInteraction
{
    public enum SpawnPointAt
    {
        //Canitdad de puertas
        None, One, Two, Three, Four, Five, Six,
        Seven, Eight, Nine, Ten, Eleven, Twelve,
        Thirteen, Fourteen, Fifteen, Sixteen, 
        Seventeen, Eighteen, Ninetween, Twenty,
        TwentyOne, TwentyTwo,TwentyThree,
        TwentyFour, TwentyFive, TwentySix,
        TwentySeven, TwentyEight, TwentyNine,
        Thirty, ThirtyOne, ThirtyTwo, ThirtyThree,
        ThirtyFour, ThirtyFive, ThirtySix, ThirtySeven,
        ThityEight, ThirtyNine, Forty, FortyOne, FortyTwo,
        FortyThree, FortyFour, FortyFive, FortySix, 
        FortySeven, FortyEight, FortyNine, Fifty, FiftyOne, 
        FiftyTwo, FiftyThree, FiftyFour, FiftyFive, FiftySix,
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
