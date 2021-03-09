using AutoMapper;
using Contoso.Parameters.Expressions;
using Contoso.Bsl.Configuration.ExpressionDescriptors;
using System;

namespace Contoso.AutoMapperProfiles
{
    public class ParameterToDescriptorMappingProfile : Profile
    {
        public ParameterToDescriptorMappingProfile()
        {
			CreateMap<AddBinaryOperatorParameter, AddBinaryOperatorDescriptor>();
			CreateMap<AllOperatorParameter, AllOperatorDescriptor>();
			CreateMap<AndBinaryOperatorParameter, AndBinaryOperatorDescriptor>();
			CreateMap<AnyOperatorParameter, AnyOperatorDescriptor>();
			CreateMap<AsQueryableOperatorParameter, AsQueryableOperatorDescriptor>();
			CreateMap<AverageOperatorParameter, AverageOperatorDescriptor>();
			CreateMap<BinaryOperatorParameter, BinaryOperatorDescriptor>();
			CreateMap<CastOperatorParameter, CastOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<CeilingOperatorParameter, CeilingOperatorDescriptor>();
			CreateMap<CollectionCastOperatorParameter, CollectionCastOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<CollectionConstantOperatorParameter, CollectionConstantOperatorDescriptor>()
				.ForMember(dest => dest.ElementType, opts => opts.MapFrom(x => x.ElementType.AssemblyQualifiedName));
			CreateMap<ConcatOperatorParameter, ConcatOperatorDescriptor>();
			CreateMap<ConstantOperatorParameter, ConstantOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ContainsOperatorParameter, ContainsOperatorDescriptor>();
			CreateMap<ConvertCharArrayToStringOperatorParameter, ConvertCharArrayToStringOperatorDescriptor>();
			CreateMap<ConvertOperatorParameter, ConvertOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ConvertToEnumOperatorParameter, ConvertToEnumOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ConvertToNullableUnderlyingValueOperatorParameter, ConvertToNullableUnderlyingValueOperatorDescriptor>();
			CreateMap<ConvertToNumericDateOperatorParameter, ConvertToNumericDateOperatorDescriptor>();
			CreateMap<ConvertToNumericTimeOperatorParameter, ConvertToNumericTimeOperatorDescriptor>();
			CreateMap<ConvertToStringOperatorParameter, ConvertToStringOperatorDescriptor>();
			CreateMap<CountOperatorParameter, CountOperatorDescriptor>();
			CreateMap<CustomMethodOperatorParameter, CustomMethodOperatorDescriptor>();
			CreateMap<DateOperatorParameter, DateOperatorDescriptor>();
			CreateMap<DayOperatorParameter, DayOperatorDescriptor>();
			CreateMap<DistinctOperatorParameter, DistinctOperatorDescriptor>();
			CreateMap<DivideBinaryOperatorParameter, DivideBinaryOperatorDescriptor>();
			CreateMap<EndsWithOperatorParameter, EndsWithOperatorDescriptor>();
			CreateMap<EqualsBinaryOperatorParameter, EqualsBinaryOperatorDescriptor>();
			CreateMap<FilterLambdaOperatorParameter, FilterLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName));
			CreateMap<FirstOperatorParameter, FirstOperatorDescriptor>();
			CreateMap<FirstOrDefaultOperatorParameter, FirstOrDefaultOperatorDescriptor>();
			CreateMap<FloorOperatorParameter, FloorOperatorDescriptor>();
			CreateMap<FractionalSecondsOperatorParameter, FractionalSecondsOperatorDescriptor>();
			CreateMap<GreaterThanBinaryOperatorParameter, GreaterThanBinaryOperatorDescriptor>();
			CreateMap<GreaterThanOrEqualsBinaryOperatorParameter, GreaterThanOrEqualsBinaryOperatorDescriptor>();
			CreateMap<GroupByOperatorParameter, GroupByOperatorDescriptor>();
			CreateMap<HasOperatorParameter, HasOperatorDescriptor>();
			CreateMap<HourOperatorParameter, HourOperatorDescriptor>();
			CreateMap<IEnumerableSelectorLambdaOperatorParameter, IEnumerableSelectorLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName));
			CreateMap<IndexOfOperatorParameter, IndexOfOperatorDescriptor>();
			CreateMap<InOperatorParameter, InOperatorDescriptor>();
			CreateMap<IsOfOperatorParameter, IsOfOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<LastOperatorParameter, LastOperatorDescriptor>();
			CreateMap<LastOrDefaultOperatorParameter, LastOrDefaultOperatorDescriptor>();
			CreateMap<LengthOperatorParameter, LengthOperatorDescriptor>();
			CreateMap<LessThanBinaryOperatorParameter, LessThanBinaryOperatorDescriptor>();
			CreateMap<LessThanOrEqualsBinaryOperatorParameter, LessThanOrEqualsBinaryOperatorDescriptor>();
			CreateMap<MaxDateTimeOperatorParameter, MaxDateTimeOperatorDescriptor>();
			CreateMap<MaxOperatorParameter, MaxOperatorDescriptor>();
			CreateMap<MemberInitOperatorParameter, MemberInitOperatorDescriptor>()
				.ForMember(dest => dest.NewType, opts => opts.MapFrom(x => x.NewType.AssemblyQualifiedName));
			CreateMap<MemberSelectorOperatorParameter, MemberSelectorOperatorDescriptor>();
			CreateMap<MinDateTimeOperatorParameter, MinDateTimeOperatorDescriptor>();
			CreateMap<MinOperatorParameter, MinOperatorDescriptor>();
			CreateMap<MinuteOperatorParameter, MinuteOperatorDescriptor>();
			CreateMap<ModuloBinaryOperatorParameter, ModuloBinaryOperatorDescriptor>();
			CreateMap<MonthOperatorParameter, MonthOperatorDescriptor>();
			CreateMap<MultiplyBinaryOperatorParameter, MultiplyBinaryOperatorDescriptor>();
			CreateMap<NegateOperatorParameter, NegateOperatorDescriptor>();
			CreateMap<NotEqualsBinaryOperatorParameter, NotEqualsBinaryOperatorDescriptor>();
			CreateMap<NotOperatorParameter, NotOperatorDescriptor>();
			CreateMap<NowDateTimeOperatorParameter, NowDateTimeOperatorDescriptor>();
			CreateMap<OrBinaryOperatorParameter, OrBinaryOperatorDescriptor>();
			CreateMap<OrderByOperatorParameter, OrderByOperatorDescriptor>();
			CreateMap<ParameterOperatorParameter, ParameterOperatorDescriptor>();
			CreateMap<RoundOperatorParameter, RoundOperatorDescriptor>();
			CreateMap<SecondOperatorParameter, SecondOperatorDescriptor>();
			CreateMap<SelectManyOperatorParameter, SelectManyOperatorDescriptor>();
			CreateMap<SelectOperatorParameter, SelectOperatorDescriptor>();
			CreateMap<SelectorLambdaOperatorParameter, SelectorLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName))
				.ForMember(dest => dest.BodyType, opts => opts.MapFrom(x => x.BodyType.AssemblyQualifiedName));
			CreateMap<SingleOperatorParameter, SingleOperatorDescriptor>();
			CreateMap<SingleOrDefaultOperatorParameter, SingleOrDefaultOperatorDescriptor>();
			CreateMap<SkipOperatorParameter, SkipOperatorDescriptor>();
			CreateMap<StartsWithOperatorParameter, StartsWithOperatorDescriptor>();
			CreateMap<SubstringOperatorParameter, SubstringOperatorDescriptor>();
			CreateMap<SubtractBinaryOperatorParameter, SubtractBinaryOperatorDescriptor>();
			CreateMap<SumOperatorParameter, SumOperatorDescriptor>();
			CreateMap<TakeOperatorParameter, TakeOperatorDescriptor>();
			CreateMap<ThenByOperatorParameter, ThenByOperatorDescriptor>();
			CreateMap<TimeOperatorParameter, TimeOperatorDescriptor>();
			CreateMap<ToListOperatorParameter, ToListOperatorDescriptor>();
			CreateMap<ToLowerOperatorParameter, ToLowerOperatorDescriptor>();
			CreateMap<TotalOffsetMinutesOperatorParameter, TotalOffsetMinutesOperatorDescriptor>();
			CreateMap<TotalSecondsOperatorParameter, TotalSecondsOperatorDescriptor>();
			CreateMap<ToUpperOperatorParameter, ToUpperOperatorDescriptor>();
			CreateMap<TrimOperatorParameter, TrimOperatorDescriptor>();
			CreateMap<WhereOperatorParameter, WhereOperatorDescriptor>();
			CreateMap<YearOperatorParameter, YearOperatorDescriptor>();

            CreateMap<IExpressionParameter, OperatorDescriptorBase>()
				.Include<AddBinaryOperatorParameter, AddBinaryOperatorDescriptor>()
				.Include<AllOperatorParameter, AllOperatorDescriptor>()
				.Include<AndBinaryOperatorParameter, AndBinaryOperatorDescriptor>()
				.Include<AnyOperatorParameter, AnyOperatorDescriptor>()
				.Include<AsQueryableOperatorParameter, AsQueryableOperatorDescriptor>()
				.Include<AverageOperatorParameter, AverageOperatorDescriptor>()
				.Include<BinaryOperatorParameter, BinaryOperatorDescriptor>()
				.Include<CastOperatorParameter, CastOperatorDescriptor>()
				.Include<CeilingOperatorParameter, CeilingOperatorDescriptor>()
				.Include<CollectionCastOperatorParameter, CollectionCastOperatorDescriptor>()
				.Include<CollectionConstantOperatorParameter, CollectionConstantOperatorDescriptor>()
				.Include<ConcatOperatorParameter, ConcatOperatorDescriptor>()
				.Include<ConstantOperatorParameter, ConstantOperatorDescriptor>()
				.Include<ContainsOperatorParameter, ContainsOperatorDescriptor>()
				.Include<ConvertCharArrayToStringOperatorParameter, ConvertCharArrayToStringOperatorDescriptor>()
				.Include<ConvertOperatorParameter, ConvertOperatorDescriptor>()
				.Include<ConvertToEnumOperatorParameter, ConvertToEnumOperatorDescriptor>()
				.Include<ConvertToNullableUnderlyingValueOperatorParameter, ConvertToNullableUnderlyingValueOperatorDescriptor>()
				.Include<ConvertToNumericDateOperatorParameter, ConvertToNumericDateOperatorDescriptor>()
				.Include<ConvertToNumericTimeOperatorParameter, ConvertToNumericTimeOperatorDescriptor>()
				.Include<ConvertToStringOperatorParameter, ConvertToStringOperatorDescriptor>()
				.Include<CountOperatorParameter, CountOperatorDescriptor>()
				.Include<CustomMethodOperatorParameter, CustomMethodOperatorDescriptor>()
				.Include<DateOperatorParameter, DateOperatorDescriptor>()
				.Include<DayOperatorParameter, DayOperatorDescriptor>()
				.Include<DistinctOperatorParameter, DistinctOperatorDescriptor>()
				.Include<DivideBinaryOperatorParameter, DivideBinaryOperatorDescriptor>()
				.Include<EndsWithOperatorParameter, EndsWithOperatorDescriptor>()
				.Include<EqualsBinaryOperatorParameter, EqualsBinaryOperatorDescriptor>()
				.Include<FilterLambdaOperatorParameter, FilterLambdaOperatorDescriptor>()
				.Include<FirstOperatorParameter, FirstOperatorDescriptor>()
				.Include<FirstOrDefaultOperatorParameter, FirstOrDefaultOperatorDescriptor>()
				.Include<FloorOperatorParameter, FloorOperatorDescriptor>()
				.Include<FractionalSecondsOperatorParameter, FractionalSecondsOperatorDescriptor>()
				.Include<GreaterThanBinaryOperatorParameter, GreaterThanBinaryOperatorDescriptor>()
				.Include<GreaterThanOrEqualsBinaryOperatorParameter, GreaterThanOrEqualsBinaryOperatorDescriptor>()
				.Include<GroupByOperatorParameter, GroupByOperatorDescriptor>()
				.Include<HasOperatorParameter, HasOperatorDescriptor>()
				.Include<HourOperatorParameter, HourOperatorDescriptor>()
				.Include<IEnumerableSelectorLambdaOperatorParameter, IEnumerableSelectorLambdaOperatorDescriptor>()
				.Include<IndexOfOperatorParameter, IndexOfOperatorDescriptor>()
				.Include<InOperatorParameter, InOperatorDescriptor>()
				.Include<IsOfOperatorParameter, IsOfOperatorDescriptor>()
				.Include<LastOperatorParameter, LastOperatorDescriptor>()
				.Include<LastOrDefaultOperatorParameter, LastOrDefaultOperatorDescriptor>()
				.Include<LengthOperatorParameter, LengthOperatorDescriptor>()
				.Include<LessThanBinaryOperatorParameter, LessThanBinaryOperatorDescriptor>()
				.Include<LessThanOrEqualsBinaryOperatorParameter, LessThanOrEqualsBinaryOperatorDescriptor>()
				.Include<MaxDateTimeOperatorParameter, MaxDateTimeOperatorDescriptor>()
				.Include<MaxOperatorParameter, MaxOperatorDescriptor>()
				.Include<MemberInitOperatorParameter, MemberInitOperatorDescriptor>()
				.Include<MemberSelectorOperatorParameter, MemberSelectorOperatorDescriptor>()
				.Include<MinDateTimeOperatorParameter, MinDateTimeOperatorDescriptor>()
				.Include<MinOperatorParameter, MinOperatorDescriptor>()
				.Include<MinuteOperatorParameter, MinuteOperatorDescriptor>()
				.Include<ModuloBinaryOperatorParameter, ModuloBinaryOperatorDescriptor>()
				.Include<MonthOperatorParameter, MonthOperatorDescriptor>()
				.Include<MultiplyBinaryOperatorParameter, MultiplyBinaryOperatorDescriptor>()
				.Include<NegateOperatorParameter, NegateOperatorDescriptor>()
				.Include<NotEqualsBinaryOperatorParameter, NotEqualsBinaryOperatorDescriptor>()
				.Include<NotOperatorParameter, NotOperatorDescriptor>()
				.Include<NowDateTimeOperatorParameter, NowDateTimeOperatorDescriptor>()
				.Include<OrBinaryOperatorParameter, OrBinaryOperatorDescriptor>()
				.Include<OrderByOperatorParameter, OrderByOperatorDescriptor>()
				.Include<ParameterOperatorParameter, ParameterOperatorDescriptor>()
				.Include<RoundOperatorParameter, RoundOperatorDescriptor>()
				.Include<SecondOperatorParameter, SecondOperatorDescriptor>()
				.Include<SelectManyOperatorParameter, SelectManyOperatorDescriptor>()
				.Include<SelectOperatorParameter, SelectOperatorDescriptor>()
				.Include<SelectorLambdaOperatorParameter, SelectorLambdaOperatorDescriptor>()
				.Include<SingleOperatorParameter, SingleOperatorDescriptor>()
				.Include<SingleOrDefaultOperatorParameter, SingleOrDefaultOperatorDescriptor>()
				.Include<SkipOperatorParameter, SkipOperatorDescriptor>()
				.Include<StartsWithOperatorParameter, StartsWithOperatorDescriptor>()
				.Include<SubstringOperatorParameter, SubstringOperatorDescriptor>()
				.Include<SubtractBinaryOperatorParameter, SubtractBinaryOperatorDescriptor>()
				.Include<SumOperatorParameter, SumOperatorDescriptor>()
				.Include<TakeOperatorParameter, TakeOperatorDescriptor>()
				.Include<ThenByOperatorParameter, ThenByOperatorDescriptor>()
				.Include<TimeOperatorParameter, TimeOperatorDescriptor>()
				.Include<ToListOperatorParameter, ToListOperatorDescriptor>()
				.Include<ToLowerOperatorParameter, ToLowerOperatorDescriptor>()
				.Include<TotalOffsetMinutesOperatorParameter, TotalOffsetMinutesOperatorDescriptor>()
				.Include<TotalSecondsOperatorParameter, TotalSecondsOperatorDescriptor>()
				.Include<ToUpperOperatorParameter, ToUpperOperatorDescriptor>()
				.Include<TrimOperatorParameter, TrimOperatorDescriptor>()
				.Include<WhereOperatorParameter, WhereOperatorDescriptor>()
				.Include<YearOperatorParameter, YearOperatorDescriptor>();
        }
    }
}