[System.Serializable]
public class TrashSlot
{
    public ItemData currentItem;
    public bool isEmpty = true;

    public void SetItem(ItemData item)
    {
        currentItem = item;
        isEmpty = false;
    }

    public void ClearSlot()
    {
        currentItem = null;
        isEmpty = true;
    }

    public bool CanAcceptItem()
    {
        return isEmpty;
    }
}