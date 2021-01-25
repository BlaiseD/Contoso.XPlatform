using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Forms.Parameters;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Contoso.Bsl.Flow
{
    public class FlowActivity : IFlowActivity
    {
        private IExpressionDescriptor expressionDescriptor;
        public DirectorBase Director => throw new NotImplementedException();

        public void DisplayInputQuestions(InputFormParameters form, ICollection<ConnectorParameters> shortValues = null)
        {
            throw new NotImplementedException();
        }

        public void DisplayQuestions(QuestionFormParameters form, ICollection<ConnectorParameters> shortValues = null)
        {
            throw new NotImplementedException();
        }

        public void FlowComplete()
        {
            throw new NotImplementedException();
        }

        public string FormatString(string format, Collection<object> list) 
            => string.Format(CultureInfo.CurrentCulture, format, list.ToArray());

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public void Wait()
        {
            throw new NotImplementedException();
        }
    }
}
