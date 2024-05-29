using AutoMapper;

public class NullValueIgnoringConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>
    where TDestination : class, new()
{
    public TDestination Convert(TSource source, TDestination destination, ResolutionContext context)
    {
        if (source == null)
            return null;

        if (destination == null)
            destination = new TDestination();

        var sourceType = typeof(TSource);
        var destinationType = typeof(TDestination);

        foreach (var sourceProperty in sourceType.GetProperties())
        {
            var value = sourceProperty.GetValue(source, null);
            if (value != null)
            {
                var destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.CanWrite)
                {
                    destinationProperty.SetValue(destination, value, null);
                }
            }
        }

        return destination;
    }
}
