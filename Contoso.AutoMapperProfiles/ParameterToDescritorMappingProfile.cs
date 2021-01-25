using AutoMapper;
using Contoso.Parameters.Expressions;
using Contoso.Bsl.Configuration.ExpressionDescriptors;
using System;

namespace LogicBuilder.Expressions.Utils.Strutures.Mapping
{
    public class ParameterToDescriptorMappingProfile : Profile
    {
        public ParameterToDescriptorMappingProfile()
        {
			CreateMap<AddBinaryOperatorParameter, AddBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<AllOperatorParameter, AllOperatorDescriptor>().ReverseMap();
			CreateMap<AndBinaryOperatorParameter, AndBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<AnyOperatorParameter, AnyOperatorDescriptor>().ReverseMap();
			CreateMap<AsQueryableOperatorParameter, AsQueryableOperatorDescriptor>().ReverseMap();
			CreateMap<AverageOperatorParameter, AverageOperatorDescriptor>().ReverseMap();
			CreateMap<BinaryOperatorParameter, BinaryOperatorDescriptor>().ReverseMap();
			CreateMap<CastOperatorParameter, CastOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<CeilingOperatorParameter, CeilingOperatorDescriptor>().ReverseMap();
			CreateMap<CollectionCastOperatorParameter, CollectionCastOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<CollectionConstantOperatorParameter, CollectionConstantOperatorDescriptor>()
				.ForMember(dest => dest.ElementType, opts => opts.MapFrom(x => x.ElementType.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.ElementType, opts => opts.MapFrom(x => Type.GetType(x.ElementType)));
			CreateMap<ConcatOperatorParameter, ConcatOperatorDescriptor>().ReverseMap();
			CreateMap<ConstantOperatorParameter, ConstantOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ContainsOperatorParameter, ContainsOperatorDescriptor>().ReverseMap();
			CreateMap<ConvertCharArrayToStringOperatorParameter, ConvertCharArrayToStringOperatorDescriptor>().ReverseMap();
			CreateMap<ConvertOperatorParameter, ConvertOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ConvertToEnumOperatorParameter, ConvertToEnumOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<ConvertToNullableUnderlyingValueOperatorParameter, ConvertToNullableUnderlyingValueOperatorDescriptor>().ReverseMap();
			CreateMap<ConvertToNumericDateOperatorParameter, ConvertToNumericDateOperatorDescriptor>().ReverseMap();
			CreateMap<ConvertToNumericTimeOperatorParameter, ConvertToNumericTimeOperatorDescriptor>().ReverseMap();
			CreateMap<ConvertToStringOperatorParameter, ConvertToStringOperatorDescriptor>().ReverseMap();
			CreateMap<CountOperatorParameter, CountOperatorDescriptor>().ReverseMap();
			CreateMap<CustomMethodOperatorParameter, CustomMethodOperatorDescriptor>().ReverseMap();
			CreateMap<DateOperatorParameter, DateOperatorDescriptor>().ReverseMap();
			CreateMap<DayOperatorParameter, DayOperatorDescriptor>().ReverseMap();
			CreateMap<DistinctOperatorParameter, DistinctOperatorDescriptor>().ReverseMap();
			CreateMap<DivideBinaryOperatorParameter, DivideBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<EndsWithOperatorParameter, EndsWithOperatorDescriptor>().ReverseMap();
			CreateMap<EqualsBinaryOperatorParameter, EqualsBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<FilterLambdaOperatorParameter, FilterLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => Type.GetType(x.SourceElementType)));
			CreateMap<FirstOperatorParameter, FirstOperatorDescriptor>().ReverseMap();
			CreateMap<FirstOrDefaultOperatorParameter, FirstOrDefaultOperatorDescriptor>().ReverseMap();
			CreateMap<FloorOperatorParameter, FloorOperatorDescriptor>().ReverseMap();
			CreateMap<FractionalSecondsOperatorParameter, FractionalSecondsOperatorDescriptor>().ReverseMap();
			CreateMap<GreaterThanBinaryOperatorParameter, GreaterThanBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<GreaterThanOrEqualsBinaryOperatorParameter, GreaterThanOrEqualsBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<GroupByOperatorParameter, GroupByOperatorDescriptor>().ReverseMap();
			CreateMap<HasOperatorParameter, HasOperatorDescriptor>().ReverseMap();
			CreateMap<HourOperatorParameter, HourOperatorDescriptor>().ReverseMap();
			CreateMap<IEnumerableSelectorLambdaOperatorParameter, IEnumerableSelectorLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => Type.GetType(x.SourceElementType)));
			CreateMap<IndexOfOperatorParameter, IndexOfOperatorDescriptor>().ReverseMap();
			CreateMap<InOperatorParameter, InOperatorDescriptor>().ReverseMap();
			CreateMap<IsOfOperatorParameter, IsOfOperatorDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => Type.GetType(x.Type)));
			CreateMap<LastOperatorParameter, LastOperatorDescriptor>().ReverseMap();
			CreateMap<LastOrDefaultOperatorParameter, LastOrDefaultOperatorDescriptor>().ReverseMap();
			CreateMap<LengthOperatorParameter, LengthOperatorDescriptor>().ReverseMap();
			CreateMap<LessThanBinaryOperatorParameter, LessThanBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<LessThanOrEqualsBinaryOperatorParameter, LessThanOrEqualsBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<MaxDateTimeOperatorParameter, MaxDateTimeOperatorDescriptor>().ReverseMap();
			CreateMap<MaxOperatorParameter, MaxOperatorDescriptor>().ReverseMap();
			CreateMap<MemberInitOperatorParameter, MemberInitOperatorDescriptor>()
				.ForMember(dest => dest.NewType, opts => opts.MapFrom(x => x.NewType.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.NewType, opts => opts.MapFrom(x => Type.GetType(x.NewType)));
			CreateMap<MemberSelectorOperatorParameter, MemberSelectorOperatorDescriptor>().ReverseMap();
			CreateMap<MinDateTimeOperatorParameter, MinDateTimeOperatorDescriptor>().ReverseMap();
			CreateMap<MinOperatorParameter, MinOperatorDescriptor>().ReverseMap();
			CreateMap<MinuteOperatorParameter, MinuteOperatorDescriptor>().ReverseMap();
			CreateMap<ModuloBinaryOperatorParameter, ModuloBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<MonthOperatorParameter, MonthOperatorDescriptor>().ReverseMap();
			CreateMap<MultiplyBinaryOperatorParameter, MultiplyBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<NegateOperatorParameter, NegateOperatorDescriptor>().ReverseMap();
			CreateMap<NotEqualsBinaryOperatorParameter, NotEqualsBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<NotOperatorParameter, NotOperatorDescriptor>().ReverseMap();
			CreateMap<NowDateTimeOperatorParameter, NowDateTimeOperatorDescriptor>().ReverseMap();
			CreateMap<OrBinaryOperatorParameter, OrBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<OrderByOperatorParameter, OrderByOperatorDescriptor>().ReverseMap();
			CreateMap<ParameterOperatorParameter, ParameterOperatorDescriptor>().ReverseMap();
			CreateMap<RoundOperatorParameter, RoundOperatorDescriptor>().ReverseMap();
			CreateMap<SecondOperatorParameter, SecondOperatorDescriptor>().ReverseMap();
			CreateMap<SelectManyOperatorParameter, SelectManyOperatorDescriptor>().ReverseMap();
			CreateMap<SelectOperatorParameter, SelectOperatorDescriptor>().ReverseMap();
			CreateMap<SelectorLambdaOperatorParameter, SelectorLambdaOperatorDescriptor>()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => x.SourceElementType.AssemblyQualifiedName))
				.ForMember(dest => dest.BodyType, opts => opts.MapFrom(x => x.BodyType.AssemblyQualifiedName))
			.ReverseMap()
				.ForMember(dest => dest.SourceElementType, opts => opts.MapFrom(x => Type.GetType(x.SourceElementType)))
				.ForMember(dest => dest.BodyType, opts => opts.MapFrom(x => Type.GetType(x.BodyType)));
			CreateMap<SingleOperatorParameter, SingleOperatorDescriptor>().ReverseMap();
			CreateMap<SingleOrDefaultOperatorParameter, SingleOrDefaultOperatorDescriptor>().ReverseMap();
			CreateMap<SkipOperatorParameter, SkipOperatorDescriptor>().ReverseMap();
			CreateMap<StartsWithOperatorParameter, StartsWithOperatorDescriptor>().ReverseMap();
			CreateMap<SubstringOperatorParameter, SubstringOperatorDescriptor>().ReverseMap();
			CreateMap<SubtractBinaryOperatorParameter, SubtractBinaryOperatorDescriptor>().ReverseMap();
			CreateMap<SumOperatorParameter, SumOperatorDescriptor>().ReverseMap();
			CreateMap<TakeOperatorParameter, TakeOperatorDescriptor>().ReverseMap();
			CreateMap<ThenByOperatorParameter, ThenByOperatorDescriptor>().ReverseMap();
			CreateMap<TimeOperatorParameter, TimeOperatorDescriptor>().ReverseMap();
			CreateMap<ToListOperatorParameter, ToListOperatorDescriptor>().ReverseMap();
			CreateMap<ToLowerOperatorParameter, ToLowerOperatorDescriptor>().ReverseMap();
			CreateMap<TotalOffsetMinutesOperatorParameter, TotalOffsetMinutesOperatorDescriptor>().ReverseMap();
			CreateMap<TotalSecondsOperatorParameter, TotalSecondsOperatorDescriptor>().ReverseMap();
			CreateMap<ToUpperOperatorParameter, ToUpperOperatorDescriptor>().ReverseMap();
			CreateMap<TrimOperatorParameter, TrimOperatorDescriptor>().ReverseMap();
			CreateMap<WhereOperatorParameter, WhereOperatorDescriptor>().ReverseMap();
			CreateMap<YearOperatorParameter, YearOperatorDescriptor>().ReverseMap();

            CreateMap<IExpressionParameter, IExpressionDescriptor>()
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