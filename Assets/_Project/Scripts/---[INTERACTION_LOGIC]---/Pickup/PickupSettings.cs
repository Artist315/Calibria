using UnityEngine;

public class PickupSettings : MonoBehaviour
{
    [SerializeField] private PickupSO _pickupSettings;

    private OrderPickupController _pickupController;

    private void Awake()
    {
        _pickupController = GetComponent<OrderPickupController>();

        _pickupController.MoneyReward = _pickupSettings.MoneyReward;
    }
}
