using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Camera;
using System;
using UnityEngine;
using Assets.Scripts.Abstracts.Game;
namespace Assets.Scripts.Camera
{
    public class CameraResizeController : Controller
    {
        private CameraResizeRepository _cameraResizeRepository;
        public override void OnCreate()
        {
            this._cameraResizeRepository = Game.GetRepository<CameraResizeRepository>();
        }

        public override void Initialize()
        {
            _cameraResizeRepository.Initialize();
            CameraResize();
        }

        private void CameraResize()
        {
            float screenRatio = (float)Screen.width / Screen.height;
            float targerRatio = this._cameraResizeRepository.TargetSizeX / this._cameraResizeRepository.TargetSizeY;

            if (screenRatio >= targerRatio)
            {
                Resize();
            }
            else
            {
                float differentSize = targerRatio / screenRatio;
                Resize(differentSize);
            }
        }

        private void Resize(float differentSize = 1.0f)
        {
            float newOrthographicSize = this._cameraResizeRepository.TargetSizeY / this._cameraResizeRepository.HalfSize * differentSize;
            this._cameraResizeRepository.camera.orthographicSize = newOrthographicSize;
        }

        public override void OnStart()
        {
        }
    }
}
