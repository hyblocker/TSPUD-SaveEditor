using System;
using Newtonsoft.Json;

namespace UnityEngine
{
	/// <summary>
	/// A poly fill for Unity's JsonUtility
	/// </summary>
    public static class JsonUtility
    {
		public static string ToJson(object obj)
		{
			return JsonUtility.ToJson(obj, false);
		}

		public static string ToJson(object obj, bool prettyPrint)
		{
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = JsonConvert.SerializeObject(obj, prettyPrint ? Formatting.Indented : Formatting.None);
			}
			return result;
		}

		public static T FromJson<T>(string json)
		{
			return (T)FromJson(json, typeof(T));
		}

		public static object FromJson(string json, Type type)
		{
			bool flag = string.IsNullOrEmpty(json);
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = type == null;
				if (flag2)
				{
					throw new ArgumentNullException("type");
				}
				bool flag3 = type.IsAbstract || type.IsSubclassOf(typeof(object));
				if (flag3)
				{
					throw new ArgumentException("Cannot deserialize JSON to new instances of type '" + type.Name + ".'");
				}
				result = JsonConvert.DeserializeObject(json);
			}
			return result;
		}
	}
}
