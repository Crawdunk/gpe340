using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Drop
{
    public GameObject itemToDrop;
    public int weight;
}

public class ItemDrops : MonoBehaviour
{
    public List<Drop> itemDrops;
    private List<int> cdfArray;

    public void DropRandomItem()
    {
        int rndNum = Random.Range(0, cdfArray[cdfArray.Count - 1]);

        int selectedIndex = System.Array.BinarySearch(cdfArray.ToArray(), rndNum);
        if (selectedIndex < 0)
        {
            selectedIndex = ~selectedIndex;
        }

        Instantiate(itemDrops[selectedIndex].itemToDrop, transform.position, transform.rotation);
    }

    void Start()
    {
        cdfArray = new List<int>();

        for(int i = 0; i < itemDrops.Count; i++)
        {
            if (i == 0)
            {
                cdfArray.Add(itemDrops[i].weight);
            }
            else
            {
                cdfArray.Add(itemDrops[i].weight + cdfArray[i - 1]);
            }
        }
    }


}
