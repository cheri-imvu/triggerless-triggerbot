using System;
using System.Text.Json;

namespace Triggerless.TriggerBot.Models
{
    public static class JsonElementExtensions
    {
        public static string GetStringOrNull(this JsonElement element,
            string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out JsonElement value))
                return null;

            return value.ValueKind == JsonValueKind.Null
                ? null
                : value.GetString();
        }

        public static int? GetInt32OrNull(this JsonElement element,
            string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out JsonElement value))
                return null;

            if (value.ValueKind == JsonValueKind.Null)
                return null;

            return value.GetInt32();
        }

        public static bool? GetBooleanOrNull(this JsonElement element,
            string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out JsonElement value))
                return null;

            if (value.ValueKind == JsonValueKind.Null)
                return null;

            return value.GetBoolean();
        }

        public static JsonElement? GetElementOrNull(this JsonElement element,
            string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out JsonElement value))
                return null;

            return value;
        }

        public static string GetPathString(this JsonElement element,
            params string[] path)
        {
            foreach (string property in path)
            {
                if (!element.TryGetProperty(property, out element))
                    return null;
            }

            if (element.ValueKind == JsonValueKind.Null)
                return null;

            return element.GetString();
        }

        public static JsonElement? GetPathElement(this JsonElement element,
            params string[] path)
        {
            foreach (string property in path)
            {
                if (!element.TryGetProperty(property, out element))
                    return null;
            }

            return element;
        }
    }
}