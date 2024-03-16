using UnityEngine;

public class EnviromentSound : MonoBehaviour
{
    [SerializeField] 
    private float _actionDelay = 0.35f;

    [SerializeField]
    private IAudioManager _audioManager;

    internal bool _playerInTrigger;
    private float _triggerStayTimer = 0;

    private void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = GetComponentInChildren<AudioManager>();
        }         
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsTriggered(other))
        {
            _triggerStayTimer += Time.deltaTime;

            if (_triggerStayTimer >= _actionDelay)
            {
                Debug.Log("Sound Played!!!!");
                _playerInTrigger = true;
                _triggerStayTimer = 0;

                _audioManager.PlayPredefinedAudioSeriesCycled();
            }
        }
    }

    public virtual bool IsTriggered(Collider other)
    {
        return other.CompareTag(TagConstants.Player) && !_playerInTrigger;
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
