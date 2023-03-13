using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace TarodevController {
    /// <summary>
    /// Game Feel, effects, animation and audio
    /// </summary>
    public class PlayerAnimator : MonoBehaviour {

        [Header("Dependencies")]
        [SerializeField] private Animator _anim;
        [SerializeField] private AudioSource _source;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private ParticleSystem _jumpParticles, _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles, _landParticles;
        [SerializeField] private AudioClip[] _footsteps;
        [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxParticleFallSpeed = -40;

        private IPlayerController _player;
        private bool _beganMoving = false;
        private bool _playerGrounded;
        private bool _currentlyIdling = false;
        private ParticleSystem.MinMaxGradient _currentGradient;
        private Vector2 _movement;

        void Awake() => _player = GetComponentInParent<IPlayerController>();

        void Update() {
            if (_player == null) return;

            // Flip the sprite
            if (_player.Input.X != 0) transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);

            // check if we JUST started moving
            if (_player.Input.X != 0 && _beganMoving == false && _player.Grounded)
            {
                _beganMoving = true;
                Debug.Log("Began Moving");
                _anim.Play(RunningKey);

            } 
            // check if we JUST stopped moving
            else if(_player.Input.X == 0 && _beganMoving && _player.Grounded)
            {
                _beganMoving = false;
                Debug.Log("Stopped Moving");
                _anim.Play(IdleKey);
            }

            //TODO handle 'falling animation' without jump

            // Splat
            if (_player.LandingThisFrame) {
                // _anim.SetTrigger(GroundedKey);
                Debug.Log("Landed");
                _anim.Play(IdleKey);
                _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
            }

            // Jump effects
            if (_player.JumpingThisFrame) {

                //_anim.SetTrigger(JumpKey);
                //_anim.ResetTrigger(GroundedKey);
                Debug.Log("Jumped");
                _anim.Play(JumpKey);

                // Only play particles when grounded (avoid coyote)
                if (_player.Grounded) {
                    SetColor(_jumpParticles);
                    SetColor(_launchParticles);
                    _jumpParticles.Play();
                }
            }

            // Play landing effects and begin ground movement effects
            if (!_playerGrounded && _player.Grounded) {
                _playerGrounded = true;
                _moveParticles.Play();
                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, _maxParticleFallSpeed, _movement.y);
                SetColor(_landParticles);
                _landParticles.Play();
            }
            else if (_playerGrounded && !_player.Grounded) {
                _playerGrounded = false;
                _moveParticles.Stop();
            }

            // Detect ground color
            var groundHit = Physics2D.Raycast(transform.position, Vector3.down, 2, _groundMask);
            if (groundHit && groundHit.transform.TryGetComponent(out SpriteRenderer r)) {
                _currentGradient = new ParticleSystem.MinMaxGradient(r.color * 0.9f, r.color * 1.2f);
                SetColor(_moveParticles);
            }

            _movement = _player.RawMovement; // Previous frame movement is more valuable
        }

        private void OnDisable() {
            _moveParticles.Stop();
        }

        private void OnEnable() {
            _moveParticles.Play();
        }

        void SetColor(ParticleSystem ps) {
            var main = ps.main;
            main.startColor = _currentGradient;
        }


        #region Animation Keys

        private static readonly int IdleKey = Animator.StringToHash("Idle");
        private static readonly int RunningKey = Animator.StringToHash("Running");
        private static readonly int JumpKey = Animator.StringToHash("Jump");


        #endregion
    }
}