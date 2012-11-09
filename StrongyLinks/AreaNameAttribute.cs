using System;

namespace ChessOk.StrongyLinks
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class AreaNameAttribute : Attribute
    {
        public AreaNameAttribute(string areaName)
        {
            AreaName = areaName;
        }

        public string AreaName { get; private set; }
    }
}
