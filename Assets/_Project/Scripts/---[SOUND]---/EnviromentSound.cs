using UnityEngine;

public class EnviromentSound : MonoBehaviour
{
    [SerializeField] 
    private float _actionDelay = 0.35f;

    [SerializeField]
    internal IAudioManager _audioManager;

    internal bool _playerInTrigger;
    private float _triggerStayTimer = 0;

    private void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = GetComponentInChildren<IAudioManager>();
        }         
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsTriggered(other))
        {
            _triggerStayTimer += Time.deltaTime;

            if (_triggerStayTimer >= _actionDelay)
            {
                _playerInTrigger = true;
                _triggerStayTimer = 0;
                Action();
            }
        }
    }

    public virtual bool IsTriggered(Collider other)
    {
        return other.CompareTag(TagConstants.Player) && !_playerInTrigger;
    }
    
    public virtual void Action()
    {
        _audioManager.PlayPredefinedAudioSeriesCycled();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagConstants.Player))
        {
            _triggerStayTimer = 0;
            _playerInTrigger = false;
            _audioManager.Pause();
        }
    }
}
