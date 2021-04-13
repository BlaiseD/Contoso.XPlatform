using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Validation;
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
            }
        };
    }
}
