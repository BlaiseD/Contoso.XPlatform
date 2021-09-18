﻿using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DetailForm;
using Contoso.Forms.Configuration.EditForm;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Tests
{
    public static class ReadOnlyDescriptors
    {
        internal static DetailFormSettingsDescriptor InstructorFormWithInlineOfficeAssignment = new DetailFormSettingsDescriptor
        {
            Title = "Instructor",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle"
            },
            FieldSettings = new List<DetailItemSettingsDescriptor>
            {
                new DetailControlSettingsDescriptor
                {
                    Field = "ID",
                    Type = "System.Int32",
                    Title = "ID",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "HiddenTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "FirstName",
                    Type = "System.String",
                    Title = "First Name",
                    StringFormat = "{0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "LastName",
                    Type = "System.String",
                    Title = "Last Name",
                    StringFormat = "{0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "HireDate",
                    Type = "System.DateTime",
                    Title = "Hire Date",
                    StringFormat = "{0:MM/dd/yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailGroupSettingsDescriptor
                {
                    Field = "OfficeAssignment",
                    ModelType = typeof(OfficeAssignmentModel).AssemblyQualifiedName,
                    FieldSettings = new List<DetailItemSettingsDescriptor>
                    {
                        new DetailControlSettingsDescriptor
                        {
                            Field = "Location",
                            Type = "System.String",
                            Title = "Location",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        }
                    },
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "InlineFormGroupTemplate"
                    },
                    Title = ""
                },
                new MultiSelectDetailControlSettingsDescriptor
                {
                    KeyFields = new List<string> { "CourseID" },
                    Field = "Courses",
                    Title ="Courses",
                    Type = typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName,
                    MultiSelectTemplate =  new MultiSelectTemplateDescriptor
                    {
                        TemplateName = "MultiSelectTemplate",
                        PlaceholderText = "(Courses)",
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

        internal static DetailFormSettingsDescriptor InstructorFormWithPopupOfficeAssignment = new DetailFormSettingsDescriptor
        {
            Title = "Instructor",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Instructor/GetSingle"
            },
            FieldSettings = new List<DetailItemSettingsDescriptor>
            {
                new DetailControlSettingsDescriptor
                {
                    Field = "ID",
                    Type = "System.Int32",
                    Title = "ID",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "HiddenTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "FirstName",
                    Type = "System.String",
                    Title = "First Name",
                    StringFormat = "{0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" },
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "LastName",
                    Type = "System.String",
                    Title = "Last Name",
                    StringFormat = "{0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "HireDate",
                    Type = "System.DateTime",
                    Title = "Hire Date",
                    StringFormat = "{0:MM/dd/yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailGroupSettingsDescriptor
                {
                    Field = "OfficeAssignment",
                    ModelType = typeof(OfficeAssignmentModel).AssemblyQualifiedName,
                    FieldSettings = new List<DetailItemSettingsDescriptor>
                    {
                        new DetailControlSettingsDescriptor
                        {
                            Field = "Location",
                            Type = "System.String",
                            Title = "Location",
                            StringFormat = "{0}",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        }
                    },
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "PopupFormGroupTemplate"
                    },
                    Title = ""
                },
                new MultiSelectDetailControlSettingsDescriptor
                {
                    KeyFields = new List<string> { "CourseID" },
                    Field = "Courses",
                    Title ="Courses",
                    Type = typeof(ICollection<CourseAssignmentModel>).AssemblyQualifiedName,
                    MultiSelectTemplate =  new MultiSelectTemplateDescriptor
                    {
                        TemplateName = "MultiSelectTemplate",
                        PlaceholderText = "(Courses)",
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

        internal static DetailFormSettingsDescriptor DepartmentForm = new DetailFormSettingsDescriptor
        {
            Title = "Department",
            RequestDetails = new FormRequestDetailsDescriptor
            {
                GetUrl = "/Department/GetSingle"
            },
            FieldSettings = new List<DetailItemSettingsDescriptor>
            {
                new DetailControlSettingsDescriptor
                {
                    Field = "DepartmentID",
                    Type = "System.Int32",
                    Title = "ID",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "HiddenTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "Budget",
                    Type = "System.Decimal",
                    Title = "Budget",
                    StringFormat = "{0:F2}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "InstructorID",
                    Type = "System.Int32",
                    Title = "Administrator",
                    StringFormat = "{0}",
                    DropDownTemplate = new DropDownTemplateDescriptor
                    {
                        TemplateName = "PickerTemplate",
                        PlaceholderText = "Select Administrator:",
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
                    }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "Name",
                    Type = "System.String",
                    Title = "Name",
                    StringFormat = "{0}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailControlSettingsDescriptor
                {
                    Field = "StartDate",
                    Type = "System.DateTime",
                    Title = "Start Date",
                    StringFormat = "{0:MM/dd/yyyy}",
                    TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                },
                new DetailGroupArraySettingsDescriptor
                {
                    Field = "Courses",
                    FormGroupTemplate = new FormGroupTemplateDescriptor
                    {
                        TemplateName = "FormGroupArrayTemplate"
                    },
                    FormsCollectionDisplayTemplate = new FormsCollectionDisplayTemplateDescriptor
                    {
                        TemplateName = "TextDetailTemplate",
                        Bindings = new List<ItemBindingDescriptor>
                        {
                            new ItemBindingDescriptor
                            {
                                Name = "Text",
                                Property = "DepartmentName",
                                StringFormat = "{0}"
                            },
                            new ItemBindingDescriptor
                            {
                                Name = "Detail",
                                Property = "StartDate",
                                StringFormat = "{0:MMMM dd, yyyy}"
                            }
                        }.ToDictionary(b => b.Name)
                    },
                    FieldSettings = new List<DetailItemSettingsDescriptor>
                    {
                        new DetailControlSettingsDescriptor
                        {
                            Field = "CourseID",
                            Type = "System.Int32",
                            Title = "Course",
                            StringFormat = "{0}",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        },
                        new DetailControlSettingsDescriptor
                        {
                            Field = "Credits",
                            Type = "System.Int32",
                            Title = "Credits",
                            StringFormat = "{0}",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                PlaceholderText = "Select credits:",
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
                        },
                        new DetailControlSettingsDescriptor
                        {
                            Field = "DepartmentID",
                            Type = "System.Int32",
                            Title = "Department",
                            StringFormat = "{0}",
                            DropDownTemplate = new DropDownTemplateDescriptor
                            {
                                TemplateName = "PickerTemplate",
                                PlaceholderText = "Select Department:",
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
                            }
                        },
                        new DetailControlSettingsDescriptor
                        {
                            Field = "Title",
                            Type = "System.String",
                            Title = "Title",
                            StringFormat = "{0}",
                            TextTemplate = new TextFieldTemplateDescriptor { TemplateName = "TextTemplate" }
                        }
                    },
                    ModelType = "Contoso.Domain.Entities.CourseModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                    KeyFields = new List<string> { "CourseID" }
                }
            },
            ModelType = "Contoso.Domain.Entities.DepartmentModel, Contoso.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        };
    }
}
