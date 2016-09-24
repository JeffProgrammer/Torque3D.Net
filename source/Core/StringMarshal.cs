//-----------------------------------------------------------------------------
// StringMarshal.cs
//
// Copyright(c) 2016 Jeff Hutchinson
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//-----------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace Torque3D
{
   /// <summary>
   /// A static class to help ease marshalling strings between managed and 
   /// unmanaged code.
   /// 
   /// Some references for marshaling strings:
   /// http://stackoverflow.com/questions/1498931/marshalling-array-of-strings-to-char-in-c-sharp
   /// </summary>
   public static class StringMarshal
   {
      /// <summary>
      /// Marshals an array of strings str into an array of heap allocated strings.
      /// </summary>
      /// <param name="str">An array of strings.</param>
      /// <returns>An array of int pointers to marshal to the native api.</returns>
      public static IntPtr[] StringArrayToIntPtrArray(string[] str)
      {
         int length = str.Length;
         IntPtr[] arr = new IntPtr[length];
         for (int i = 0; i < length; i++)
         {
            arr[i] = Marshal.StringToHGlobalUni(str[i]);
         }
         return arr;
      }

      /// <summary>
      /// Frees an array of heap allocated data stored in an IntPtr array.
      /// </summary>
      /// <param name="arr">The array of int pointers.</param>
      public static void FreeIntPtrArray(IntPtr[] arr)
      {
         int length = arr.Length;
         for (int i = 0; i < length; i++)
         {
            Marshal.FreeHGlobal(arr[i]);
         }
      }
   }
}
