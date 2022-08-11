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
            EcsSystems movabelSystems = MovableSystems();
            EcsSystems characterSystems = CharacterSystems();

            _updateSystems
                .Add(new InitGameSystem())
                .Add(inputSystems)
                .Add(spawnSystems)
                .Add(characterSystems)

                .Add(new CharacterAnimationSystem())

                .Add(new LevelProgressSystem())
                .Add(new TimeSystem())
                .Add(new GameVibrationSystem())
                .Add(new MoneyCounterSystem())

                .Inject(_gameData)
                .Inject(_gameUi)

                .OneFrame<MovingCompleteEvent>()
                .OneFrame<ChangeGameStateEvent>()

                .Init();

            _fixedUpdateSystems
                .Add(movabelSystems)

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
            .Add(new SpawnPopcornSystem())
            .OneFrame<CreateNewCharacterRequest>()
            .Add(new SpawnSystem());
        }


        private EcsSystems InputSystems()
        {
            return new EcsSystems(_ecsWorld, "InputSystems")
            .Add(new InputJoystickSystem());
        }

        private EcsSystems MovableSystems()
        {
            return new EcsSystems(_ecsWorld, "MovableSystems")
            .Add(new MovingSystem())
            .Add(new LookAtSystem())
            .Add(new PhysicForceAddSystem())
            .Add(new LandingSystem());
        }

        private EcsSystems CharacterSystems()
        {
            return new EcsSystems(_ecsWorld, "CharacterSystems")
                .Add(new CharacterInitSystem())
                .Add(new CharacterNavigationSystem())
                .Add(new CharacterOnTriggerEnterSystem());
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
