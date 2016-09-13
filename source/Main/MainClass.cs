//-----------------------------------------------------------------------------
// MainClass.cs
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

namespace Torque3D {
	struct NativeMain {
#if DEBUG
		public const string dll = "Torque3D_DEBUG";
#else
		public const string dll = "Torque3D";
#endif

		[DllImport(dll, EntryPoint = "TorqueMain")]
		public static extern int TorqueMain(int argc, IntPtr[] argv);
	}

	class MainClass {
		static int Main(string[] args) {
			// Marshal the strings from c# to native.
			IntPtr[] argv = StringMarshal.StringArrayToIntPtrArray(args);

			// Invoke Torque's main method.
			int ret = NativeMain.TorqueMain(args.Length, argv);

			// Free the marshaled string as it resizes on the heap.
			// the GC will not get it.
			StringMarshal.FreeIntPtrArray(argv);

			return ret;
		}
	}
}
