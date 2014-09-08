using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ItemCollection = System.Windows.Forms.ListView.ListViewItemCollection;

namespace ChatR.ChatClient
{
    public static class ListViewHelpers
    {
        public static void Append(this ItemCollection collection, ListViewItem item)
        {
            // Adds item to the listview second to last so that it is not cut off
            // Depends on an empty item being in the last place
            collection.Insert(collection.Count - 1, item);
            EnsureLastItemVisible(collection);
        }

        public static void Append(this ItemCollection collection, string s)
        {
            // Adds item to the listview second to last so that it is not cut off
            // Depends on an empty item being in the last place
            collection.Insert(collection.Count - 1, new ListViewItem(s));
            EnsureLastItemVisible(collection);
        }

        public static void Append(this ItemCollection collection, string key, string s, string imageKey)
        {
            // Adds item to the listview second to last so that it is not cut off
            // Depends on an empty item being in the last place
            collection.Insert(collection.Count - 1, key, s, imageKey);
            EnsureLastItemVisible(collection);

        }

        static void EnsureLastItemVisible(ItemCollection collection)
        {
            var item = collection[collection.Count - 2];
            var bottom = item.ListView.Bottom;
            var top = item.Bounds.Top;
            if (top < bottom)
            {
                collection[collection.Count - 1].EnsureVisible();
            }
        }
    }
}
