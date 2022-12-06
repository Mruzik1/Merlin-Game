using System.Collections;
using Merlin2d.Game.Items;


namespace MyGame.Actors.Items
{
    public class Backpack : AbstractActor, IInventory
    {
        private List<IItem> items;

        public Backpack(int capacity)
        {
            items = new List<IItem>(capacity);
        }

        public override void Update()
        {
            
        }

        public IItem GetItem()
        {
            return items[0];
        }

        public void AddItem(IItem item)
        {
            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    return;
                }
            }

            throw new FullInventoryException("The backpack is full!");
        }

        public int GetCapacity()
        {
            return items.Count;
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

        }

        public void ShiftRight()
        {
            
        }

        public void RemoveItem(IItem item)
        {
            items.Remove(item);
        }

        public void RemoveItem(int idx)
        {
            items.RemoveAt(idx);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}