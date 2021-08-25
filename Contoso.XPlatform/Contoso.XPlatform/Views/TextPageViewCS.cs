using Contoso.Forms.Configuration.TextForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.TextPage;
using System;

using Xamarin.Forms;

namespace Contoso.XPlatform.Views
{
    public class TextPageViewCS : ContentPage
    {
        public TextPageViewCS(TextPageViewModel textPageViewModel)
        {
            this.textPageScreenViewModel = textPageViewModel.TextPageScreenViewModel;
            AddContent();
            BindingContext = this.textPageScreenViewModel;
        }

        public TextPageScreenViewModel textPageScreenViewModel { get; set; }

        private void AddContent()
        {
            Content = GetContent();
        }

        private StackLayout GetContent()
        {
            StackLayout stackLayout = new StackLayout
            {
                Padding = new Thickness(30),
                Children =
                {
                    new Label
                    {
                        Text = "Home",
                        Style = LayoutHelpers.GetStaticStyleResource("HeaderStyle")
                    }
                }
            };

            foreach (var group in textPageScreenViewModel.FormSettings.TextGroups)
            {
                stackLayout.Children.Add(GetGroupHeader(group));
                foreach (LabelItemDescriptorBase item in group.Labels)
                {
                    switch (item)
                    {
                        case LabelItemDescriptor labelItemDescriptor:
                            stackLayout.Children.Add(GetLabelItem(labelItemDescriptor));
                            break;
                        case HyperLinkLabelItemDescriptor hyperLinkLabelItemDescriptor:
                            stackLayout.Children.Add(GetHyperLinkLabelItem(hyperLinkLabelItemDescriptor));
                            break;
                        case FormattedLabelItemDescriptor formattedItemDescriptor:
                            stackLayout.Children.Add(GetFornattedLabelItem(formattedItemDescriptor));
                            break;
                        default:
                            throw new ArgumentException($"{nameof(item)}: 615C881A-3EA5-4681-AD72-482E055E728E");
                    }
                }
            }

            return stackLayout;

            Label GetFornattedLabelItem(FormattedLabelItemDescriptor formattedItemDescriptor)
            {
                Label formattedLabel = new Label
                {
                    FormattedText = new FormattedString
                    {
                        Spans = { }
                    }
                };

                foreach (SpanItemDescriptorBase item in formattedItemDescriptor.Items)
                {
                    switch (item)
                    {
                        case SpanItemDescriptor spanItemDescriptor:
                            formattedLabel.FormattedText.Spans.Add(GetSpanItem(spanItemDescriptor));
                            break;
                        case HyperLinkSpanItemDescriptor hyperLinkSpanItemDescriptor:
                            formattedLabel.FormattedText.Spans.Add(GetHyperLinkSpanItem(hyperLinkSpanItemDescriptor));
                            break;
                        default:
                            throw new ArgumentException($"{nameof(item)}: BD90BDA3-31E9-4FCC-999E-2486CB527E30");
                    }
                }

                return formattedLabel;
            }

            Span GetHyperLinkSpanItem(HyperLinkSpanItemDescriptor spanItemDescriptor)
                => new Span
                {
                    Text = spanItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormHyperLinkSpanStyle"),
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            CommandParameter = spanItemDescriptor.Url
                        }
                        .AddBinding(TapGestureRecognizer.CommandProperty, new Binding(path: "TapCommand"))
                    }
                };

            Span GetSpanItem(SpanItemDescriptor spanItemDescriptor)
                => new Span
                {
                    Text = spanItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormItemSpanStyle")
                };


            Label GetHyperLinkLabelItem(HyperLinkLabelItemDescriptor labelItemDescriptor)
                => new Label
                {
                    Text = labelItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormHyperLinkLabelStyle"),
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            CommandParameter = labelItemDescriptor.Url
                        }
                        .AddBinding(TapGestureRecognizer.CommandProperty, new Binding(path: "TapCommand"))
                    }
                };

            Label GetLabelItem(LabelItemDescriptor labelItemDescriptor)
                => new Label
                {
                    Text = labelItemDescriptor.Text,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormItemLabelStyle")
                };

            Label GetGroupHeader(TextGroupDescriptor textGroupDescriptor)
                => new Label
                {
                    Text = textGroupDescriptor.Title,
                    Style = LayoutHelpers.GetStaticStyleResource("TextFormGroupHeaderStyle")
                };
        }
    }
}