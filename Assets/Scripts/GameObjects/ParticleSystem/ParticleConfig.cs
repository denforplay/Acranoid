using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.ParticleSystem
{
    [CreateAssetMenu]
    public class ParticleConfig : ScriptableObject
    {
        public List<ParticleBase> particles;
    }
}
