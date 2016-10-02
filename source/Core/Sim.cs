//-----------------------------------------------------------------------------
// Sim.cs
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
using System.Collections.Generic;

// TODO: expose TorqueScript and C++ created sim objects to c# so that they can
// be stored as managed objects. Use callbacks and store the references using
// this class.

namespace Torque3D
{
   /// <summary>
   /// A static class that lets us find what simobjects exist.
   /// It holds a global reference to all SimObjects within the Torque3D console
   /// system. This will also be the "manager" to keep objects alive and well
   /// within the garbage collector.
   /// </summary>
   public static class Sim
   {
      /// <summary>
      /// A Dictionary holding simobjects between their native interopt pointer 
      /// and the SimObject managed reference.
      /// </summary>
      private static Dictionary<IntPtr, SimObject> simObjectLookupTable = new Dictionary<IntPtr, SimObject>();

      /// <summary>
      /// Registers a simobject into the managed console reference sim system.
      /// </summary>
      /// <param name="obj">The reference to the object we are registering.</param>
      /// <returns>True if the object registered, false if the object was already registered.</returns>
      public static bool RegisterObject(SimObject obj)
      {
         if (simObjectLookupTable.ContainsValue(obj))
            return false;
         simObjectLookupTable.Add(obj.nativePtr, obj);
         return true;
      }

      /// <summary>
      /// Removes the object from the managed console sim system.
      /// </summary>
      /// <param name="obj">The reference to the object we are removing.</param>
      /// <returns>True if the object was removed from the internal console sim system, false if the object was already not registered.</returns>
      public static bool DeleteObject(SimObject obj)
      {
         if (!simObjectLookupTable.ContainsValue(obj))
            return false;
         simObjectLookupTable.Remove(obj.nativePtr);
         return true;
      }

      /// <summary>
      /// Gets a SimObject reference from the native pointer.
      /// </summary>
      /// <param name="pointer">The native pointer to lookup.</param>
      /// <returns>A SimObject referenced by the pointer, or null if it doesn't exist.</returns>
      public static SimObject Find(IntPtr pointer)
      {
         if (simObjectLookupTable.ContainsKey(pointer))
            return simObjectLookupTable[pointer];
         return null;
      }

      /// <summary>
      /// Gets a SimObject reference by name.
      /// </summary>
      /// <param name="name">The name of the SimObject</param>
      /// <returns>The SimObject reference if it exists, or null if not found.</returns>
      public static SimObject Find(string name)
      {
         foreach (var i in simObjectLookupTable.Values)
         {
            // Todo: Make GetName synced between c++ and c# with callbacks 
            // so we don't have to call the native function for each object?
            if (i.GetName() == name)
               return i;
         }
         return null;
      }

      /// <summary>
      /// Checks if a sim object instance exists.
      /// </summary>
      /// <param name="obj">The object we are checking.</param>
      /// <returns></returns>
      public static bool IsObject(SimObject obj)
      {
         return simObjectLookupTable.ContainsValue(obj);
      }
   }
}
