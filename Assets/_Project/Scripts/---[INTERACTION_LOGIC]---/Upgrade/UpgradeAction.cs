using UnityEngine;

public class UpgradeAction : MonoBehaviour
{
    [HideInInspector] public bool PlayerInTrigger;
    
    [SerializeField] private float _actionDelay = 0.35f;
    [SerializeField] private UpgradeUI _upgradeUI;

    [SerializeField]
    private AudioManager _audioManager;

    [SerializeField]
    private AudioClip _closeAudioClip;
    [SerializeField]
    private AudioClip _openAudioClip;

    private bool _isClosedForCutscene;
    
    private float _triggerStayTimer = 0;

    private bool _isUpgradeOpened = false;

    private void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = GetComponentInChildren<AudioManager>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isClosedForCutscene) return;
        
        if (other.CompareTag(TagConstants.Player) && !_isUpgradeOpened)
        {
            _triggerStayTimer += Time.deltaTime;
            
            if (_triggerStayTimer >= _actionDelay && !PlayerInTrigger)
            {
                Debug.Log("Upgrade Ui open action called");
                _isUpgradeOpened = true;
                _audioManager.PlayAudio(_openAudioClip);
                PlayerInTrigger = true;
                _triggerStayTimer = 0;

                _upgradeUI.Open();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagConstants.Player) && _isUpgradeOpened)
        {
            CloseUpgradeUI(false);
        }
        else if (other.CompareTag(TagConstants.Player))
        {
            _triggerStayTimer = 0;
            PlayerInTrigger = false;
            _upgradeUI.UpgradeView.Priority = -1;

            _isClosedForCutscene = false;
        }
    }
    
    public void CloseUpgradeUI(bool flag)
    {
        Debug.Log("Upgrade Ui close action called");
        _isUpgradeOpened = false;
        _triggerStayTimer = 0;
        _audioManager.Stop();
        _audioManager.PlayAudio(_closeAudioClip);
        _upgradeUI.Close();
        PlayerInTrigger = false;
        
        _isClosedForCutscene = flag;
    }


}
