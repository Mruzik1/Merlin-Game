using System.Collections;
using Merlin2d.Game.Items;


namespace MyGame.Actors.Items
{
    public class Backpack : AbstractActor, IInventory
    {
        private List<IItem> items;
        private int itemsCount;

        public Backpack(int capacity)
        {
            items = Enumerable.Repeat<IItem>(null, capacity).ToList();
            itemsCount = 0;
        }

        public override void Update() {}

        public IItem GetItem()
        {
            return items[0];
        }

        public void AddItem(IItem item)
        {
            for (int i = 0; i < items.Capacity; ++i)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    itemsCount++;
                    return;
                }
            }

            Console.Error.WriteLine($"Warning: {new FullInventoryException("The backpack is full!")}");
        }

        public int GetCapacity()
        {
            return items.Capacity;
        }

        public IEnumerator<IItem> GetEnumerator()
        {
            foreach (IItem item in items)
            {
                if (item != null)
                    yield return item;
                else 
                    yield break;
            }
        }

        public void ShiftLeft()
        {
            List<IItem> tmpItems = new List<IItem>(items);

            for (int i = 0; i < itemsCount; ++i)
            {
                int newIdx = (i <= 0) ? itemsCount-1 : i-1;
                tmpItems[newIdx] = items[i];
            }

            items = tmpItems;
        }

        public void ShiftRight()
        {
            List<IItem> tmpItems = new List<IItem>(items);

            for (int i = 0; i < itemsCount; ++i)
            {
                int newIdx = (i >= itemsCount-1) ? 0 : i+1;
                tmpItems[newIdx] = items[i];
            }

            items = tmpItems;
        }

        public void RemoveItem(IItem item)
        {
            RemoveItem(items.IndexOf(item));
        }

        public void RemoveItem(int idx)
        {
            if (idx >= items.Capacity || items[idx] == null)
                return;

            items[idx] = null;
            
            // shift all items
            for (int i = idx; i < itemsCount; ++i)
            {
                items[i] = (i+1 != itemsCount) ? items[i+1] : null;
            }

            itemsCount--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}