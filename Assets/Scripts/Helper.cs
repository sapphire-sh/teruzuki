using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace teruzuki
{
	public static class Helper
	{
		public static class JsonParser
		{
			public static T[] FromJson<T> (string json)
			{
				Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>> (json);
				return wrapper.items;
			}
			
			public static string ToJson<T> (T[] array)
			{
				Wrapper<T> wrapper = new Wrapper<T> ();
				wrapper.items = array;
				return UnityEngine.JsonUtility.ToJson (wrapper);
			}

			[Serializable]
			private class Wrapper<T>
			{
				public T[] items;
			}
		}
	}
}
