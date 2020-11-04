namespace Todos.Extensions
{
    public static class GenericExtensions
    {
        public static void Copy<TParent, TChild>(this TParent source, TChild destination)
        {
            var parentProperties = source.GetType().GetProperties();
            var childProperties = destination.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name &&
                        parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(destination, parentProperty.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}