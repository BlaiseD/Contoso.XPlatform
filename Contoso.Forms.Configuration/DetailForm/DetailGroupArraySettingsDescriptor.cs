﻿using Contoso.Forms.Configuration.EditForm;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DetailForm
{
    public class DetailGroupArraySettingsDescriptor : DetailItemSettingsDescriptor, IDetailGroupSettings
    {
        public string Title { get; set; }
        public string ModelType { get; set; }//e.g. T
        public string Type { get; set; }//e.g. ICollection<T>
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<DetailItemSettingsDescriptor> FieldSettings { get; set; }
    }
}