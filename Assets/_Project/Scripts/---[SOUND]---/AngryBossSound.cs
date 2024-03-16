using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AngryBossSound : EnviromentSound
{
    public override bool IsTriggered(Collider other)
    {
        if (!other.CompareTag(TagConstants.Player))
        {
            return false;
        }

        if (_playerInTrigger)
        {
            return false;
        }

        var pickupAction = other.gameObject.GetComponent<PickupAction>();
        if (pickupAction != null && pickupAction.GarbageColl.Any())
        {
            return true;
        }
        return false;
    }
}
