using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSound : MonoBehaviour
{
    [SerializeField] 
    private float _actionDelay = 0.35f;

    [SerializeField]
    private AudioManager _audioManager;

    private bool _playerInTrigger;
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
        if (other.CompareTag(TagConstants.Player) && !_playerInTrigger)
        {
            _triggerStayTimer += Time.deltaTime;

            if (_triggerStayTimer >= _actionDelay)
            {
                _playerInTrigger = true;
                _triggerStayTimer = 0;

                _audioManager.PlayPredefinedAudioSeriesCycled();
            }
        }
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
