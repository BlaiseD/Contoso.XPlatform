using AutoMapper;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using Contoso.Bsl.Configuration.ExpressionDescriptors;
using System;

namespace Contoso.AutoMapperProfiles
{
    public class OperatorDescriptorToDescriptorMappingProfile : Profile
    {
        public OperatorDescriptorToDescriptorMappingProfile()
        {
			CreateMap<AddBinaryOperatorDescriptor, AddBinaryDescriptor>();
			CreateMap<AllOperatorDescriptor, AllDescriptor>();
			CreateMap<AndBinaryOperatorDescriptor, AndBinaryDescriptor>();
			CreateMap<AnyOperatorDescriptor, AnyDescriptor>();
			CreateMap<AsQueryableOperatorDescriptor, AsQueryableDescriptor>();
			CreateMap<AverageOperatorDescriptor, AverageDescriptor>();
			CreateMap<BinaryOperatorDescriptor, BinaryDescriptor>();
			CreateMap<CastOperatorDescriptor, CastDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<CeilingOperatorDescriptor, CeilingDescriptor>();
			CreateMap<CollectionCastOperatorDescriptor, CollectionCastDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<CollectionConstantOperatorDescriptor, CollectionConstantDescriptor>()
				.ForMember(dest => dest.ElementType, opts => opts.Ignore())
				.ForCtorParam("elementType", opts => opts.MapFrom(x => Type.GetType(x.ElementType)));
			CreateMap<ConcatOperatorDescriptor, ConcatDescriptor>();
			CreateMap<ConstantOperatorDescriptor, ConstantDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ContainsOperatorDescriptor, ContainsDescriptor>();
			CreateMap<ConvertCharArrayToStringOperatorDescriptor, ConvertCharArrayToStringDescriptor>();
			CreateMap<ConvertOperatorDescriptor, ConvertDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ConvertToEnumOperatorDescriptor, ConvertToEnumDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ConvertToNullableUnderlyingValueOperatorDescriptor, ConvertToNullableUnderlyingValueDescriptor>();
			CreateMap<ConvertToNumericDateOperatorDescriptor, ConvertToNumericDateDescriptor>();
			CreateMap<ConvertToNumericTimeOperatorDescriptor, ConvertToNumericTimeDescriptor>();
			CreateMap<ConvertToStringOperatorDescriptor, ConvertToStringDescriptor>();
			CreateMap<CountOperatorDescriptor, CountDescriptor>();
			CreateMap<CustomMethodOperatorDescriptor, CustomMethodDescriptor>();
			CreateMap<DateOperatorDescriptor, DateDescriptor>();
			CreateMap<DayOperatorDescriptor, DayDescriptor>();
			CreateMap<DistinctOperatorDescriptor, DistinctDescriptor>();
			CreateMap<DivideBinaryOperatorDescriptor, DivideBinaryDescriptor>();
			CreateMap<EndsWithOperatorDescriptor, EndsWithDescriptor>();
			CreateMap<EqualsBinaryOperatorDescriptor, EqualsBinaryDescriptor>();
			CreateMap<FilterLambdaOperatorDescriptor, FilterLambdaDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.Ignore())
				.ForCtorParam("sourceElementType", opts => opts.MapFrom(x => Type.GetType(x.SourceElementType)));
			CreateMap<FirstOperatorDescriptor, FirstDescriptor>();
			CreateMap<FirstOrDefaultOperatorDescriptor, FirstOrDefaultDescriptor>();
			CreateMap<FloorOperatorDescriptor, FloorDescriptor>();
			CreateMap<FractionalSecondsOperatorDescriptor, FractionalSecondsDescriptor>();
			CreateMap<GreaterThanBinaryOperatorDescriptor, GreaterThanBinaryDescriptor>();
			CreateMap<GreaterThanOrEqualsBinaryOperatorDescriptor, GreaterThanOrEqualsBinaryDescriptor>();
			CreateMap<GroupByOperatorDescriptor, GroupByDescriptor>();
			CreateMap<HasOperatorDescriptor, HasDescriptor>();
			CreateMap<HourOperatorDescriptor, HourDescriptor>();
			CreateMap<IEnumerableSelectorLambdaOperatorDescriptor, IEnumerableSelectorLambdaDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.Ignore())
				.ForCtorParam("sourceElementType", opts => opts.MapFrom(x => Type.GetType(x.SourceElementType)));
			CreateMap<IndexOfOperatorDescriptor, IndexOfDescriptor>();
			CreateMap<InOperatorDescriptor, InDescriptor>();
			CreateMap<IsOfOperatorDescriptor, IsOfDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForCtorParam("type", opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<LastOperatorDescriptor, LastDescriptor>();
			CreateMap<LastOrDefaultOperatorDescriptor, LastOrDefaultDescriptor>();
			CreateMap<LengthOperatorDescriptor, LengthDescriptor>();
			CreateMap<LessThanBinaryOperatorDescriptor, LessThanBinaryDescriptor>();
			CreateMap<LessThanOrEqualsBinaryOperatorDescriptor, LessThanOrEqualsBinaryDescriptor>();
			CreateMap<MaxDateTimeOperatorDescriptor, MaxDateTimeDescriptor>();
			CreateMap<MaxOperatorDescriptor, MaxDescriptor>();
			CreateMap<MemberInitOperatorDescriptor, MemberInitDescriptor>()
				.ForMember(dest => dest.NewType, opts => opts.Ignore())
				.ForCtorParam("newType", opts => opts.MapFrom(x => Type.GetType(x.NewType)));
			CreateMap<MemberSelectorOperatorDescriptor, MemberSelectorDescriptor>();
			CreateMap<MinDateTimeOperatorDescriptor, MinDateTimeDescriptor>();
			CreateMap<MinOperatorDescriptor, MinDescriptor>();
			CreateMap<MinuteOperatorDescriptor, MinuteDescriptor>();
			CreateMap<ModuloBinaryOperatorDescriptor, ModuloBinaryDescriptor>();
			CreateMap<MonthOperatorDescriptor, MonthDescriptor>();
			CreateMap<MultiplyBinaryOperatorDescriptor, MultiplyBinaryDescriptor>();
			CreateMap<NegateOperatorDescriptor, NegateDescriptor>();
			CreateMap<NotEqualsBinaryOperatorDescriptor, NotEqualsBinaryDescriptor>();
			CreateMap<NotOperatorDescriptor, NotDescriptor>();
			CreateMap<NowDateTimeOperatorDescriptor, NowDateTimeDescriptor>();
			CreateMap<OrBinaryOperatorDescriptor, OrBinaryDescriptor>();
			CreateMap<OrderByOperatorDescriptor, OrderByDescriptor>();
			CreateMap<ParameterOperatorDescriptor, ParameterDescriptor>();
			CreateMap<RoundOperatorDescriptor, RoundDescriptor>();
			CreateMap<SecondOperatorDescriptor, SecondDescriptor>();
			CreateMap<SelectManyOperatorDescriptor, SelectManyDescriptor>();
			CreateMap<SelectOperatorDescriptor, SelectDescriptor>();
			CreateMap<SelectorLambdaOperatorDescriptor, SelectorLambdaDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.Ignore())
				.ForCtorParam("sourceElementType", opts => opts.MapFrom(x => Type.GetType(x.SourceElementType)))
				.ForMember(dest => dest.BodyType, opts => opts.Ignore())
				.ForCtorParam("bodyType", opts => opts.MapFrom(x => Type.GetType(x.BodyType)));
			CreateMap<SingleOperatorDescriptor, SingleDescriptor>();
			CreateMap<SingleOrDefaultOperatorDescriptor, SingleOrDefaultDescriptor>();
			CreateMap<SkipOperatorDescriptor, SkipDescriptor>();
			CreateMap<StartsWithOperatorDescriptor, StartsWithDescriptor>();
			CreateMap<SubstringOperatorDescriptor, SubstringDescriptor>();
			CreateMap<SubtractBinaryOperatorDescriptor, SubtractBinaryDescriptor>();
			CreateMap<SumOperatorDescriptor, SumDescriptor>();
			CreateMap<TakeOperatorDescriptor, TakeDescriptor>();
			CreateMap<ThenByOperatorDescriptor, ThenByDescriptor>();
			CreateMap<TimeOperatorDescriptor, TimeDescriptor>();
			CreateMap<ToListOperatorDescriptor, ToListDescriptor>();
			CreateMap<ToLowerOperatorDescriptor, ToLowerDescriptor>();
			CreateMap<TotalOffsetMinutesOperatorDescriptor, TotalOffsetMinutesDescriptor>();
			CreateMap<TotalSecondsOperatorDescriptor, TotalSecondsDescriptor>();
			CreateMap<ToUpperOperatorDescriptor, ToUpperDescriptor>();
			CreateMap<TrimOperatorDescriptor, TrimDescriptor>();
			CreateMap<WhereOperatorDescriptor, WhereDescriptor>();
			CreateMap<YearOperatorDescriptor, YearDescriptor>();

            CreateMap<IExpressionOperatorDescriptor, IExpressionDescriptor>()
				.Include<AddBinaryOperatorDescriptor, AddBinaryDescriptor>()
				.Include<AllOperatorDescriptor, AllDescriptor>()
				.Include<AndBinaryOperatorDescriptor, AndBinaryDescriptor>()
				.Include<AnyOperatorDescriptor, AnyDescriptor>()
				.Include<AsQueryableOperatorDescriptor, AsQueryableDescriptor>()
				.Include<AverageOperatorDescriptor, AverageDescriptor>()
				.Include<BinaryOperatorDescriptor, BinaryDescriptor>()
				.Include<CastOperatorDescriptor, CastDescriptor>()
				.Include<CeilingOperatorDescriptor, CeilingDescriptor>()
				.Include<CollectionCastOperatorDescriptor, CollectionCastDescriptor>()
				.Include<CollectionConstantOperatorDescriptor, CollectionConstantDescriptor>()
				.Include<ConcatOperatorDescriptor, ConcatDescriptor>()
				.Include<ConstantOperatorDescriptor, ConstantDescriptor>()
				.Include<ContainsOperatorDescriptor, ContainsDescriptor>()
				.Include<ConvertCharArrayToStringOperatorDescriptor, ConvertCharArrayToStringDescriptor>()
				.Include<ConvertOperatorDescriptor, ConvertDescriptor>()
				.Include<ConvertToEnumOperatorDescriptor, ConvertToEnumDescriptor>()
				.Include<ConvertToNullableUnderlyingValueOperatorDescriptor, ConvertToNullableUnderlyingValueDescriptor>()
				.Include<ConvertToNumericDateOperatorDescriptor, ConvertToNumericDateDescriptor>()
				.Include<ConvertToNumericTimeOperatorDescriptor, ConvertToNumericTimeDescriptor>()
				.Include<ConvertToStringOperatorDescriptor, ConvertToStringDescriptor>()
				.Include<CountOperatorDescriptor, CountDescriptor>()
				.Include<CustomMethodOperatorDescriptor, CustomMethodDescriptor>()
				.Include<DateOperatorDescriptor, DateDescriptor>()
				.Include<DayOperatorDescriptor, DayDescriptor>()
				.Include<DistinctOperatorDescriptor, DistinctDescriptor>()
				.Include<DivideBinaryOperatorDescriptor, DivideBinaryDescriptor>()
				.Include<EndsWithOperatorDescriptor, EndsWithDescriptor>()
				.Include<EqualsBinaryOperatorDescriptor, EqualsBinaryDescriptor>()
				.Include<FilterLambdaOperatorDescriptor, FilterLambdaDescriptor>()
				.Include<FirstOperatorDescriptor, FirstDescriptor>()
				.Include<FirstOrDefaultOperatorDescriptor, FirstOrDefaultDescriptor>()
				.Include<FloorOperatorDescriptor, FloorDescriptor>()
				.Include<FractionalSecondsOperatorDescriptor, FractionalSecondsDescriptor>()
				.Include<GreaterThanBinaryOperatorDescriptor, GreaterThanBinaryDescriptor>()
				.Include<GreaterThanOrEqualsBinaryOperatorDescriptor, GreaterThanOrEqualsBinaryDescriptor>()
				.Include<GroupByOperatorDescriptor, GroupByDescriptor>()
				.Include<HasOperatorDescriptor, HasDescriptor>()
				.Include<HourOperatorDescriptor, HourDescriptor>()
				.Include<IEnumerableSelectorLambdaOperatorDescriptor, IEnumerableSelectorLambdaDescriptor>()
				.Include<IndexOfOperatorDescriptor, IndexOfDescriptor>()
				.Include<InOperatorDescriptor, InDescriptor>()
				.Include<IsOfOperatorDescriptor, IsOfDescriptor>()
				.Include<LastOperatorDescriptor, LastDescriptor>()
				.Include<LastOrDefaultOperatorDescriptor, LastOrDefaultDescriptor>()
				.Include<LengthOperatorDescriptor, LengthDescriptor>()
				.Include<LessThanBinaryOperatorDescriptor, LessThanBinaryDescriptor>()
				.Include<LessThanOrEqualsBinaryOperatorDescriptor, LessThanOrEqualsBinaryDescriptor>()
				.Include<MaxDateTimeOperatorDescriptor, MaxDateTimeDescriptor>()
				.Include<MaxOperatorDescriptor, MaxDescriptor>()
				.Include<MemberInitOperatorDescriptor, MemberInitDescriptor>()
				.Include<MemberSelectorOperatorDescriptor, MemberSelectorDescriptor>()
				.Include<MinDateTimeOperatorDescriptor, MinDateTimeDescriptor>()
				.Include<MinOperatorDescriptor, MinDescriptor>()
				.Include<MinuteOperatorDescriptor, MinuteDescriptor>()
				.Include<ModuloBinaryOperatorDescriptor, ModuloBinaryDescriptor>()
				.Include<MonthOperatorDescriptor, MonthDescriptor>()
				.Include<MultiplyBinaryOperatorDescriptor, MultiplyBinaryDescriptor>()
				.Include<NegateOperatorDescriptor, NegateDescriptor>()
				.Include<NotEqualsBinaryOperatorDescriptor, NotEqualsBinaryDescriptor>()
				.Include<NotOperatorDescriptor, NotDescriptor>()
				.Include<NowDateTimeOperatorDescriptor, NowDateTimeDescriptor>()
				.Include<OrBinaryOperatorDescriptor, OrBinaryDescriptor>()
				.Include<OrderByOperatorDescriptor, OrderByDescriptor>()
				.Include<ParameterOperatorDescriptor, ParameterDescriptor>()
				.Include<RoundOperatorDescriptor, RoundDescriptor>()
				.Include<SecondOperatorDescriptor, SecondDescriptor>()
				.Include<SelectManyOperatorDescriptor, SelectManyDescriptor>()
				.Include<SelectOperatorDescriptor, SelectDescriptor>()
				.Include<SelectorLambdaOperatorDescriptor, SelectorLambdaDescriptor>()
				.Include<SingleOperatorDescriptor, SingleDescriptor>()
				.Include<SingleOrDefaultOperatorDescriptor, SingleOrDefaultDescriptor>()
				.Include<SkipOperatorDescriptor, SkipDescriptor>()
				.Include<StartsWithOperatorDescriptor, StartsWithDescriptor>()
				.Include<SubstringOperatorDescriptor, SubstringDescriptor>()
				.Include<SubtractBinaryOperatorDescriptor, SubtractBinaryDescriptor>()
				.Include<SumOperatorDescriptor, SumDescriptor>()
				.Include<TakeOperatorDescriptor, TakeDescriptor>()
				.Include<ThenByOperatorDescriptor, ThenByDescriptor>()
				.Include<TimeOperatorDescriptor, TimeDescriptor>()
				.Include<ToListOperatorDescriptor, ToListDescriptor>()
				.Include<ToLowerOperatorDescriptor, ToLowerDescriptor>()
				.Include<TotalOffsetMinutesOperatorDescriptor, TotalOffsetMinutesDescriptor>()
				.Include<TotalSecondsOperatorDescriptor, TotalSecondsDescriptor>()
				.Include<ToUpperOperatorDescriptor, ToUpperDescriptor>()
				.Include<TrimOperatorDescriptor, TrimDescriptor>()
				.Include<WhereOperatorDescriptor, WhereDescriptor>()
				.Include<YearOperatorDescriptor, YearDescriptor>();
        }
    }
}