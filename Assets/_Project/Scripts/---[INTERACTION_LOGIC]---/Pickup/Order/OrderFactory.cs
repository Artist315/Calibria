using System.Collections.Generic;
using UnityEngine;

public static class OrderFactory
{
    public static PickupsEnum RandomOrderItem(List<PickupsEnum> OrderTypes, List<float> OrderTypeDropRate)
    {
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.KitchenUpgrade) == 0)
        {
            int pastaIndex = OrderTypes.IndexOf(PickupsEnum.Pasta);

            OrderTypes.Remove(PickupsEnum.Pasta);
            OrderTypeDropRate.RemoveAt(pastaIndex);
        }
        
        PickupsEnum orderItem = OrderTypes[ChooseRecourseType(OrderTypeDropRate)];

        return orderItem;
    }

    private static int ChooseRecourseType(List<float> OrderTypeDropRate)
    {
        float totalRand = 0;
        
        for (int i = 0; i < OrderTypeDropRate.Count; i++)
        {
            totalRand += OrderTypeDropRate[i];
        }

        float randVal = Random.Range(0, totalRand);

        for (int i = 0; i < OrderTypeDropRate.Count; i++)
        {
            if (randVal < OrderTypeDropRate[i])
            {
                return i;
            }
            randVal -= OrderTypeDropRate[i];
        }
        
        return -1;
    }
}


