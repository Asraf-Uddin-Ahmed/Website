using System;
using Website.Foundation.Enums;
namespace Website.Foundation.Aggregates
{
    public interface ISettings : IEntity
    {
        string DisplayName { get; set; }
        string Name { get; set; }
        SettingsType Type { get; set; }
        string Value { get; set; }
    }
}
