using Assets.Scripts.Abstracts.Repository;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraResizeRepository : Repository
    {
        private const float SIZE_X = 1920.0f;
        private const float SIZE_Y = 1080.0f;
        private const bool IS_HORIZONTAL = true;
        
        public float HalfSize { get => 200.0f; }
        public UnityEngine.Camera camera { get; private set; }
        public float TargetSizeX { get; set; }
        public float TargetSizeY { get; set; }

        public override void Initialize()
        {
            camera = UnityEngine.Camera.main;
            TargetSizeX = IS_HORIZONTAL ? SIZE_X : SIZE_Y;
            TargetSizeY = IS_HORIZONTAL ? SIZE_Y : SIZE_X;
        }

        public override void OnCreate()
        {
        }

        public override void OnStart()
        {
        }

        public override void Save()
        {
        }
    }
}
