using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Game {
    sealed class GameEcsStartup : MonoBehaviour {
        [SerializeField] private GameSceneDataView sceneData;
        [SerializeField] private StaticData staticData;
        [SerializeField] private ShareDataView shareData;


        EcsWorld world;
        EcsSystems updateSystems;
        EcsSystems fixedUpdateSystems;
        EcsSystems lateUpdateSystems;

        void Start () {
            world = new EcsWorld();
            updateSystems = new EcsSystems(world);
            fixedUpdateSystems = new EcsSystems(world);
            lateUpdateSystems = new EcsSystems(world);

            #region Editor
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixedUpdateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(lateUpdateSystems);
#endif
            #endregion

            updateSystems
                .ConvertScene()
                .Add(new LoadGameSceneSystem())
                ;

            #region Inject and Init Systems

            updateSystems.Inject(sceneData);
            fixedUpdateSystems.Inject(sceneData);
            lateUpdateSystems.Inject(sceneData);

            updateSystems.Inject(shareData);
            fixedUpdateSystems.Inject(shareData);
            lateUpdateSystems.Inject(shareData);

            updateSystems.Inject(staticData);
            fixedUpdateSystems.Inject(staticData);
            lateUpdateSystems.Inject(staticData);

            updateSystems.Init();
            fixedUpdateSystems.Init();
            lateUpdateSystems.Init();

            #endregion
        }

        #region Updates And Destroy
        void Update()
        {
            updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems.Run();
        }

        private void LateUpdate()
        {
            lateUpdateSystems.Run();

        }

        void OnDestroy()
        {
            if (updateSystems != null)
            {
                updateSystems.Destroy();
                updateSystems = null;
            }
            if (fixedUpdateSystems != null)
            {
                fixedUpdateSystems.Destroy();
                fixedUpdateSystems = null;
            }
            if (lateUpdateSystems != null)
            {
                lateUpdateSystems.Destroy();
                lateUpdateSystems = null;
            }
            world.Destroy();
            world = null;
        }
        #endregion
    }
}