using AutoMapper;
using Contoso.Parameters.Expressions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Arithmetic;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Cacnonical;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Collection;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Conversions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.DateTimeOperators;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Logical;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Operand;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.StringOperators;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Contoso.AutoMapperProfiles
{
    public class ParameterToOperatorMappingProfile : Profile
    {
        const string PARAMETERS_KEY = "parameters";

        public ParameterToOperatorMappingProfile()
        {
			CreateMap<AddBinaryOperatorParameter, AddBinaryOperator>();
			CreateMap<AllOperatorParameter, AllOperator>()
				.ConstructUsing
				(
					(src, context) => new AllOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<AndBinaryOperatorParameter, AndBinaryOperator>();
			CreateMap<AnyOperatorParameter, AnyOperator>()
				.ConstructUsing
				(
					(src, context) => new AnyOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<AsQueryableOperatorParameter, AsQueryableOperator>();
			CreateMap<AverageOperatorParameter, AverageOperator>()
				.ConstructUsing
				(
					(src, context) => new AverageOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<BinaryOperatorParameter, BinaryOperator>();
			CreateMap<CastOperatorParameter, CastOperator>();
			CreateMap<CeilingOperatorParameter, CeilingOperator>();
			CreateMap<CollectionCastOperatorParameter, CollectionCastOperator>();
			CreateMap<CollectionConstantOperatorParameter, CollectionConstantOperator>();
			CreateMap<ConcatOperatorParameter, ConcatOperator>();
			CreateMap<ConstantOperatorParameter, ConstantOperator>();
			CreateMap<ContainsOperatorParameter, ContainsOperator>();
			CreateMap<ConvertCharArrayToStringOperatorParameter, ConvertCharArrayToStringOperator>();
			CreateMap<ConvertOperatorParameter, ConvertOperator>();
			CreateMap<ConvertToEnumOperatorParameter, ConvertToEnumOperator>();
			CreateMap<ConvertToNullableUnderlyingValueOperatorParameter, ConvertToNullableUnderlyingValueOperator>();
			CreateMap<ConvertToNumericDateOperatorParameter, ConvertToNumericDateOperator>();
			CreateMap<ConvertToNumericTimeOperatorParameter, ConvertToNumericTimeOperator>();
			CreateMap<ConvertToStringOperatorParameter, ConvertToStringOperator>();
			CreateMap<CountOperatorParameter, CountOperator>()
				.ConstructUsing
				(
					(src, context) => new CountOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<CustomMethodOperatorParameter, CustomMethodOperator>();
			CreateMap<DateOperatorParameter, DateOperator>();
			CreateMap<DayOperatorParameter, DayOperator>();
			CreateMap<DistinctOperatorParameter, DistinctOperator>();
			CreateMap<DivideBinaryOperatorParameter, DivideBinaryOperator>();
			CreateMap<EndsWithOperatorParameter, EndsWithOperator>();
			CreateMap<EqualsBinaryOperatorParameter, EqualsBinaryOperator>();
			CreateMap<FilterLambdaOperatorParameter, FilterLambdaOperator>()
				.ConstructUsing
				(
					(src, context) => new FilterLambdaOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.SourceElementType,
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<FirstOperatorParameter, FirstOperator>()
				.ConstructUsing
				(
					(src, context) => new FirstOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<FirstOrDefaultOperatorParameter, FirstOrDefaultOperator>()
				.ConstructUsing
				(
					(src, context) => new FirstOrDefaultOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<FloorOperatorParameter, FloorOperator>();
			CreateMap<FractionalSecondsOperatorParameter, FractionalSecondsOperator>();
			CreateMap<GreaterThanBinaryOperatorParameter, GreaterThanBinaryOperator>();
			CreateMap<GreaterThanOrEqualsBinaryOperatorParameter, GreaterThanOrEqualsBinaryOperator>();
			CreateMap<GroupByOperatorParameter, GroupByOperator>()
				.ConstructUsing
				(
					(src, context) => new GroupByOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<HasOperatorParameter, HasOperator>();
			CreateMap<HourOperatorParameter, HourOperator>();
			CreateMap<IEnumerableSelectorLambdaOperatorParameter, IEnumerableSelectorLambdaOperator>()
				.ConstructUsing
				(
					(src, context) => new IEnumerableSelectorLambdaOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.Selector),
						src.SourceElementType,
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<IndexOfOperatorParameter, IndexOfOperator>();
			CreateMap<InOperatorParameter, InOperator>();
			CreateMap<IsOfOperatorParameter, IsOfOperator>();
			CreateMap<LastOperatorParameter, LastOperator>()
				.ConstructUsing
				(
					(src, context) => new LastOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<LastOrDefaultOperatorParameter, LastOrDefaultOperator>()
				.ConstructUsing
				(
					(src, context) => new LastOrDefaultOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<LengthOperatorParameter, LengthOperator>();
			CreateMap<LessThanBinaryOperatorParameter, LessThanBinaryOperator>();
			CreateMap<LessThanOrEqualsBinaryOperatorParameter, LessThanOrEqualsBinaryOperator>();
			CreateMap<MaxDateTimeOperatorParameter, MaxDateTimeOperator>();
			CreateMap<MaxOperatorParameter, MaxOperator>()
				.ConstructUsing
				(
					(src, context) => new MaxOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<MemberInitOperatorParameter, MemberInitOperator>();
			CreateMap<MemberSelectorOperatorParameter, MemberSelectorOperator>();
			CreateMap<MinDateTimeOperatorParameter, MinDateTimeOperator>();
			CreateMap<MinOperatorParameter, MinOperator>()
				.ConstructUsing
				(
					(src, context) => new MinOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<MinuteOperatorParameter, MinuteOperator>();
			CreateMap<ModuloBinaryOperatorParameter, ModuloBinaryOperator>();
			CreateMap<MonthOperatorParameter, MonthOperator>();
			CreateMap<MultiplyBinaryOperatorParameter, MultiplyBinaryOperator>();
			CreateMap<NegateOperatorParameter, NegateOperator>();
			CreateMap<NotEqualsBinaryOperatorParameter, NotEqualsBinaryOperator>();
			CreateMap<NotOperatorParameter, NotOperator>();
			CreateMap<NowDateTimeOperatorParameter, NowDateTimeOperator>();
			CreateMap<OrBinaryOperatorParameter, OrBinaryOperator>();
			CreateMap<OrderByOperatorParameter, OrderByOperator>()
				.ConstructUsing
				(
					(src, context) => new OrderByOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SortDirection,
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<ParameterOperatorParameter, ParameterOperator>()
				.ConstructUsing
				(
					(src, context) => new ParameterOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<RoundOperatorParameter, RoundOperator>();
			CreateMap<SecondOperatorParameter, SecondOperator>();
			CreateMap<SelectManyOperatorParameter, SelectManyOperator>()
				.ConstructUsing
				(
					(src, context) => new SelectManyOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SelectOperatorParameter, SelectOperator>()
				.ConstructUsing
				(
					(src, context) => new SelectOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SelectorLambdaOperatorParameter, SelectorLambdaOperator>()
				.ConstructUsing
				(
					(src, context) => new SelectorLambdaOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.Selector),
						src.SourceElementType,
						src.BodyType,
						src.ParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SingleOperatorParameter, SingleOperator>()
				.ConstructUsing
				(
					(src, context) => new SingleOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SingleOrDefaultOperatorParameter, SingleOrDefaultOperator>()
				.ConstructUsing
				(
					(src, context) => new SingleOrDefaultOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<SkipOperatorParameter, SkipOperator>();
			CreateMap<StartsWithOperatorParameter, StartsWithOperator>();
			CreateMap<SubstringOperatorParameter, SubstringOperator>();
			CreateMap<SubtractBinaryOperatorParameter, SubtractBinaryOperator>();
			CreateMap<SumOperatorParameter, SumOperator>()
				.ConstructUsing
				(
					(src, context) => new SumOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<TakeOperatorParameter, TakeOperator>();
			CreateMap<ThenByOperatorParameter, ThenByOperator>()
				.ConstructUsing
				(
					(src, context) => new ThenByOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.SelectorBody),
						src.SortDirection,
						src.SelectorParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<TimeOperatorParameter, TimeOperator>();
			CreateMap<ToListOperatorParameter, ToListOperator>();
			CreateMap<ToLowerOperatorParameter, ToLowerOperator>();
			CreateMap<TotalOffsetMinutesOperatorParameter, TotalOffsetMinutesOperator>();
			CreateMap<TotalSecondsOperatorParameter, TotalSecondsOperator>();
			CreateMap<ToUpperOperatorParameter, ToUpperOperator>();
			CreateMap<TrimOperatorParameter, TrimOperator>();
			CreateMap<WhereOperatorParameter, WhereOperator>()
				.ConstructUsing
				(
					(src, context) => new WhereOperator
					(
						(IDictionary<string, ParameterExpression>)context.Items[PARAMETERS_KEY],
						context.Mapper.Map<IExpressionPart>(src.SourceOperand),
						context.Mapper.Map<IExpressionPart>(src.FilterBody),
						src.FilterParameterName
					)
				)
				.ForAllMembers(opt => opt.Ignore());

			CreateMap<YearOperatorParameter, YearOperator>();

            CreateMap<IExpressionParameter, IExpressionPart>()
				.Include<AddBinaryOperatorParameter, AddBinaryOperator>()
				.Include<AllOperatorParameter, AllOperator>()
				.Include<AndBinaryOperatorParameter, AndBinaryOperator>()
				.Include<AnyOperatorParameter, AnyOperator>()
				.Include<AsQueryableOperatorParameter, AsQueryableOperator>()
				.Include<AverageOperatorParameter, AverageOperator>()
				.Include<BinaryOperatorParameter, BinaryOperator>()
				.Include<CastOperatorParameter, CastOperator>()
				.Include<CeilingOperatorParameter, CeilingOperator>()
				.Include<CollectionCastOperatorParameter, CollectionCastOperator>()
				.Include<CollectionConstantOperatorParameter, CollectionConstantOperator>()
				.Include<ConcatOperatorParameter, ConcatOperator>()
				.Include<ConstantOperatorParameter, ConstantOperator>()
				.Include<ContainsOperatorParameter, ContainsOperator>()
				.Include<ConvertCharArrayToStringOperatorParameter, ConvertCharArrayToStringOperator>()
				.Include<ConvertOperatorParameter, ConvertOperator>()
				.Include<ConvertToEnumOperatorParameter, ConvertToEnumOperator>()
				.Include<ConvertToNullableUnderlyingValueOperatorParameter, ConvertToNullableUnderlyingValueOperator>()
				.Include<ConvertToNumericDateOperatorParameter, ConvertToNumericDateOperator>()
				.Include<ConvertToNumericTimeOperatorParameter, ConvertToNumericTimeOperator>()
				.Include<ConvertToStringOperatorParameter, ConvertToStringOperator>()
				.Include<CountOperatorParameter, CountOperator>()
				.Include<CustomMethodOperatorParameter, CustomMethodOperator>()
				.Include<DateOperatorParameter, DateOperator>()
				.Include<DayOperatorParameter, DayOperator>()
				.Include<DistinctOperatorParameter, DistinctOperator>()
				.Include<DivideBinaryOperatorParameter, DivideBinaryOperator>()
				.Include<EndsWithOperatorParameter, EndsWithOperator>()
				.Include<EqualsBinaryOperatorParameter, EqualsBinaryOperator>()
				.Include<FilterLambdaOperatorParameter, FilterLambdaOperator>()
				.Include<FirstOperatorParameter, FirstOperator>()
				.Include<FirstOrDefaultOperatorParameter, FirstOrDefaultOperator>()
				.Include<FloorOperatorParameter, FloorOperator>()
				.Include<FractionalSecondsOperatorParameter, FractionalSecondsOperator>()
				.Include<GreaterThanBinaryOperatorParameter, GreaterThanBinaryOperator>()
				.Include<GreaterThanOrEqualsBinaryOperatorParameter, GreaterThanOrEqualsBinaryOperator>()
				.Include<GroupByOperatorParameter, GroupByOperator>()
				.Include<HasOperatorParameter, HasOperator>()
				.Include<HourOperatorParameter, HourOperator>()
				.Include<IEnumerableSelectorLambdaOperatorParameter, IEnumerableSelectorLambdaOperator>()
				.Include<IndexOfOperatorParameter, IndexOfOperator>()
				.Include<InOperatorParameter, InOperator>()
				.Include<IsOfOperatorParameter, IsOfOperator>()
				.Include<LastOperatorParameter, LastOperator>()
				.Include<LastOrDefaultOperatorParameter, LastOrDefaultOperator>()
				.Include<LengthOperatorParameter, LengthOperator>()
				.Include<LessThanBinaryOperatorParameter, LessThanBinaryOperator>()
				.Include<LessThanOrEqualsBinaryOperatorParameter, LessThanOrEqualsBinaryOperator>()
				.Include<MaxDateTimeOperatorParameter, MaxDateTimeOperator>()
				.Include<MaxOperatorParameter, MaxOperator>()
				.Include<MemberInitOperatorParameter, MemberInitOperator>()
				.Include<MemberSelectorOperatorParameter, MemberSelectorOperator>()
				.Include<MinDateTimeOperatorParameter, MinDateTimeOperator>()
				.Include<MinOperatorParameter, MinOperator>()
				.Include<MinuteOperatorParameter, MinuteOperator>()
				.Include<ModuloBinaryOperatorParameter, ModuloBinaryOperator>()
				.Include<MonthOperatorParameter, MonthOperator>()
				.Include<MultiplyBinaryOperatorParameter, MultiplyBinaryOperator>()
				.Include<NegateOperatorParameter, NegateOperator>()
				.Include<NotEqualsBinaryOperatorParameter, NotEqualsBinaryOperator>()
				.Include<NotOperatorParameter, NotOperator>()
				.Include<NowDateTimeOperatorParameter, NowDateTimeOperator>()
				.Include<OrBinaryOperatorParameter, OrBinaryOperator>()
				.Include<OrderByOperatorParameter, OrderByOperator>()
				.Include<ParameterOperatorParameter, ParameterOperator>()
				.Include<RoundOperatorParameter, RoundOperator>()
				.Include<SecondOperatorParameter, SecondOperator>()
				.Include<SelectManyOperatorParameter, SelectManyOperator>()
				.Include<SelectOperatorParameter, SelectOperator>()
				.Include<SelectorLambdaOperatorParameter, SelectorLambdaOperator>()
				.Include<SingleOperatorParameter, SingleOperator>()
				.Include<SingleOrDefaultOperatorParameter, SingleOrDefaultOperator>()
				.Include<SkipOperatorParameter, SkipOperator>()
				.Include<StartsWithOperatorParameter, StartsWithOperator>()
				.Include<SubstringOperatorParameter, SubstringOperator>()
				.Include<SubtractBinaryOperatorParameter, SubtractBinaryOperator>()
				.Include<SumOperatorParameter, SumOperator>()
				.Include<TakeOperatorParameter, TakeOperator>()
				.Include<ThenByOperatorParameter, ThenByOperator>()
				.Include<TimeOperatorParameter, TimeOperator>()
				.Include<ToListOperatorParameter, ToListOperator>()
				.Include<ToLowerOperatorParameter, ToLowerOperator>()
				.Include<TotalOffsetMinutesOperatorParameter, TotalOffsetMinutesOperator>()
				.Include<TotalSecondsOperatorParameter, TotalSecondsOperator>()
				.Include<ToUpperOperatorParameter, ToUpperOperator>()
				.Include<TrimOperatorParameter, TrimOperator>()
				.Include<WhereOperatorParameter, WhereOperator>()
				.Include<YearOperatorParameter, YearOperator>();
        }
    }
}