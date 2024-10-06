// Allows all GameObjects that are children of this object persist between Scene loads
public class ScenePersist : PersistentSingleton<ScenePersist>
{
    // Reloads the ScenePersist GameObject as it is at the start of a fresh Scene instance.
    // Used to load up the ScenePersist with a new Scene's GameObjects or to reset on Game Over.
    public void ResetScene() => Destroy(gameObject);
}