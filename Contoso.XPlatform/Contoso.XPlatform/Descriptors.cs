﻿using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Utils;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform
{
    internal static class Descriptors
    {
        internal static EditFormSettingsDescriptor InstructorForm = new EditFormSettingsDescriptor
        {
            Title = "Instructor",
            DisplayField = "FullName",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle",
                ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                DataType = typeof(Instructor).AssemblyQualifiedName,
                EditType = EditType.Update,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "ID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 3 }
                    },
                    SourceElementType = typeof(InstructorModel).AssemblyQualifiedName,
                    ParameterName = "f"
                },
                SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                    {
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "Courses"
                        },
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "OfficeAssignment"
                        }
                    }
                }
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
                        Field = "HireDate",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Hire Date is required." }
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
                        }
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
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "HireDate",
                    Type = "System.DateTime",
                    DomElementId = "hireDateId",
                    Title = "Hire Date",
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
                        }
                    }
                },
                new FormGroupSettingsDescriptor
                {
                    Field = "OfficeAssignment",
                    ModelType = typeof(OfficeAssignmentModel).AssemblyQualifiedName,
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "Location",
                            Type = "System.String",
                            DomElementId = "locationId",
                            Title = "Location",
                            Placeholder = "Location",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        }
                    },
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "InlineFormGroupTemplate"
                    },
                    Title = "",
                    ShowTitle = false
                },
                new MultiSelectFormControlSettingsDescriptor
                {
                    KeyFields = new List<string> { "CourseID" },
                    Field = "Courses",
                    DomElementId = "coursesId",
                    Title ="Courses",
                    Placeholder = "Select Courses ...",
                    Type = typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName,
                    MultiSelectTemplate =  new MultiSelectTemplateDescriptor
                    {
                        TemplateName = "MultiSelectTemplate",
                        PlaceHolderText = "(Courses)",
                        LoadingIndicatorText = "Loading ...",
                        ModelType = typeof(CourseAssignmentModel).AssemblyQualifiedName,
                        TextField = "CourseTitle",
                        ValueField = "CourseID",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                        MemberFullName = "Title"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["CourseID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "CourseID"
                                        },
                                        ["CourseTitle"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Title"
                                        }
                                    },
                                    NewType = typeof(CourseAssignmentModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(CourseModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(CourseAssignmentModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(CourseAssignmentModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(CourseAssignment).GetIEnumerableTypeString()
                        }
                    }
                }
            },
            ModelType = "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
        };

        internal static EditFormSettingsDescriptor InstructorFormWithPopupOfficeAssignment = new EditFormSettingsDescriptor
        {
            Title = "Instructor",
            DisplayField = "FullName",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle",
                ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                DataType = typeof(Instructor).AssemblyQualifiedName,
                EditType = EditType.Update,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "ID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 3 }
                    },
                    SourceElementType = typeof(InstructorModel).AssemblyQualifiedName,
                    ParameterName = "f"
                },
                SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                    {
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "Courses"
                        },
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "OfficeAssignment"
                        }
                    }
                }
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
                        Field = "HireDate",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Hire Date is required." }
                        }
                    }
                }
            ),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "ID",
                    Type = "System.Int32",
                    DomElementId = "id",
                    Title = "ID",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "HiddenTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0
                    }
                },
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
                        }
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
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "HireDate",
                    Type = "System.DateTime",
                    DomElementId = "hireDateId",
                    Title = "Hire Date",
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
                        }
                    }
                },
                new FormGroupSettingsDescriptor
                {
                    Field = "OfficeAssignment",
                    ModelType = typeof(OfficeAssignmentModel).AssemblyQualifiedName,
                    InvalidFormControlText = "(Invalid Form)",
                    ValidFormControlText = "(Form)",
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "Location",
                            Type = "System.String",
                            DomElementId = "locationId",
                            Title = "Location",
                            Placeholder = "Location",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        }
                    },
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "PopupFormGroupTemplate"
                    },
                    Title = "Office Assignment",
                    ShowTitle = false
                },
                new MultiSelectFormControlSettingsDescriptor
                {
                    KeyFields = new List<string> { "CourseID" },
                    Field = "Courses",
                    DomElementId = "coursesId",
                    Title ="Courses",
                    Placeholder = "Select Courses ...",
                    Type = typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName,
                    MultiSelectTemplate =  new MultiSelectTemplateDescriptor
                    {
                        TemplateName = "MultiSelectTemplate",
                        PlaceHolderText = "(Courses)",
                        LoadingIndicatorText = "Loading ...",
                        ModelType = typeof(CourseAssignmentModel).AssemblyQualifiedName,
                        TextField = "CourseTitle",
                        ValueField = "CourseID",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                        MemberFullName = "Title"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["CourseID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "CourseID"
                                        },
                                        ["CourseTitle"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Title"
                                        }
                                    },
                                    NewType = typeof(CourseAssignmentModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(CourseModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(CourseAssignmentModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(CourseAssignmentModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(CourseAssignment).GetIEnumerableTypeString()
                        }
                    }
                }
            },
            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
        };

        internal static EditFormSettingsDescriptor DepartmentForm = new EditFormSettingsDescriptor
        {
            Title = "Department",
            DisplayField = "Name",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle",
                ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                DataType = typeof(Department).AssemblyQualifiedName,
                EditType = EditType.Update,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "DepartmentID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 1 }
                    },
                    SourceElementType = typeof(DepartmentModel).AssemblyQualifiedName,
                    ParameterName = "f"
                }
            },
            ValidationMessages = new ValidationMessageDictionary
            (
                new List<ValidationMessageDescriptor>
                {
                    new ValidationMessageDescriptor
                    {
                        Field = "Budget",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Budget is required." },
                            new ValidationMethodDescriptor { ClassName = "MustBePositiveNumberRule", Message = "Budget must be a positive number." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "InstructorID",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Administrator is required." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "Name",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Name is required." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "StartDate",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Start Date is required." }
                        }
                    }
                }
            ),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "Budget",
                    Type = "System.Decimal",
                    DomElementId = "mudgetId",
                    Title = "Budget",
                    Placeholder = "Budget (required)",
                    StringFormat = "{0:F2}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0m,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            },
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "MustBePositiveNumberRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "InstructorID",
                    Type = "System.Int32",
                    DomElementId = "instructorID",
                    Title = "Administrator",
                    Placeholder = "Select Administrator (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        PlaceHolderText = "Select Administrator:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "FullName",
                        ValueField = "ID",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                        MemberFullName = "FullName"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["ID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "ID"
                                        },
                                        ["FirstName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "FirstName"
                                        },
                                        ["LastName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "LastName"
                                        },
                                        ["FullName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "FullName"
                                        }
                                    },
                                    NewType = typeof(InstructorModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(InstructorModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(InstructorModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                            DataType = typeof(Instructor).AssemblyQualifiedName,
                            ModelReturnType = typeof(InstructorModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(Instructor).GetIEnumerableTypeString()
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "Name",
                    Type = "System.String",
                    DomElementId = "nameId",
                    Title = "Name",
                    Placeholder = "Name (required)",
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
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "StartDate",
                    Type = "System.DateTime",
                    DomElementId = "startDateId",
                    Title = "Start Date",
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
                        }
                    }
                }
            },
            ModelType = typeof(DepartmentModel).AssemblyQualifiedName
        };

        internal static EditFormSettingsDescriptor DepartmentFormWithCourses = new EditFormSettingsDescriptor
        {
            Title = "Department",
            DisplayField = "Name",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle",
                ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                DataType = typeof(Department).AssemblyQualifiedName,
                EditType = EditType.Update,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "DepartmentID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 2 }
                    },
                    SourceElementType = typeof(DepartmentModel).AssemblyQualifiedName,
                    ParameterName = "f"
                },
                SelectExpandDefinition = new Common.Configuration.ExpansionDescriptors.SelectExpandDefinitionDescriptor
                {
                    ExpandedItems = new List<Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor>
                    {
                        new Common.Configuration.ExpansionDescriptors.SelectExpandItemDescriptor
                        {
                            MemberName = "Courses"
                        }
                    }
                }
            },
            ValidationMessages = new ValidationMessageDictionary
            (
                new List<ValidationMessageDescriptor>
                {
                    new ValidationMessageDescriptor
                    {
                        Field = "Budget",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Budget is required." },
                            new ValidationMethodDescriptor { ClassName = "MustBePositiveNumberRule", Message = "Budget must be a positive number." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "InstructorID",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Administrator is required." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "Name",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Name is required." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "StartDate",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Start Date is required." }
                        }
                    }
                }
            ),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "Budget",
                    Type = "System.Decimal",
                    DomElementId = "mudgetId",
                    Title = "Budget",
                    Placeholder = "Budget (required)",
                    StringFormat = "{0:F2}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0m,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            },
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "MustBePositiveNumberRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "InstructorID",
                    Type = "System.Int32",
                    DomElementId = "instructorID",
                    Title = "Administrator",
                    Placeholder = "Select Administrator (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        PlaceHolderText = "Select Administrator:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "FullName",
                        ValueField = "ID",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                        MemberFullName = "FullName"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["ID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "ID"
                                        },
                                        ["FirstName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "FirstName"
                                        },
                                        ["LastName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "LastName"
                                        },
                                        ["FullName"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "FullName"
                                        }
                                    },
                                    NewType = typeof(InstructorModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(InstructorModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(InstructorModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                            DataType = typeof(Instructor).AssemblyQualifiedName,
                            ModelReturnType = typeof(InstructorModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(Instructor).GetIEnumerableTypeString()
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "Name",
                    Type = "System.String",
                    DomElementId = "nameId",
                    Title = "Name",
                    Placeholder = "Name (required)",
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
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "StartDate",
                    Type = "System.DateTime",
                    DomElementId = "startDateId",
                    Title = "Start Date",
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
                        }
                    }
                },
                new FormGroupArraySettingsDescriptor
                {
                    Field = "Courses",
                    Title = "Courses",
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "FormGroupArrayTemplate"
                    },
                    ValidationMessages= new ValidationMessageDictionary
                    (
                        new List<ValidationMessageDescriptor>
                        {
                            new ValidationMessageDescriptor
                            {
                                Field = "CourseID",
                                Methods = new List<ValidationMethodDescriptor>
                                {
                                    new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "CourseID is required." },
                                    new ValidationMethodDescriptor { ClassName = "MustBeIntegerRule", Message = "CourseID must be an integer." }
                                }
                            },
                            new ValidationMessageDescriptor
                            {
                                Field = "Credits",
                                Methods = new List<ValidationMethodDescriptor>
                                {
                                    new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Credits is required." },
                                    new ValidationMethodDescriptor { ClassName = "RangeRule", Message = "Credits must be between 0 and 5 inclusive." }
                                }
                            },
                            new ValidationMessageDescriptor
                            {
                                Field = "DepartmentID",
                                Methods = new List<ValidationMethodDescriptor>
                                {
                                    new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Department is required.." }
                                }
                            },
                            new ValidationMessageDescriptor
                            {
                                Field = "Title",
                                Methods = new List<ValidationMethodDescriptor>
                                {
                                    new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Title is required." }
                                }
                            }
                        }
                    ),
                    FormsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateDescriptor
                    {
                        TemplateName = "TextDetailTemplate",
                        PlaceHolderText = "(Courses)",
                        LoadingIndicatorText = "Loading ...",
                        ModelType = typeof(CourseModel).AssemblyQualifiedName,
                        Bindings = new CollectionViewItemBindingsDictionary
                        (
                            new List<CollectionViewItemBindingDescriptor>
                            {
                                new CollectionViewItemBindingDescriptor
                                {
                                    Name = "Header",
                                    Property = "CourseID",
                                    StringFormat = "ID {0}"
                                },
                                new CollectionViewItemBindingDescriptor
                                {
                                    Name = "Text",
                                    Property = "Title",
                                    StringFormat = "{0}"
                                },
                                new CollectionViewItemBindingDescriptor
                                {
                                    Name = "Detail",
                                    Property = "Credits",
                                    StringFormat = "Credits: {0}"
                                }
                            }
                        ),
                        CollectionSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new OrderByOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                SelectorBody = new MemberSelectorOperatorDescriptor
                                {
                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                    MemberFullName = "Title"
                                },
                                SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                SelectorParameterName = "d"
                            },
                            SourceElementType = typeof(CourseModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(CourseModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/List/GetList",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(CourseModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(Course).GetIEnumerableTypeString()
                        }
                    },
                    FieldSettings = new List<FormItemSettingsDescriptor>
                    {
                        new FormControlSettingsDescriptor
                        {
                            Field = "CourseID",
                            Type = "System.Int32",
                            DomElementId = "courseIDId",
                            Title = "Course",
                            Placeholder = "Course ID (required)",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "MustBeIntegerRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "Credits",
                            Type = "System.Int32",
                            DomElementId = "creditsId",
                            Title = "Credits",
                            Placeholder = "Credits (required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                PlaceHolderText = "Select credits:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Text",
                                ValueField = "NumericValue",
                                TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                {
                                    Selector = new SelectOperatorDescriptor
                                    {
                                        SourceOperand = new OrderByOperatorDescriptor
                                        {
                                            SourceOperand = new WhereOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                                FilterBody = new EqualsBinaryOperatorDescriptor
                                                {
                                                    Left = new MemberSelectorOperatorDescriptor
                                                    {
                                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                        MemberFullName = "ListName"
                                                    },
                                                    Right = new ConstantOperatorDescriptor
                                                    {
                                                        ConstantValue = "Credits",
                                                        Type = typeof(string).AssemblyQualifiedName
                                                    }
                                                },
                                                FilterParameterName = "l"
                                            },
                                            SelectorBody = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "NumericValue"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                            SelectorParameterName = "l"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["NumericValue"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "NumericValue"
                                                },
                                                ["Text"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                                    MemberFullName = "Text"
                                                }
                                            },
                                            NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "s"
                                    },
                                    SourceElementType = typeof(LookUpsModel).GetIQueryableTypeString(),
                                    ParameterName = "$it",
                                    BodyType = typeof(LookUpsModel).GetIEnumerableTypeString()
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                                    DataType = typeof(LookUps).AssemblyQualifiedName,
                                    ModelReturnType = typeof(LookUpsModel).GetIEnumerableTypeString(),
                                    DataReturnType = typeof(LookUps).GetIEnumerableTypeString()
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    },
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RangeRule",
                                        FunctionName = "Check",
                                        Arguments = new ValidatorArgumentDictionary
                                        (
                                            new List<ValidatorArgumentDescriptor>
                                            {
                                                new ValidatorArgumentDescriptor { Name = "min", Value = 0, Type = "System.Int32" },
                                                new ValidatorArgumentDescriptor { Name = "max", Value = 5, Type = "System.Int32" }
                                            }
                                        )
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "DepartmentID",
                            Type = "System.Int32",
                            DomElementId = "departmentID",
                            Title = "Department",
                            Placeholder = "Department(required)",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                PlaceHolderText = "Select Department:",
                                LoadingIndicatorText = "Loading ...",
                                TextField = "Name",
                                ValueField = "DepartmentID",
                                TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                                {
                                    Selector = new SelectOperatorDescriptor
                                    {
                                        SourceOperand = new OrderByOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                            SelectorBody = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                MemberFullName = "Name"
                                            },
                                            SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                            SelectorParameterName = "d"
                                        },
                                        SelectorBody = new MemberInitOperatorDescriptor
                                        {
                                            MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                            {
                                                ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                    MemberFullName = "DepartmentID"
                                                },
                                                ["Name"] = new MemberSelectorOperatorDescriptor
                                                {
                                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                                    MemberFullName = "Name"
                                                }
                                            },
                                            NewType = typeof(DepartmentModel).AssemblyQualifiedName
                                        },
                                        SelectorParameterName = "d"
                                    },
                                    SourceElementType = typeof(DepartmentModel).GetIQueryableTypeString(),
                                    ParameterName = "$it",
                                    BodyType = typeof(DepartmentModel).GetIEnumerableTypeString()
                                },
                                RequestDetails = new RequestDetailsDescriptor
                                {
                                    DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                                    ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                                    DataType = typeof(Department).AssemblyQualifiedName,
                                    ModelReturnType = typeof(DepartmentModel).GetIEnumerableTypeString(),
                                    DataReturnType = typeof(Department).GetIEnumerableTypeString()
                                }
                            },
                            ValidationSetting = new FieldValidationSettingsDescriptor
                            {
                                DefaultValue = 0,
                                Validators = new List<ValidatorDefinitionDescriptor>
                                {
                                    new ValidatorDefinitionDescriptor
                                    {
                                        ClassName = "RequiredRule",
                                        FunctionName = "Check"
                                    }
                                }
                            }
                        },
                        new FormControlSettingsDescriptor
                        {
                            Field = "Title",
                            Type = "System.String",
                            DomElementId = "titleId",
                            Title = "Title",
                            Placeholder = "Title (required)",
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
                                }
                            }
                        }
                    },
                    ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    KeyFields = new List<string> { "CourseID" }
                }
            },
            ModelType = typeof(DepartmentModel).AssemblyQualifiedName
        };

        internal static EditFormSettingsDescriptor CourseForm = new EditFormSettingsDescriptor
        {
            Title = "Course",
            DisplayField = "FullName",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Course/GetSingle",
                ModelType = typeof(CourseModel).AssemblyQualifiedName,
                DataType = typeof(Course).AssemblyQualifiedName,
                EditType = EditType.Update,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "CourseID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 1050 }
                    },
                    SourceElementType = typeof(CourseModel).AssemblyQualifiedName,
                    ParameterName = "f"
                }
            },
            ValidationMessages = new ValidationMessageDictionary
            (
                new List<ValidationMessageDescriptor>
                {
                    new ValidationMessageDescriptor
                    {
                        Field = "CourseID",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "CourseID is required." },
                            new ValidationMethodDescriptor { ClassName = "MustBeIntegerRule", Message = "CourseID must be an integer." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "Credits",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Credits is required." },
                            new ValidationMethodDescriptor { ClassName = "RangeRule", Message = "Credits must be between 0 and 5 inclusive." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "DepartmentID",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Department is required.." }
                        }
                    },
                    new ValidationMessageDescriptor
                    {
                        Field = "Title",
                        Methods = new List<ValidationMethodDescriptor>
                        {
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Title is required." }
                        }
                    }
                }
            ),
            FieldSettings = new List<FormItemSettingsDescriptor>
            {
                new FormControlSettingsDescriptor
                {
                    Field = "CourseID",
                    Type = "System.Int32",
                    DomElementId = "courseIDId",
                    Title = "Course",
                    Placeholder = "Course ID (required)",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            },
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "MustBeIntegerRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "Credits",
                    Type = "System.Int32",
                    DomElementId = "creditsId",
                    Title = "Credits",
                    Placeholder = "Credits (required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        PlaceHolderText = "Select credits:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Text",
                        ValueField = "NumericValue",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new WhereOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                        FilterBody = new EqualsBinaryOperatorDescriptor
                                        {
                                            Left = new MemberSelectorOperatorDescriptor
                                            {
                                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                                MemberFullName = "ListName"
                                            },
                                            Right = new ConstantOperatorDescriptor
                                            {
                                                ConstantValue = "Credits",
                                                Type = typeof(string).AssemblyQualifiedName
                                            }
                                        },
                                        FilterParameterName = "l"
                                    },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                        MemberFullName = "NumericValue"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                                    SelectorParameterName = "l"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["NumericValue"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "NumericValue"
                                        },
                                        ["Text"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "s" },
                                            MemberFullName = "Text"
                                        }
                                    },
                                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "s"
                            },
                            SourceElementType = typeof(LookUpsModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(LookUpsModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(LookUpsModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(LookUps).GetIEnumerableTypeString()
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            },
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RangeRule",
                                FunctionName = "Check",
                                Arguments = new ValidatorArgumentDictionary
                                (
                                    new List<ValidatorArgumentDescriptor>
                                    {
                                        new ValidatorArgumentDescriptor { Name = "min", Value = 0, Type = "System.Int32" },
                                        new ValidatorArgumentDescriptor { Name = "max", Value = 5, Type = "System.Int32" }
                                    }
                                )
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "DepartmentID",
                    Type = "System.Int32",
                    DomElementId = "departmentID",
                    Title = "Department",
                    Placeholder = "Department(required)",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        PlaceHolderText = "Select Department:",
                        LoadingIndicatorText = "Loading ...",
                        TextField = "Name",
                        ValueField = "DepartmentID",
                        TextAndValueSelector = new SelectorLambdaOperatorDescriptor
                        {
                            Selector = new SelectOperatorDescriptor
                            {
                                SourceOperand = new OrderByOperatorDescriptor
                                {
                                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "$it" },
                                    SelectorBody = new MemberSelectorOperatorDescriptor
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                        MemberFullName = "Name"
                                    },
                                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                                    SelectorParameterName = "d"
                                },
                                SelectorBody = new MemberInitOperatorDescriptor
                                {
                                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                                    {
                                        ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                            MemberFullName = "DepartmentID"
                                        },
                                        ["Name"] = new MemberSelectorOperatorDescriptor
                                        {
                                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                            MemberFullName = "Name"
                                        }
                                    },
                                    NewType = typeof(DepartmentModel).AssemblyQualifiedName
                                },
                                SelectorParameterName = "d"
                            },
                            SourceElementType = typeof(DepartmentModel).GetIQueryableTypeString(),
                            ParameterName = "$it",
                            BodyType = typeof(DepartmentModel).GetIEnumerableTypeString()
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                            DataType = typeof(Department).AssemblyQualifiedName,
                            ModelReturnType = typeof(DepartmentModel).GetIEnumerableTypeString(),
                            DataReturnType = typeof(Department).GetIEnumerableTypeString()
                        }
                    },
                    ValidationSetting = new FieldValidationSettingsDescriptor
                    {
                        DefaultValue = 0,
                        Validators = new List<ValidatorDefinitionDescriptor>
                        {
                            new ValidatorDefinitionDescriptor
                            {
                                ClassName = "RequiredRule",
                                FunctionName = "Check"
                            }
                        }
                    }
                },
                new FormControlSettingsDescriptor
                {
                    Field = "Title",
                    Type = "System.String",
                    DomElementId = "titleId",
                    Title = "Title",
                    Placeholder = "Title (required)",
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
                        }
                    }
                }
            },
            ModelType = typeof(CourseModel).AssemblyQualifiedName
        };

        internal static EditFormSettingsDescriptor StudentForm = new EditFormSettingsDescriptor
        {
            Title = "Student",
            DisplayField = "FullName",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Student/GetSingle",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                EditType = EditType.Update,
                Filter = new FilterLambdaOperatorDescriptor
                {
                    FilterBody = new EqualsBinaryOperatorDescriptor
                    {
                        Left = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "f" },
                            MemberFullName = "ID"
                        },
                        Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 1}
                    },
                    SourceElementType = typeof(StudentModel).AssemblyQualifiedName,
                    ParameterName = "f"
                }
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
                            new ValidationMethodDescriptor { ClassName = "RequiredRule", Message = "Enrollment Date is required." }
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
                        }
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
                        }
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
                        }
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
            ModelType = typeof(StudentModel).AssemblyQualifiedName
        };

        internal static SearchFormSettingsDescriptor StudentSearchForm = new SearchFormSettingsDescriptor
        {
            Title = "Student",
            ModelType = typeof(StudentModel).AssemblyQualifiedName,
            LoadingIndicatorText = "Loading ...",
            FilterPlaceholder = "Filter",
            ItemTemplateName = "TextDetailTemplate",
            Bindings = new CollectionViewItemBindingsDictionary
            (
                new List<CollectionViewItemBindingDescriptor>
                {
                    new CollectionViewItemBindingDescriptor
                    {
                        Name = "Header",
                        Property = "ID",
                        StringFormat = "ID {0}"
                    },
                    new CollectionViewItemBindingDescriptor
                    {
                        Name = "Text",
                        Property = "FullName",
                        StringFormat = "{0}"
                    },
                    new CollectionViewItemBindingDescriptor
                    {
                        Name = "Detail",
                        Property = "EnrollmentDate",
                        StringFormat = "Enrollment Date: {0:MM/dd/yyyy}"
                    }
                }
            ),
            SortDescriptor = new SortCollectionDescriptor
            {
                SortDescriptions = new List<SortDescriptionDescriptor>
                {
                    new SortDescriptionDescriptor
                    {
                        PropertyName = "EnrollmentDate",
                        SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending
                    }
                },
                Take = 2
            },
            SearchDescriptor = new SearchFilterGroupDescriptor
            {
                Filters = new List<SearchFilterDescriptorBase>
                {
                    new SearchFilterDescriptor { Field = "EnrollmentDateString" },
                    new SearchFilterGroupDescriptor
                    {
                        Filters = new List<SearchFilterDescriptorBase>
                        {
                            new SearchFilterDescriptor { Field = "FirstName" },
                            new SearchFilterDescriptor { Field = "LastName" }
                        }
                    }
                }
            },
            RequestDetails = new RequestDetailsDescriptor
            {
                DataSourceUrl = "api/List/GetList",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                ModelReturnType = typeof(StudentModel).GetIQueryableTypeString(),
                DataReturnType = typeof(Student).GetIQueryableTypeString()
            }
        };

        internal static ListFormSettingsDescriptor AboutListForm = new ListFormSettingsDescriptor
        {
            Title = "About",
            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
            LoadingIndicatorText = "Loading ...",
            ItemTemplateName = "TextDetailTemplate",
            Bindings = new CollectionViewItemBindingsDictionary
            (
                new List<CollectionViewItemBindingDescriptor>
                {
                    new CollectionViewItemBindingDescriptor
                    {
                        Name = "Text",
                        Property = "DateTimeValue",
                        StringFormat = "Enrollment Date: {0:MM/dd/yyyy}"
                    },
                    new CollectionViewItemBindingDescriptor
                    {
                        Name = "Detail",
                        Property = "NumericValue",
                        StringFormat = "Count: {0:f0}"
                    }
                }
            ),
            FieldsSelector = new SelectorLambdaOperatorDescriptor
            {
                Selector = new SelectOperatorDescriptor
                {
                    SourceOperand = new OrderByOperatorDescriptor
                    {
                        SourceOperand = new GroupByOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor
                            {
                                ParameterName = "$it"
                            },
                            SelectorBody = new MemberSelectorOperatorDescriptor
                            {
                                MemberFullName = "EnrollmentDate",
                                SourceOperand = new ParameterOperatorDescriptor
                                {
                                    ParameterName = "item"
                                }
                            },
                            SelectorParameterName = "item"
                        },
                        SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                        SelectorBody = new MemberSelectorOperatorDescriptor
                        {
                            MemberFullName = "Key",
                            SourceOperand = new ParameterOperatorDescriptor
                            {
                                ParameterName = "group"
                            }
                        },
                        SelectorParameterName = "group"
                    },
                    SelectorBody = new MemberInitOperatorDescriptor
                    {
                        MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                        {
                            ["DateTimeValue"] = new MemberSelectorOperatorDescriptor
                            {
                                MemberFullName = "Key",
                                SourceOperand = new ParameterOperatorDescriptor
                                {
                                    ParameterName = "sel"
                                }
                            },
                            ["NumericValue"] = new ConvertOperatorDescriptor
                            {
                                SourceOperand = new CountOperatorDescriptor
                                {
                                    SourceOperand = new AsEnumerableOperatorDescriptor()
                                    {
                                        SourceOperand = new ParameterOperatorDescriptor
                                        {
                                            ParameterName = "sel"
                                        }
                                    }
                                },
                                Type = typeof(double?).FullName
                            }
                        },
                        NewType = typeof(LookUpsModel).AssemblyQualifiedName
                    },
                    SelectorParameterName = "sel"
                },
                SourceElementType = typeof(StudentModel).GetIQueryableTypeString(),
                ParameterName = "$it",
                BodyType = typeof(LookUpsModel).GetIQueryableTypeString()
            },
            RequestDetails = new RequestDetailsDescriptor
            {
                DataSourceUrl = "api/List/GetList",
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName,
                ModelReturnType = typeof(LookUpsModel).GetIQueryableTypeString(),
                DataReturnType = typeof(LookUps).GetIQueryableTypeString()
            }
        };

        internal static IList<CommandButtonDescriptor> ButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Save", ShortString = "S", Command = "SubmitCommand", ButtonIcon = "Save" },
            new CommandButtonDescriptor { Id = 2, LongString = "Home", ShortString = "H", Command = "NavigateCommand", ButtonIcon = "Home" }
        };

        internal static IList<CommandButtonDescriptor> ListFormButtonDescriptors = new List<CommandButtonDescriptor>
        {
        };

        internal static IList<CommandButtonDescriptor> SearchFormButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Add", ShortString = "A", Command = "AddCommand", ButtonIcon = "Plus" },
            new CommandButtonDescriptor { Id = 2, LongString = "Edit", ShortString = "E", Command = "EditCommand", ButtonIcon = "Edit" },
            new CommandButtonDescriptor { Id = 3, LongString = "Detail", ShortString = "D0", Command = "DetailCommand", ButtonIcon = "Info" },
            new CommandButtonDescriptor { Id = 4, LongString = "Delete", ShortString = "D1", Command = "DeleteCommand", ButtonIcon = "TrashAlt" }
        };

        internal static ScreenSettingsBase GetScreenSettings(string moduleName)
        {
            if (moduleName == "about")
                return new ScreenSettings<ListFormSettingsDescriptor>(AboutListForm, ListFormButtonDescriptors, ViewType.ListPage);
            if (moduleName == "students")
                // return new ScreenSettings<EditFormSettingsDescriptor>(StudentForm, ButtonDescriptors, ViewType.EditForm);
                return new ScreenSettings<SearchFormSettingsDescriptor>(StudentSearchForm, SearchFormButtonDescriptors, ViewType.SearchPage);
            else if (moduleName == "courses")
                return new ScreenSettings<EditFormSettingsDescriptor>(CourseForm, ButtonDescriptors, ViewType.EditForm);
            else if (moduleName == "departments")
                //return new ScreenSettings<EditFormSettingsDescriptor>(DepartmentForm, ButtonDescriptors, ViewType.EditForm);
                return new ScreenSettings<EditFormSettingsDescriptor>(DepartmentFormWithCourses, ButtonDescriptors, ViewType.EditForm);
            else if (moduleName == "instructors")
                //return new ScreenSettings<EditFormSettingsDescriptor>(InstructorForm, ButtonDescriptors, ViewType.EditForm);
                return new ScreenSettings<EditFormSettingsDescriptor>(InstructorFormWithPopupOfficeAssignment, ButtonDescriptors, ViewType.EditForm);
            //else if (moduleName == "studentslist")
                //return new ScreenSettings<SearchFormSettingsDescriptor>(StudentSearchForm, SearchFormButtonDescriptors, ViewType.SearchPage);
            else
            {
                DisplayInvalidPageMessage(moduleName);
                return null;
            }
        }

        async static void DisplayInvalidPageMessage(string page) =>
                await App.Current.MainPage.DisplayAlert("Nav Bar", $"No {page} page.", "Ok");


    internal static NavigationBarDescriptor GetNavigationBar(string currentModule) => new NavigationBarDescriptor
        {
            BrandText = "Contoso",
            CurrentModule = currentModule,
            MenuItems = new List<NavigationMenuItemDescriptor>
            {
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "home",
                    Icon = "Home",
                    Text = "Home"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "about",
                    Icon = "University",
                    Text = "About"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "students",
                    Icon = "Users",
                    Text = "Students"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "courses",
                    Icon = "BookOpen",
                    Text = "Courses"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "departments",
                    Icon = "Building",
                    Text = "Departments"
                },
                new NavigationMenuItemDescriptor
                {
                    InitialModule = "instructors",
                    Icon = "ChalkboardTeacher",
                    Text = "Instructors"
                }//,
                //new NavigationMenuItemDescriptor
                //{
                //    InitialModule = "studentslist",
                //    Icon = "Users",
                //    Text = "studentslist"
                //}
            }
        };

        internal static FlowSettings GetFlowSettings<T>(string currentModule)
        {
            return new FlowSettings(new FlowDataCache(), GetNavigationBar(currentModule), GetScreenSettings(currentModule));
        }
    }

    
}
