using System;
using System.Diagnostics.CodeAnalysis;

namespace WebAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class ModelDescription
    {
        public string Documentation { get; set; }

        public Type ModelType { get; set; }

        public string Name { get; set; }
    }
}