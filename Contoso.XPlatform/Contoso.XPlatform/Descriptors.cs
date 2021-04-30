using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Flow.Settings.Screen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform
{
    internal static class Descriptors
    {
        internal static EditFormSettingsDescriptor StudentForm = new EditFormSettingsDescriptor
        {
            Title = "Student",
            DisplayField = "FullName",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Student/GetSingle"
            },
            ValidationMessages = new ValidationMessageDictionary
            (
                new List<ValidationMessageDescriptor> 
                { 
                    new ValidationMessageDescriptor 
                    { 
                        Field = "FirstName",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "First Name is required." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "LastName",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Last Name is required." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "EnrollmentDate",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Enrollment Dateis required." }
                        }
                    }
                }
            ),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "FirstName",
                    Type = "System.String",
                    DomElementId = "firstNameId",
                    Title = "First Name",
                    Placeholder = "First Name (required)",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = "",
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            }
                        },
                        ValueType = "System.String"
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "LastName",
                    Type = "System.String",
                    DomElementId = "lastNameId",
                    Title = "Last Name",
                    Placeholder = "Last Name (required)",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = "",
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            }
                        },
                        ValueType = "System.String"
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "EnrollmentDate",
                    Type = "System.DateTime",
                    DomElementId = "enrollmentDateId",
                    Title = "Enrollment Date",
                    Placeholder = "",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "DateTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = new DateTime(1900, 1, 1),
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            }
                        },
                        ValueType = "System.DateTime"
                    }
                }
            },
            ConditionalDirectives = new Forms.Configuration.Directives.VariableDirectivesDictionary
            (
                new List<Forms.Configuration.Directives.VariableDirectivesDescriptor>
                {
                    new Forms.Configuration.Directives.VariableDirectivesDescriptor
                    {
                        Field = "EnrollmentDate",
                        ConditionalDirectives = new List<Forms.Configuration.Directives.DirectiveDescriptor>
                        {
                            new Forms.Configuration.Directives.DirectiveDescriptor
                            {
                                Definition = new Forms.Configuration.Directives.DirectiveDefinitionDescriptor
                                {
                                    ClassName = "ValidateIf",
                                    FunctionName = "Check"
                                },
                                Condition = new Common.Configuration.ExpressionDescriptors.FilterLambdaOperatorDescriptor
                                {
                                    SourceElementType = typeof(Domain.Entities.StudentModel).AssemblyQualifiedName,
                                    ParameterName = "f",
                                    FilterBody = new Common.Configuration.ExpressionDescriptors.EqualsBinaryOperatorDescriptor
                                    {
                                        Left = new Common.Configuration.ExpressionDescriptors.MemberSelectorOperatorDescriptor
                                        {
                                            MemberFullName = "FirstName",
                                            SourceOperand = new Common.Configuration.ExpressionDescriptors.ParameterOperatorDescriptor{ ParameterName = "f" }
                                        },
                                        Right = new Common.Configuration.ExpressionDescriptors.MemberSelectorOperatorDescriptor
                                        {
                                            MemberFullName = "LastName",
                                            SourceOperand = new Common.Configuration.ExpressionDescriptors.ParameterOperatorDescriptor{ ParameterName = "f" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            ),
            ModelType = "Contoso.Domain.Entities.StudentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static IEnumerable<CommandButtonDescriptor> ButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Save", ShortString = "S", Submit = true, ButtonIcon = "Save" },
            new CommandButtonDescriptor { Id = 2, LongString = "Home", ShortString = "H", Submit = false, ButtonIcon = "Home" }
        };

        internal static ScreenSettings<EditFormSettingsDescriptor> ScreenSettings = new ScreenSettings<EditFormSettingsDescriptor>(StudentForm, ButtonDescriptors, ViewType.EditForm);
    }

    
}
