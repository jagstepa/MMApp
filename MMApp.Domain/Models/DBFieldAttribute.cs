using System;

namespace MMApp.Domain.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DBFieldAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RelationshipAttribute : Attribute
    {
    }
}
