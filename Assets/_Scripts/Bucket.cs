using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public List<ItemData> itemsInBucket;

    public int totalCollected()
    {
        if (itemsInBucket.Count <= 0)
        {
            return 0;
        } else
        {
            return itemsInBucket.Count;
        }
    }

    public ItemData getItem(int index)
    {
        return itemsInBucket[index];
    }


}
