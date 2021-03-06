using System.Collections.Generic;
using UnityEngine;

public class AudioDetection : MonoBehaviour, IMessageSender
{
    [SerializeField] private float _detectionRange = 10.0f;
    [SerializeField] private float _alertnessReductionPerSecond = 2.5f;
    [SerializeField] private float _startReducingCooldown = 1.0f;
    [SerializeField] private float _alertnessThreshold = 100;

    private float _alertness = 0;
    private float _reductionCooldownTimer;
    private LayerMask _checkLayer;

    public void SendMessage()
    {

    }

    private void Awake()
    {
        _checkLayer = (LayerMask.NameToLayer("Wall"));
    }

    private void Update()
    {
        if (_alertness > 0)
            _reductionCooldownTimer -= Time.deltaTime;

        if (_reductionCooldownTimer <= 0 && _alertness > 0)
        {
            _alertness -= _alertnessReductionPerSecond * Time.deltaTime;
        }

        if (_alertness < 0)
            _alertness = 0;
        if (_alertness > _alertnessThreshold)
            _alertness = _alertnessThreshold;
    }

    public bool ThresholdReached()
    {
        if (_alertness >= _alertnessThreshold)
            return true;
        else
            return false;
    }

    public bool ProcessSound(SoundEmission sound)
    {
        float distance = Vector3.Distance(transform.position, sound.Position);

        if (distance <= _detectionRange)
        {
            Vector3 direction = sound.Position - transform.position;
            RaycastHit hit;


            Debug.DrawRay(transform.position, direction, Color.blue, 1);
            DebugEx.DrawHitMark(sound.Position, Color.white);
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                float distanceToSound = Vector3.Distance(transform.position, sound.Position);
                float distanceToRayHit = Vector3.Distance(transform.position, hit.point);
                float heardVolume = sound.Volume;
                if (sound.DistanceFalloff != 0)
                    heardVolume -= (distance /sound.DistanceFalloff) * sound.Volume;

            
                if (hit.collider.gameObject.layer != _checkLayer)
                {
                    HeardSound(heardVolume);
                    return true;
                }
                else if (distanceToRayHit > distanceToSound)
                {
                    HeardSound(sound.Volume);
                    return true;
                }
            }
            else
            {
                HeardSound(sound.Volume);
                return true;
            }

            return false;
        }

        return false;
    }

    private void HeardSound(float volume)
    {
        _alertness += volume;
        _reductionCooldownTimer = _startReducingCooldown;
    }

    private void OnDrawGizmosSelected()
    {
        DebugEx.DrawCircle(transform.position, _detectionRange, Color.yellow);
    }

    public float Alertness { get => _alertness; set => _alertness = value; }
}
