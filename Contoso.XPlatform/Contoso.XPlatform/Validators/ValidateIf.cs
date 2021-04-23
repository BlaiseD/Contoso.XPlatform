using System;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Validators
{
    //From DirectiveDescriptor where DirectiveDescriptor.DirectiveDefinitionDescriptor.Classname == ValidateIf
    //Evaluator is DirectiveDescriptor.FilterLambdaOperatorDescriptor
    //Field is the field to evaluate
    //ValidateIfManager listens for PropertyChangesd and calls ValidateIfManager.Check()
    internal class ValidateIf<T>
    {
        public Expression<Func<T, bool>> Evaluator { get; set; }
        public IValidationRule Validator { get; set; }
        public string Field { get; set; }
    }
}
