using System;

namespace NptExplorer.Mobile.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExcludeFromNavigationStack : Attribute
    {
    }
}