﻿// The MIT License (MIT)

// Copyright 2015 Siney/Pangweiwei siney@yeah.net
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace SLua
{
	using UnityEngine;
	using System.Collections;
	using SLua;
	using System;
	using System.Net;
	using System.Net.Sockets;
	using LuaInterface;
	using System.IO;

	public class LuaSvrGameObject : MonoBehaviour
	{

		public LuaState state;
		public Action onUpdate;
#if UNITY_EDITOR
		public bool skipDebugger = true;
		DebugInterface di;
#endif

		// make sure lua state finalize at last
		// make sure LuaSvrGameObject excute order is max(9999)
		void OnDestroy()
		{
			if (state != null)
			{
#if UNITY_EDITOR
				if (di != null)
				{
					di.close();
					di = null;
				}
#endif

				//state.Dispose();
				//state = null;
			}
		}

		public void init() {
#if UNITY_EDITOR
			di = new DebugInterface(state);
			di.init();
#endif
		}


		void Update()
		{
			if (onUpdate != null) onUpdate();
#if UNITY_EDITOR
			if (di != null) di.update();
#endif
		}

#if UNITY_EDITOR
		void OnGUI()
		{
			if (skipDebugger || di.isStarted)
			{
				skipDebugger = true;
				return;
			}

			int w = Screen.width;
			int h = Screen.height;
			if (!skipDebugger && GUI.Button(new Rect((w - 300) / 2, (h - 100) / 2, 300, 100), "Waiting for debug connection\nPress this button to skip debugging."))
			{
				skipDebugger = true;
			}
		}
#endif


	}
}