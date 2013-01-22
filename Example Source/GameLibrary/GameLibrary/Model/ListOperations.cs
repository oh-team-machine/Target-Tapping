using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary
{
    /// <summary>
    /// This class enables various functions for lists of objects.
    /// </summary>
    public sealed class ListOperations
    {
        private ListOperations() { }

        /// <summary>
        /// Randomly shuffles a list of objects, much like shuffling a deck of cards.
        /// </summary>
        /// <param name="listToShuffle">The list of objects to be shuffled</param>
        /// <param name="shuffles">The number of times to shuffle the list</param>
        /// <returns></returns>
        public static List<T> ShuffleList<T>(List<T> listToShuffle, int shuffles)
        {
            Random rand = new Random();
            int index;
            List<T> sortOrderList = new List<T>();

            for (int i = 0; i < shuffles; i++)
            {
                while (listToShuffle.Count > 0)
                {
                    index = rand.Next(listToShuffle.Count);

                    sortOrderList.Add(listToShuffle[index]);

                    listToShuffle.RemoveAt(index);
                }

                listToShuffle = sortOrderList;
                sortOrderList = new List<T>();
            }

            return listToShuffle;
        }
    }
}
