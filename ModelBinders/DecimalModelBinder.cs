using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ObligatorioIntegrador2026.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            // Reemplazar coma por punto para normalizar al formato estándar (InvariantCulture)
            string normalizedValue = value.Replace(",", ".");

            if (bindingContext.ModelType == typeof(double) || bindingContext.ModelType == typeof(double?))
            {
                if (double.TryParse(normalizedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultDouble))
                {
                    bindingContext.Result = ModelBindingResult.Success(resultDouble);
                    return Task.CompletedTask;
                }
            }
            else if (bindingContext.ModelType == typeof(decimal) || bindingContext.ModelType == typeof(decimal?))
            {
                if (decimal.TryParse(normalizedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal resultDecimal))
                {
                    bindingContext.Result = ModelBindingResult.Success(resultDecimal);
                    return Task.CompletedTask;
                }
            }
            else if (bindingContext.ModelType == typeof(float) || bindingContext.ModelType == typeof(float?))
            {
                if (float.TryParse(normalizedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float resultFloat))
                {
                    bindingContext.Result = ModelBindingResult.Success(resultFloat);
                    return Task.CompletedTask;
                }
            }

            bindingContext.ModelState.TryAddModelError(
                bindingContext.ModelName,
                $"El valor '{value}' no es un número válido.");

            return Task.CompletedTask;
        }
    }

    public class InvariantDecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var type = context.Metadata.ModelType;
            if (type == typeof(double) || type == typeof(double?) ||
                type == typeof(decimal) || type == typeof(decimal?) ||
                type == typeof(float) || type == typeof(float?))
            {
                return new DecimalModelBinder();
            }

            return null;
        }
    }
}
