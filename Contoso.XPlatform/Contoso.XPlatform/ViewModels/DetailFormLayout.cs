using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.ViewModels
{
    public class DetailFormLayout
    {
        public DetailFormLayout()
        {
            ControlGroupBoxList = new List<ReadOnlyControlGroupBox>();
            Properties = new ObservableCollection<IReadOnly>();
        }

        public void Add(IReadOnly readOnly)
        {
            Properties.Add(readOnly);

            if (!ControlGroupBoxList.Any())
                throw new InvalidOperationException("{196C3BD2-23A7-4AB1-ACA0-62F627F904EB}");

            ControlGroupBoxList.Last().Add(readOnly);
        }

        public void AddControlGroupBox(IDetailGroupSettings formSettings)
        {
            ControlGroupBoxList.Add
            (
                new ReadOnlyControlGroupBox
                (
                    formSettings.Title,
                    formSettings.HeaderBindings,
                    new List<IReadOnly>()
                )
            );
        }

        public void AddControlGroupBox(DetailGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
        {
            ControlGroupBoxList.Add
            (
                new ReadOnlyControlGroupBox
                (
                    groupBoxSettingsDescriptor.GroupHeader,
                    groupBoxSettingsDescriptor.HeaderBindings,
                    new List<IReadOnly>()
                )
            );
        }

        public List<ReadOnlyControlGroupBox> ControlGroupBoxList { get; }

        public ObservableCollection<IReadOnly> Properties { get; }
    }
}
