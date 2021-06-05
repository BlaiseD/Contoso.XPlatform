using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Navigation;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings;
using Contoso.XPlatform.Flow.Settings.Screen;
using System;
using System.Collections.Generic;
using System.Linq;

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
                GetUrl = "/Instructor/GetSingle"
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
                        PlaceHolderText = "Select One ...",
                        TextAndValueObjectType = "System.Object",
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
                            SourceElementType = typeof(IQueryable<CourseModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(CourseModel).AssemblyQualifiedName,
                            DataType = typeof(Course).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<CourseAssignmentModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<CourseAssignment>).AssemblyQualifiedName
                        }
                    }
                }
            },
            ModelType = "Contoso.Domain.Entities.InstructorModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static EditFormSettingsDescriptor DepartmentForm = new EditFormSettingsDescriptor
        {
            Title = "Department",
            DisplayField = "Name",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle"
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
                        TextAndValueObjectType = "System.Object",
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
                            SourceElementType = typeof(IQueryable<InstructorModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(InstructorModel).AssemblyQualifiedName,
                            DataType = typeof(Instructor).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<InstructorModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<Instructor>).AssemblyQualifiedName
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
            ModelType = "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static EditFormSettingsDescriptor CourseForm = new EditFormSettingsDescriptor
        {
            Title = "Course",
            DisplayField = "FullName",
            RequestDetails = new EditFormRequestDetailsDescriptor
            {
                GetUrl = "/Course/GetSingle"
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
                        TextAndValueObjectType = "System.Object",
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
                            SourceElementType = typeof(IQueryable<LookUpsModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                            DataType = typeof(LookUps).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
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
                        TextAndValueObjectType = "System.Object",
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
                            SourceElementType = typeof(IQueryable<DepartmentModel>).AssemblyQualifiedName,
                            ParameterName = "$it",
                            BodyType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName
                        },
                        RequestDetails = new RequestDetailsDescriptor
                        {
                            DataSourceUrl = "api/Dropdown/GetObjectDropdown",
                            ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                            DataType = typeof(Department).AssemblyQualifiedName,
                            ModelReturnType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName,
                            DataReturnType = typeof(IEnumerable<Department>).AssemblyQualifiedName
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
            ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

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
            ModelType = "Contoso.Domain.Entities.StudentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };

        internal static IList<CommandButtonDescriptor> ButtonDescriptors = new List<CommandButtonDescriptor>
        {
            new CommandButtonDescriptor { Id = 1, LongString = "Save", ShortString = "S", Command = "SubmitCommand", ButtonIcon = "Save" },
            new CommandButtonDescriptor { Id = 2, LongString = "Home", ShortString = "H", Command = "NavigateCommand", ButtonIcon = "Home" }
        };

        internal static ScreenSettings<EditFormSettingsDescriptor> GetScreenSettings(string moduleName)
        {
            if (moduleName == "students")
                return new ScreenSettings<EditFormSettingsDescriptor>(StudentForm, ButtonDescriptors, ViewType.EditForm);
            else if (moduleName == "courses")
                return new ScreenSettings<EditFormSettingsDescriptor>(CourseForm, ButtonDescriptors, ViewType.EditForm);
            else if (moduleName == "departments")
                return new ScreenSettings<EditFormSettingsDescriptor>(DepartmentForm, ButtonDescriptors, ViewType.EditForm);
            else if (moduleName == "instructors")
                return new ScreenSettings<EditFormSettingsDescriptor>(InstructorForm, ButtonDescriptors, ViewType.EditForm);
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
                }
            }
        };

        internal static FlowSettings GetFlowSettings<T>(string currentModule)
        {
            return new FlowSettings(new FlowDataCache(), GetNavigationBar(currentModule), GetScreenSettings(currentModule));
        }
    }

    
}
