using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(ParticleSystem))]
    public class BallVFX : MonoBehaviour
    {
        public MeshRenderer _meshRenderer;
        public ParticleSystem ParticleSystem;

        private void Start()
        {
            ParticleSystem.Stop();
        }

        public void SetBallColor() =>
            _meshRenderer.material.color =
                gameObject.GetComponent<MeshRenderer>().material.color;

        public void PlayHitEffect() =>
            ParticleSystem.Emit(1);
    }
}