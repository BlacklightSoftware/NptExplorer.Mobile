using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NptExplorer.Core.Attributes;
using NptExplorer.Core.Models;

namespace NptExplorer.Core.Extensions
{
	public static class EnumExtension
	{
		public static string GetDescription<T>(string culture, System.Enum enumValue)
		{
			var type = enumValue.GetType();
			if (!type.IsEnum)
			{
				throw new ArgumentException($"{nameof(enumValue)} must be of Enum type", nameof(enumValue));
			}
			var memberInfo = type.GetMember(enumValue.ToString());
			if (memberInfo.Length <= 0)
			{
				return enumValue.ToString();
			}

			var attr = memberInfo[0].GetCustomAttributes(false).OfType<LanguageAttribute>().SingleOrDefault();
			if (attr != null)
			{
				return culture == "cy" ? attr.WelshText : attr.EnglishText;
			}
			else
			{
				var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs.Length > 0)
				{
					return ((DescriptionAttribute)attrs[0]).Description;
				}
			}
			return enumValue.ToString();
		}

		public static List<Chip> EnumToChips<T>(string culture, List<Enum>? ignore = null)
		{
			var chips = new List<Chip>();

			foreach (var e in Enum.GetValues(typeof(T)))
			{
				if (ignore != null && ignore.Contains((Enum)e))
				{
					continue;
				}

				var id = (int)e;
				var enumType = e.GetType();
				chips.Add(new Chip(id, GetDescription<T>(culture, (Enum)e)));
			}

			return chips;
		}

		public static int? GetEnumByDescription<T>(string culture, string selectedValue)
		{
			foreach (var e in Enum.GetValues(typeof(T)))
			{
				var desc = GetDescription<T>(culture, (Enum)e);
				if (selectedValue == desc)
				{
					return (int)e;
				}
			}

			return null;
		}
	}
}