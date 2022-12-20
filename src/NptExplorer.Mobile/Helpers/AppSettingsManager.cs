using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace NptExplorer.Mobile.Helpers
{
	public class AppSettingsManager
	{
		private static AppSettingsManager _instance;
		private readonly JObject _secrets;
 
		private const string Namespace = "NptExplorer.Mobile";
		private const string FileName = "appsettings.json";
 
		private AppSettingsManager()
		{
			try
			{
				var assembly = typeof(AppSettingsManager).GetTypeInfo().Assembly;
				var stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
				if (stream == null)
				{
					Debug.WriteLine("Unable to load secrets file");
					return;
				}

				using var reader = new StreamReader(stream);
				var json = reader.ReadToEnd();
				_secrets = JObject.Parse(json);
			}
			catch
			{
				Debug.WriteLine("Unable to load secrets file");
			}
		}
 
		public static AppSettingsManager Settings => _instance ??= new AppSettingsManager();

		public string this[string name]
		{
			get
			{
				try
				{
					var path = name.Split(':');
 
					JToken node = _secrets[path[0]];
					for (var index = 1; index < path.Length; index++)
					{
						node = node[path[index]];
					}
 
					return node.ToString();
				}
				catch (Exception)
				{
					Debug.WriteLine($"Unable to retrieve secret '{name}'");
					return string.Empty;
				}
			}
		}
	}
}