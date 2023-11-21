using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesOfTheLand.Test
{
    class MediaValidationHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();

            // Check if the object is in the desired namespace
            if (model.GetType().Namespace == "StoriesOfTheLand.Models")
            {
                var vc = new ValidationContext(model, null, null);

                // Filter properties based on the desired namespace
                var propertiesToValidate = model.GetType().GetProperties()
                   .Where(prop => prop.DeclaringType.Namespace == "StoriesOfTheLand.Models")
                   .ToArray();

                // Validate the object itself
                Validator.TryValidateObject(model, vc, results, true);

                // Validate list properties manually
                foreach (var propertyInfo in propertiesToValidate)
                {
                    if (propertyInfo.PropertyType.IsGenericType &&
                        propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var list = (IEnumerable<object>?)propertyInfo.GetValue(model);

                        if (list != null)
                        {
                            var index = 0;
                            foreach (var item in list)
                            {
                                var itemValidationContext = new ValidationContext(item, vc, vc.Items)
                                {
                                    DisplayName = $"{propertyInfo.Name}[{index}]"
                                };

                                Validator.TryValidateObject(item, itemValidationContext, results, true);

                                index++;
                            }
                        }
                    }
                }

                if (model is IValidatableObject)
                {
                    (model as IValidatableObject).Validate(vc);
                }
            }

            return results;
        }
    }
}
