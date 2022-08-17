using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

namespace Client
{
    sealed class Game : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;

        [Header("Data")]
        [SerializeField] private GameData _gameData;

        [Header("UI")]
        [SerializeField] private GameUI _gameUi;

        private IEnumerator Start()
        {
            _ecsWorld = new EcsWorld();
            _updateSystems = new EcsSystems(_ecsWorld, " - UPDATE");
            _fixedUpdateSystems = new EcsSystems(_ecsWorld, " - FIXED UPDATE");

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems);
#endif

            SetTargetFrameRate();
            _gameUi.InjectEcsWorld(_ecsWorld);
            ProvideMonoEntitiesFromScene();

            EcsSystems inputSystems = InputSystems();
            EcsSystems spawnSystems = SpawnSystems();
            EcsSystems moveSystems = MoveSystems();
            EcsSystems popSystems = PopSystems();

            //.Add(characterSystems)
            _updateSystems
                .Add(new InitGameSystem())

                .Add(new ConveyorInitSystem())
                .Add(spawnSystems)
                .Add(inputSystems)
                .Add(popSystems)

                .Add(new LevelProgressSystem())
                .Add(new TimerSystem())
                .Add(new GameVibrationSystem())
                .Add(new MoneyCounterSystem())

                .OneFrame<MovingCompleteEvent>()
                .OneFrame<ChangeGameStateEvent>()
                .OneFrame<PopCookingDoneEvent>()

                .Inject(_gameData)
                .Inject(_gameUi)

                .Init();

            _fixedUpdateSystems
                .Add(new PopTriggerSystem())
                .Add(moveSystems)

                .OneFrame<OnCollisionEnterEvent>()
                .OneFrame<OnTriggerEnterEvent>()
                .OneFrame<OnTriggerExitEvent>()

                .Inject(_gameData)

                .Init();

            yield return null;
        }

        void Update()
        {
            _updateSystems?.Run();
        }

        void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;

                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;

                _ecsWorld.Destroy();
                _ecsWorld = null;
            }
        }

        //------------------SYSTEM GROUPS---------------

        private EcsSystems SpawnSystems()
        {
            return new EcsSystems(_ecsWorld, "SpawnSystems")
            .Add(new DestroyGameObjectSystem())

            .Add(new PopSpawnSystem())
            .Add(new ObjectPoolSystem())
            .Add(new DespawnSystem())
            ;
            //.Add(new SpawnSystem());
        }


        private EcsSystems InputSystems()
        {
            return new EcsSystems(_ecsWorld, "InputSystems")
            .Add(new InputJoystickSystem());
        }

        private EcsSystems MoveSystems()
        {
            return new EcsSystems(_ecsWorld, "MovableSystems")
            .Add(new VelocityMovingSystem())
            .Add(new TransformMovingSystem())
            .Add(new LookingAtSystem())
            .Add(new PhysicForceAddSystem())
            .Add(new LandingSystem());
        }

        private EcsSystems PopSystems()
        {
            return new EcsSystems(_ecsWorld, "PopSystems")

                .Add(new PopInitSystem())
                .Add(new PopLaunchSystem())
                .Add(new PopCookingSystem())
                .Add(new PopPopingSystem())
                .Add(new PopGoToJumpSystem())
                .Add(new PopPrepareToJumpSystem())
                .Add(new PopJumpSystem())

                .Add(new PopCounterSystem())
                .Add(new PopViewHandlerSystem())
                .Add(new PopAnimationSystem());
        }

        private EcsSystems CoreGameplaySystems()
        {
            return new EcsSystems(_ecsWorld);
            //.OneFrame<OnObstacleCollisionEvent>()
            //.Add(new ObstacleCollisionCheckerSystem())
            //.OneFrame<DeadEvent>()
            //.Add(new DeadByObstacleCollisionSystem())
            //.Add(new DeadCheckerGameplaySystem());
        }

        private static void SetTargetFrameRate() => Application.targetFrameRate = 60;

        private void ProvideMonoEntitiesFromScene()
        {
            foreach (var monoEntity in _gameData.SceneData.MonoEntities)
            {
                EcsEntity ecsEntity = _ecsWorld.NewEntity();
                monoEntity.Make(ref ecsEntity);
            }
        }
    }
}
