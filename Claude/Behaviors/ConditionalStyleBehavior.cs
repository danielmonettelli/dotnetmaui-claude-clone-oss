namespace Claude.Behaviors;

public class ConditionalStyleBehavior : Behavior<VisualElement>
{
    public static readonly BindableProperty PropertyNameProperty =
        BindableProperty.Create(nameof(PropertyName), typeof(string), typeof(ConditionalStyleBehavior), null);

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(object), typeof(ConditionalStyleBehavior), null);

    public static readonly BindableProperty StyleProperty =
        BindableProperty.Create(nameof(Style), typeof(Style), typeof(ConditionalStyleBehavior), null);

    public string PropertyName
    {
        get => (string)GetValue(PropertyNameProperty);
        set => SetValue(PropertyNameProperty, value);
    }

    public object Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public Style Style
    {
        get => (Style)GetValue(StyleProperty);
        set => SetValue(StyleProperty, value);
    }

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);

        // Monitor property changes
        bindable.BindingContextChanged += OnBindingContextChanged;
        UpdateStyle(bindable);
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.BindingContextChanged -= OnBindingContextChanged;
    }

    private void OnBindingContextChanged(object sender, EventArgs e)
    {
        UpdateStyle((VisualElement)sender);
    }

    private void UpdateStyle(VisualElement element)
    {
        if (element.BindingContext == null || string.IsNullOrEmpty(PropertyName))
        {
            return;
        }

        // Get the property value using reflection
        System.Reflection.PropertyInfo? propertyInfo = element.BindingContext.GetType().GetProperty(PropertyName);
        if (propertyInfo != null)
        {
            object? propertyValue = propertyInfo.GetValue(element.BindingContext);

            // Apply style if values match
            if ((propertyValue != null && propertyValue.Equals(Value)) ||
                (propertyValue == null && Value == null))
            {
                if (element.Parent is VisualElement parent && Style != null)
                {
                    // Apply style to parent (NavigationPage)
                    foreach (Setter? setter in Style.Setters)
                    {
                        BindableProperty targetProperty = setter.Property;
                        parent.SetValue(targetProperty, setter.Value);
                    }
                }
            }
        }
    }
}