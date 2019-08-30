using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace WebAPI.Areas.HelpPage.ModelDescriptions
{
    [ExcludeFromCodeCoverage]
    public class ParameterDescription
    {
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        public Collection<ParameterAnnotation> Annotations { get; private set; }

        public string Documentation { get; set; }

        public string Name { get; set; }

        public ModelDescription TypeDescription { get; set; }
    }
}