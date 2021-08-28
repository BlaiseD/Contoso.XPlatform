﻿using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Forms.Configuration.TextForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.Forms.Parameters;
using Contoso.Forms.Parameters.Bindings;
using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.EditForm;
using Contoso.Forms.Parameters.ListForm;
using Contoso.Forms.Parameters.Navigation;
using Contoso.Forms.Parameters.SearchForm;
using Contoso.Forms.Parameters.TextForm;
using Contoso.Forms.Parameters.Validation;

namespace Contoso.AutoMapperProfiles
{
    public class FormsParameterToFormsDescriptorMappingProfile : Profile
    {
        public FormsParameterToFormsDescriptorMappingProfile()
        {
			CreateMap<CollectionViewItemBindingParameters, CollectionViewItemBindingDescriptor>();
			CreateMap<CollectionViewItemBindingsDictionaryParameters, CollectionViewItemBindingsDictionaryDescriptor>();
			CreateMap<CommandButtonParameters, CommandButtonDescriptor>();
			CreateMap<DirectiveArgumentDictionaryParameters, DirectiveArgumentDictionaryDescriptor>();
			CreateMap<DirectiveArgumentParameters, DirectiveArgumentDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<DirectiveDefinitionParameters, DirectiveDefinitionDescriptor>();
			CreateMap<DirectiveParameters, DirectiveDescriptor>();
			CreateMap<DropDownTemplateParameters, DropDownTemplateDescriptor>();
			CreateMap<EditFormRequestDetailsParameters, EditFormRequestDetailsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName));
			CreateMap<EditFormSettingsParameters, EditFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<FieldValidationSettingsParameters, FieldValidationSettingsDescriptor>();
			CreateMap<FormattedLabelItemParameters, FormattedLabelItemDescriptor>();
			CreateMap<FormControlSettingsParameters, FormControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<FormGroupTemplateParameters, FormGroupTemplateDescriptor>();
			CreateMap<FormItemSettingsParameters, FormItemSettingsDescriptor>();
			CreateMap<FormsCollectionDisplayTemplateParameters, FormsCollectionDisplayTemplateDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>();
			CreateMap<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>();
			CreateMap<LabelItemParameters, LabelItemDescriptor>();
			CreateMap<ListFormSettingsParameters, ListFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<MultiSelectTemplateParameters, MultiSelectTemplateDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<NavigationBarParameters, NavigationBarDescriptor>();
			CreateMap<NavigationMenuItemParameters, NavigationMenuItemDescriptor>();
			CreateMap<RequestDetailsParameters, RequestDetailsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName))
				.ForMember(dest => dest.ModelReturnType, opts => opts.MapFrom(x => x.ModelReturnType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataReturnType, opts => opts.MapFrom(x => x.DataReturnType.AssemblyQualifiedName));
			CreateMap<SearchFilterGroupParameters, SearchFilterGroupDescriptor>();
			CreateMap<SearchFilterParameters, SearchFilterDescriptor>();
			CreateMap<SearchFormSettingsParameters, SearchFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<SpanItemParameters, SpanItemDescriptor>();
			CreateMap<TextFieldTemplateParameters, TextFieldTemplateDescriptor>();
			CreateMap<TextFormSettingsParameters, TextFormSettingsDescriptor>();
			CreateMap<TextGroupParameters, TextGroupDescriptor>();
			CreateMap<ValidationMessageDictionaryParameters, ValidationMessageDictionaryDescriptor>();
			CreateMap<ValidationMessageParameters, ValidationMessageDescriptor>();
			CreateMap<ValidationMethodDictionaryParameters, ValidationMethodDictionaryDescriptor>();
			CreateMap<ValidationMethodParameters, ValidationMethodDescriptor>();
			CreateMap<ValidatorArgumentDictionaryParameters, ValidatorArgumentDictionaryDescriptor>();
			CreateMap<ValidatorArgumentParameters, ValidatorArgumentDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ValidatorDefinitionParameters, ValidatorDefinitionDescriptor>();
			CreateMap<VariableDirectivesDictionaryParameters, VariableDirectivesDictionaryDescriptor>();
			CreateMap<VariableDirectivesParameters, VariableDirectivesDescriptor>();

            CreateMap<FormItemSettingsParameters, FormItemSettingsDescriptor>()
				.Include<FormControlSettingsParameters, FormControlSettingsDescriptor>()
				.Include<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
				.Include<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
				.Include<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>();

            CreateMap<SearchFilterParametersBase, SearchFilterDescriptorBase>()
				.Include<SearchFilterGroupParameters, SearchFilterGroupDescriptor>()
				.Include<SearchFilterParameters, SearchFilterDescriptor>();

            CreateMap<LabelItemParametersBase, LabelItemDescriptorBase>()
				.Include<FormattedLabelItemParameters, FormattedLabelItemDescriptor>()
				.Include<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>()
				.Include<LabelItemParameters, LabelItemDescriptor>();

            CreateMap<SpanItemParametersBase, SpanItemDescriptorBase>()
				.Include<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>()
				.Include<SpanItemParameters, SpanItemDescriptor>();
        }
    }
}