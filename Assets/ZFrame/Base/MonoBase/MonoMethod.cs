﻿using System;
using UnityEngine;

namespace ZFrame.Base.MonoBase
{
	[Serializable]
	public class MonoMethod : ZMethod
	{
		public new Component target;
		public new string method;

		public static Delegate CreateDelegate(Type type, MonoMethod monoMethod)
		{
			if (monoMethod.target == null || string.IsNullOrEmpty(monoMethod.method))
			{
				return null;
			}

			return Delegate.CreateDelegate(type, monoMethod.target, monoMethod.method, false, false);
		}

		public static Delegate CreateDelegate<T>(MonoMethod monoMethod)
		{
			return CreateDelegate(typeof (T), monoMethod);
		}

		public MonoMethod(Component target, string methodName)
			: base(target, null)
		{
			this.target = target;
			method = methodName;
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}.{2}", target.name, target.GetType(), method ?? "NULL");
		}
	}

	public class ZMethod
	{
		public object target;
		public Delegate method;

		public ZMethod(object target, Delegate del)
		{
			this.target = target;
			method = del;
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", target, method.Method);
		}
	}
}