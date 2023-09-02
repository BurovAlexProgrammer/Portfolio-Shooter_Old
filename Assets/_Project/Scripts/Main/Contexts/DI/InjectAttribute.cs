using System;
using JetBrains.Annotations;

namespace Main.Contexts.DI
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        
    }
}