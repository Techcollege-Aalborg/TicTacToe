using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public static class EnumLook
    {
        /// <summary>
        /// Go to the next value in an ENUM data type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns>Return the value of the next ENUM value</returns>
        public static T Next<T>(this T src) where T : struct
        {
            // Throw an exeption if the type provided is not of type ENUM
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
            }
            
            // Gets all the values in the ENUM
            T[] Arr = (T[])Enum.GetValues(src.GetType());

            // Goes to the index of the current value and adds one to that
            int j = Array.IndexOf<T>(Arr, src) + 1;

            // Return the value of the next in the ENUM
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }
}
