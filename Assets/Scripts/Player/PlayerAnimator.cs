using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// VERY primitive animator example.
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Animator _anim;

        [SerializeField] private SpriteRenderer _sprite;

        [Header("Settings")] [SerializeField, Range(1f, 3f)]
        private float _maxIdleSpeed = 2;

        [SerializeField] private float _maxTilt = 5;
        [SerializeField] private float _tiltSpeed = 20;
        [SerializeField] private float _turnAnimDelay = 0.5f;

        [Header("Particles")] [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles;
        [SerializeField] private ParticleSystem _landParticles;

        [Header("Audio Clips")] [SerializeField]
        private AudioClip[] _footsteps;

        private AudioSource _source;
        private IPlayerController _playerController;
        private bool _grounded;
        private float _facingDir;

        private ParticleSystem.MinMaxGradient _currentGradient;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _playerController = GetComponentInParent<IPlayerController>();
        }

        private void OnEnable()
        {
            _playerController.Jumped += OnJumped;
            _playerController.Running += OnStartRunning;
            _playerController.StopRunning += OnStopRunning;
            _playerController.GroundedChanged += OnGroundedChanged;

            _moveParticles.Play();
        }

        private void OnDisable()
        {
            _playerController.Jumped -= OnJumped;
            _playerController.Running -= OnStartRunning;
            _playerController.StopRunning -= OnStopRunning;
            _playerController.GroundedChanged -= OnGroundedChanged;

            _moveParticles.Stop();
        }

        private void Update()
        {
            if (_playerController == null) return;

            DetectGroundColor();

            HandleSpriteFlip();
            HandleCharacterTurning();

            HandleIdleSpeed();

            HandleCharacterTilt();

            UpdateGrounded();
        }

        private void HandleIdleSpeed()
        {
            // Idle
            var inputStrength = Mathf.Abs(_playerController.FrameInput.x);
            _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, inputStrength));
            _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);

        }

        #region - Horizontal -

        #region - Turning -

        private bool _canTurnAnim = false; // Enables the turning animation check
        private bool _turned = false; // The player Turned
        private float _prevFacingDir = 0.0f; // Remembers the previous direction

        private float _turnAnimTime = 0.0f;

        private void HandleSpriteFlip()
        {
            _facingDir = _playerController.FrameInput.x;
            if (_facingDir != 0) _sprite.flipX = _playerController.FrameInput.x < 0;
            
            if(_facingDir != _prevFacingDir)
            {
                _prevFacingDir = _facingDir;
                _turned = true;
            }
        }

        private void HandleCharacterTurning()
        {
            _turnAnimTime -= Time.deltaTime;
            //Debug.Log(_turnAnimTime);
            if(_turnAnimTime < 0.0f)
            {
                _turnAnimTime = 0.0f;
                _canTurnAnim = false;
                _anim.SetBool(TurnKey, false);
            }

            if (_canTurnAnim && _turned)
            {
                _anim.SetBool(TurnKey, true);
                _turned = false;
            }
        }

        // End - Turning -
        #endregion




        private void OnStopRunning()
        {
            _anim.SetTrigger(IdleKey);
            _anim.ResetTrigger(RunKey);
            _canTurnAnim = true;
            _turned = false;
        }

        private void OnStartRunning()
        {
            _anim.SetTrigger(RunKey);
            _anim.ResetTrigger(IdleKey);
            if(_turned)
            {
                _turnAnimTime = _turnAnimDelay;
            }
        }


        private void HandleCharacterTilt()
        {
            //Debug.Log(_playerController.FrameInput.x);
            var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * -_playerController.FrameInput.x) : Quaternion.identity;
            _anim.transform.up = Vector3.RotateTowards(_anim.transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
        }

        // End - Horizontal -
        #endregion

        #region - Jump/Grounded -

        private void OnJumped()
        {
            // Jump
            _anim.SetTrigger(JumpKey);

            if (_grounded) // Avoid coyote
            {
                SetColor(_jumpParticles);
                SetColor(_launchParticles);
                _jumpParticles.Play();
            }
        }

        private void OnGroundedChanged(bool grounded, float impact)
        {
            _grounded = grounded;

            if (grounded)
            {
                DetectGroundColor();
                SetColor(_landParticles);
                if(_footsteps.Length > 0)
                    _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
                _moveParticles.Play();

                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
                _landParticles.Play();
            }
            else
            {
                _moveParticles.Stop();
            }
        }
        private void UpdateGrounded()
        {
            _anim.SetBool(GroundedKey, _grounded);
        }


        #endregion

        #region - Ground Colour -

        private void DetectGroundColor()
        {
            var hit = Physics2D.Raycast(transform.position, Vector3.down, 2);

            if (!hit || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r)) return;
            var color = r.color;
            _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
            SetColor(_moveParticles);
        }

        private void SetColor(ParticleSystem ps)
        {
            var main = ps.main;
            main.startColor = _currentGradient;
        }

        #endregion

        // Trigger Name
        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        private static readonly int RunKey = Animator.StringToHash("Run");
        private static readonly int IdleKey = Animator.StringToHash("Idle");
        private static readonly int TurnKey = Animator.StringToHash("Turn");
    }
}