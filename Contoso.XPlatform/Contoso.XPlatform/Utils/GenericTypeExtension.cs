using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contoso.XPlatform.Utils
{
    //public static readonly BindableProperty MyProperty = BindableProperty.Create("My", typeof(ComboBox), typeof(PickerItemDisplayPathConverter), null);
    ////public static readonly BindableProperty ParentObjectProperty = BindableProperty.Create("ParentObject", typeof(PickerValidatableObject<string>), typeof(PickerItemDisplayPathConverter), null);
    //public ComboBox My
    //{
    //    get { return (ComboBox)GetValue(MyProperty); }
    //    set { SetValue(MyProperty, value); }
    //}

	//[Xamarin.Forms.ContentProperty(nameof(TypeName))]
	public class GenericTypeExtension : IMarkupExtension<Type>
	{
		public string TypeName { get; set; }
		public string ElementTypeName { get; set; }

		public Type ProvideValue(IServiceProvider serviceProvider)
		{
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            if (!(serviceProvider.GetService(typeof(IXamlTypeResolver)) is IXamlTypeResolver typeResolver))
                throw new ArgumentException("No IXamlTypeResolver in IServiceProvider");
            if (string.IsNullOrEmpty(TypeName))
            {
                var li = (serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lip) ? lip.XmlLineInfo : new XmlLineInfo();
                throw new XamlParseException("TypeName isn't set.", li);
            }
            if (string.IsNullOrEmpty(ElementTypeName))
            {
                var li = (serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lip) ? lip.XmlLineInfo : new XmlLineInfo();
                throw new XamlParseException("ElementTypeName isn't set.", li);
            }


            Type elementType = typeResolver.Resolve(ElementTypeName, serviceProvider);
			Type genericType = typeResolver.Resolve(TypeName, serviceProvider);

			return genericType.MakeGenericType(elementType);
		}

		object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
		{
			return (this as IMarkupExtension<Type>).ProvideValue(serviceProvider);
		}
	}
}
