using Leopotam.Ecs;
using UnityEngine;

public class CoinsPool : MonoBehaviour
{
    private EcsWorld _world;

    public void InjectWorld(EcsWorld world)
    {
        _world = world;
    }

    public void Spawn(SpawnPrefab spawnPrefab)
    {
        GameObject _go = Instantiate(spawnPrefab.Prefab, spawnPrefab.Position, spawnPrefab.Rotation, spawnPrefab.Parent);
        var monoEntity = _go.GetComponent<MonoEntity>();
        if (monoEntity == null)
            return;
        EcsEntity ecsEntity = _world.NewEntity();
        monoEntity.Make(ref ecsEntity);
    }
}
