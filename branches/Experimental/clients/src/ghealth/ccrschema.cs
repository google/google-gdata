using System.Xml.Serialization;

namespace ASTM.Org.CCR
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class ContinuityOfCareRecord
	{

		/// <remarks/>
		public string CCRDocumentObjectID;

		/// <remarks/>
		public CodedDescriptionType Language;

		/// <remarks/>
		public string Version;

		/// <remarks/>
		public DateTimeType DateTime;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Patient")]
		public ContinuityOfCareRecordPatient[] Patient;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("ActorLink", IsNullable = false)]
		public ActorReferenceType[] From;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("ActorLink", IsNullable = false)]
		public ActorReferenceType[] To;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Purpose")]
		public PurposeType[] Purpose;

		/// <remarks/>
		public ContinuityOfCareRecordBody Body;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Actor", IsNullable = false)]
		public ActorType[] Actors;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Reference", IsNullable = false)]
		public ReferenceType[] References;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Comment", IsNullable = false)]
		public CommentType[] Comments;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("CCRSignature", IsNullable = false)]
		public SignatureType[] Signatures;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PositionType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(MethodType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(InstructionType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(SiteType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DirectionRoute))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(VehicleTypeProductSize))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(VehicleTypeProductForm))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuredProductTypeProductSize))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuredProductTypeProductForm))]
	[System.Xml.Serialization.XmlRootAttribute("Description", Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class CodedDescriptionType
	{

		/// <remarks/>
		public string Text;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ObjectAttribute")]
		public CodedDescriptionTypeObjectAttribute[] ObjectAttribute;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class CodedDescriptionTypeObjectAttribute
	{

		/// <remarks/>
		public string Attribute;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AttributeValue")]
		public CodedDescriptionTypeObjectAttributeAttributeValue[] AttributeValue;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class CodedDescriptionTypeObjectAttributeAttributeValue
	{

		/// <remarks/>
		public object Value;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ContinuityOfCareRecordBody
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Payer", IsNullable = false)]
		public InsuranceType[] Payers;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("AdvanceDirective", IsNullable = false)]
		public CCRCodedDataObjectType[] AdvanceDirectives;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("SupportProvider", IsNullable = false)]
		public ActorReferenceType[] Support;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Function", IsNullable = false)]
		public FunctionType[] FunctionalStatus;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Problem", IsNullable = false)]
		public ProblemType[] Problems;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("FamilyProblemHistory", IsNullable = false)]
		public FamilyHistoryType[] FamilyHistory;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("SocialHistoryElement", IsNullable = false)]
		public SocialHistoryType[] SocialHistory;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Alert", IsNullable = false)]
		public AlertType[] Alerts;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Medication", IsNullable = false)]
		public StructuredProductType[] Medications;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Equipment", IsNullable = false)]
		public StructuredProductType[] MedicalEquipment;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Immunization", IsNullable = false)]
		public StructuredProductType[] Immunizations;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Result", IsNullable = false)]
		public ResultType[] VitalSigns;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Result", IsNullable = false)]
		public ResultType[] Results;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Procedure", IsNullable = false)]
		public ProcedureType[] Procedures;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Encounter", IsNullable = false)]
		public EncounterType[] Encounters;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Plan", IsNullable = false)]
		public PlanType[] PlanOfCare;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Provider", IsNullable = false)]
		public ActorReferenceType[] HealthCareProviders;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class InsuranceType : CCRCodedDataObjectType
	{

		/// <remarks/>
		public ActorReferenceType PaymentProvider;

		/// <remarks/>
		public ActorReferenceType Subscriber;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Authorization", IsNullable = false)]
		public AuthorizationType[] Authorizations;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(FamilyHistoryTypeFamilyMember))]
	[System.Xml.Serialization.XmlRootAttribute("Actor", Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class ActorReferenceType
	{

		/// <remarks/>
		public string ActorID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ActorRole")]
		public CodedDescriptionType[] ActorRole;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class FamilyHistoryTypeFamilyMember : ActorReferenceType
	{

		/// <remarks/>
		public CurrentHealthStatusType HealthStatus;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class CurrentHealthStatusType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public string CauseOfDeath;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DirectionAdministrationTiming))]
	public class DateTimeType
	{

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public string ExactDateTime;

		/// <remarks/>
		public MeasureType Age;

		/// <remarks/>
		public CodedDescriptionType ApproximateDateTime;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTimeRange")]
		public DateTimeTypeDateTimeRange[] DateTimeRange;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(IndicationTypePhysiologicalParameter))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(VehicleTypeQuantity))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(VehicleTypeProductConcentration))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(VehicleTypeProductStrength))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DoseCalculationTypeVariable))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DirectionDose))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuredProductTypeProductSizeDimension))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuredProductTypeProductConcentration))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuredProductTypeProductStrength))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(QuantityType))]
	[System.Xml.Serialization.XmlRootAttribute("Age", Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class MeasureType
	{

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public MeasureTypeUnits Units;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class MeasureTypeUnits
	{

		/// <remarks/>
		public string Unit;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class CodeType
	{

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public string CodingSystem;

		/// <remarks/>
		public string Version;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class IndicationTypePhysiologicalParameter : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ParameterSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableParameterModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class VehicleTypeQuantity : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string QuantitySequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableQuantityModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class VehicleTypeProductConcentration : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ConcentrationSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableConcentrationModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class VehicleTypeProductStrength : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string StrengthSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableStrengthModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DoseCalculationTypeVariable : MeasureType
	{

		/// <remarks/>
		public string VariableIdentifier;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string VariableSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DirectionDose : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Rate")]
		public RateType[] Rate;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string DoseSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableDoseModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class RateType
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public RateTypeUnits Units;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string RateSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableRateModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class RateTypeUnits
	{

		/// <remarks/>
		public string Unit;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductTypeProductSizeDimension : MeasureType
	{

		/// <remarks/>
		public CodedDescriptionType Description;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductTypeProductConcentration : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ConcentrationSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableConcentrationModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductTypeProductStrength : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string StrengthSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableStrengthModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class QuantityType : MeasureType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string QuantitySequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableQuantityModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DateTimeTypeDateTimeRange
	{

		/// <remarks/>
		public DateTimeTypeDateTimeRangeBeginRange BeginRange;

		/// <remarks/>
		public DateTimeTypeDateTimeRangeEndRange EndRange;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DateTimeTypeDateTimeRangeBeginRange
	{

		/// <remarks/>
		public string ExactDateTime;

		/// <remarks/>
		public MeasureType Age;

		/// <remarks/>
		public CodedDescriptionType ApproximateDateTime;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DateTimeTypeDateTimeRangeEndRange
	{

		/// <remarks/>
		public string ExactDateTime;

		/// <remarks/>
		public MeasureType Age;

		/// <remarks/>
		public CodedDescriptionType ApproximateDateTime;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DirectionAdministrationTiming : DateTimeType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string TimingSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableTimingModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(InternalCCRLinkSource))]
	public class SourceType
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Actor")]
		public ActorReferenceType[] Actor;

		/// <remarks/>
		public DateTimeType DateTime;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class InternalCCRLinkSource : SourceType
	{
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class InternalCCRLink
	{

		/// <remarks/>
		public string LinkID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("LinkRelationship")]
		public string[] LinkRelationship;

		/// <remarks/>
		public InternalCCRLinkSource Source;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class IDTypeSignature
	{

		/// <remarks/>
		public string SignatureID;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class AuthorizationType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Purpose")]
		public PurposeType[] Purpose;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Practitioner", IsNullable = false)]
		public ActorReferenceType[] Practitioners;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Procedure", IsNullable = false)]
		public ProcedureType[] Procedures;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Product", IsNullable = false)]
		public StructuredProductType[] Products;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Medication", IsNullable = false)]
		public StructuredProductType[] Medications;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Immunization", IsNullable = false)]
		public StructuredProductType[] Immunizations;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Service", IsNullable = false)]
		public EncounterType[] Services;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Encounter", IsNullable = false)]
		public EncounterType[] Encounters;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class PurposeType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Description")]
		public CodedDescriptionType[] Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("OrderRequest")]
		public PlanOfCareType[] OrderRequest;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Indication", typeof(IndicationType), IsNullable = false)]
		public IndicationType[][] Indications;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class PlanOfCareType : InterventionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Goal", IsNullable = false)]
		public GoalType[] Goals;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string OrderSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleOrderModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class GoalType : EncounterType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Outcome", IsNullable = false)]
		public CCRCodedDataObjectType[] Milestones;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(SocialHistoryType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PlanType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(InsuranceType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(FunctionType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(FamilyHistoryType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(InterventionType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PlanOfCareType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(AuthorizationType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(TestType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ResultType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderRxHistoryType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(EncounterType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(GoalType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(EpisodeType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ProblemType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuredProductType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(AlertType))]
	public class CCRCodedDataObjectType
	{

		/// <remarks/>
		public string CCRDataObjectID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("IDs")]
		public IDType[] IDs;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public CodedDescriptionType Status;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute("IDs", Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class IDType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public string ID;

		/// <remarks/>
		public ActorReferenceType IssuedBy;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class SocialHistoryType : CCRCodedDataObjectType
	{

		/// <remarks/>
		public EpisodesType Episodes;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class EpisodesType
	{

		/// <remarks/>
		public string Number;

		/// <remarks/>
		public MeasureType Frequency;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Episode")]
		public EpisodeType[] Episode;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class EpisodeType : CCRCodedDataObjectType
	{

		/// <remarks/>
		public DurationType Duration;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DurationType
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public RateTypeUnits Units;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string DurationSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableDurationModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class PlanType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("OrderRequest")]
		public PlanOfCareType[] OrderRequest;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class FunctionType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Problem")]
		public ProblemType[] Problem;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Test")]
		public ResultType[] Test;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ProblemType : CCRCodedDataObjectType
	{

		/// <remarks/>
		public EpisodesType Episodes;

		/// <remarks/>
		public CurrentHealthStatusType HealthStatus;

		/// <remarks/>
		public PatientKnowledgeType PatientKnowledge;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class PatientKnowledgeType
	{

		/// <remarks/>
		public string PatientAware;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ResultType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Procedure")]
		public ProcedureType[] Procedure;

		/// <remarks/>
		public CodedDescriptionType Substance;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Test")]
		public TestType[] Test;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ProcedureType
	{

		/// <remarks/>
		public string CCRDataObjectID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		public IDType IDs;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public CodedDescriptionType Status;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public Location[] Locations;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Practitioner", IsNullable = false)]
		public ActorReferenceType[] Practitioners;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Frequency")]
		public FrequencyType[] Frequency;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Interval")]
		public IntervalType[] Interval;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Duration")]
		public DurationType[] Duration;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Indication", typeof(IndicationType), IsNullable = false)]
		public IndicationType[][] Indications;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Instruction", IsNullable = false)]
		public CodedDescriptionType[] Instructions;

		/// <remarks/>
		public CCRCodedDataObjectType Consent;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Product", IsNullable = false)]
		public StructuredProductType[] Products;

		/// <remarks/>
		public CodedDescriptionType Substance;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Method")]
		public MethodType[] Method;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Position")]
		public PositionType[] Position;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Site")]
		public SiteType[] Site;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Location
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public ActorReferenceType Actor;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class FrequencyType
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public RateTypeUnits Units;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string FrequencySequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableFrequencyModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class IntervalType
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public RateTypeUnits Units;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string IntervalSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableIntervalModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class IndicationType
	{

		/// <remarks/>
		public CodedDescriptionType PRNFlag;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Description")]
		public CodedDescriptionType[] Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Problem")]
		public ProblemType[] Problem;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PhysiologicalParameter")]
		public IndicationTypePhysiologicalParameter[] PhysiologicalParameter;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string IndicationSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleIndicationModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Product")]
		public StructuredProductTypeProduct[] Product;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Quantity")]
		public QuantityType[] Quantity;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute(typeof(Direction), IsNullable = false)]
		public Direction[][] Directions;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Instruction", IsNullable = false)]
		public InstructionType[] PatientInstructions;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Instruction", IsNullable = false)]
		public InstructionType[] FulfillmentInstructions;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Refill", IsNullable = false)]
		public StructuredProductTypeRefill[] Refills;

		/// <remarks/>
		public string SeriesNumber;

		/// <remarks/>
		public CCRCodedDataObjectType Consent;

		/// <remarks/>
		public Reaction Reaction;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Fulfillment", IsNullable = false)]
		public OrderRxHistoryType[] FulfillmentHistory;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductTypeProduct
	{

		/// <remarks/>
		public CodedDescriptionType ProductName;

		/// <remarks/>
		public CodedDescriptionType BrandName;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Strength")]
		public StructuredProductTypeProductStrength[] Strength;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Form")]
		public StructuredProductTypeProductForm[] Form;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Concentration")]
		public StructuredProductTypeProductConcentration[] Concentration;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Size")]
		public StructuredProductTypeProductSize[] Size;

		/// <remarks/>
		public ActorReferenceType Manufacturer;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("IDs")]
		public IDType[] IDs;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ProductSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleProductModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductTypeProductForm : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string FormSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleFormModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductTypeProductSize : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Dimension", IsNullable = false)]
		public StructuredProductTypeProductSizeDimension[] Dimensions;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string SizeSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableSizeModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Direction
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public CodedDescriptionType DoseIndicator;

		/// <remarks/>
		public CodedDescriptionType DeliveryMethod;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Dose")]
		public DirectionDose[] Dose;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DoseCalculation")]
		public DirectionDoseCalculation[] DoseCalculation;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Vehicle")]
		public VehicleType[] Vehicle;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Route")]
		public DirectionRoute[] Route;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Site")]
		public SiteType[] Site;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AdministrationTiming")]
		public DirectionAdministrationTiming[] AdministrationTiming;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Frequency")]
		public FrequencyType[] Frequency;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Interval")]
		public IntervalType[] Interval;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Duration")]
		public DurationType[] Duration;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DoseRestriction")]
		public DirectionDoseRestriction[] DoseRestriction;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Indication")]
		public IndicationType[] Indication;

		/// <remarks/>
		public CodedDescriptionType StopIndicator;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string DirectionSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleDirectionModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DirectionDoseCalculation : DoseCalculationType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Rate")]
		public RateType[] Rate;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string CalculationSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableCalculationModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DirectionDoseRestriction))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DirectionDoseCalculation))]
	public class DoseCalculationType
	{

		/// <remarks/>
		public MeasureType Dose;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Variable")]
		public DoseCalculationTypeVariable[] Variable;

		/// <remarks/>
		public DoseCalculationTypeCalculationEquation CalculationEquation;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DoseCalculationTypeCalculationEquation
	{

		/// <remarks/>
		public string Value;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DirectionDoseRestriction : DoseCalculationType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Rate")]
		public RateType[] Rate;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string RestrictionSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableRestrictionModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class VehicleType
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Product")]
		public VehicleTypeProduct[] Product;

		/// <remarks/>
		public ActorReferenceType Manufacturer;

		/// <remarks/>
		public IDType IDs;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Quantity")]
		public VehicleTypeQuantity[] Quantity;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string VehicleSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleVehicleModifier;

		/// <remarks/>
		public InternalCCRLink InternalCCRLink;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class VehicleTypeProduct
	{

		/// <remarks/>
		public CodedDescriptionType ProductName;

		/// <remarks/>
		public CodedDescriptionType BrandName;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Strength")]
		public VehicleTypeProductStrength[] Strength;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Form")]
		public VehicleTypeProductForm[] Form;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Concentration")]
		public VehicleTypeProductConcentration[] Concentration;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Size")]
		public VehicleTypeProductSize[] Size;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ProductSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleProductModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class VehicleTypeProductForm : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string FormSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleFormModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class VehicleTypeProductSize : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string SizeSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableSizeModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class DirectionRoute : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string RouteSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleRouteModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class SiteType : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string SiteSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleSiteModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class InstructionType : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string instructionSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleInstructionModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class StructuredProductTypeRefill
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Number", DataType = "integer")]
		public string[] Number;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Quantity")]
		public QuantityType[] Quantity;

		/// <remarks/>
		public CodedDescriptionType Status;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Comment")]
		public CommentType[] Comment;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class CommentType
	{

		/// <remarks/>
		public string CommentObjectID;

		/// <remarks/>
		public DateTimeType DateTime;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public ActorReferenceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Reaction
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public CodedDescriptionType Status;

		/// <remarks/>
		public CodedDescriptionType Severity;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Intervention", IsNullable = false)]
		public ReactionIntervention[] Interventions;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ReactionSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleReactionModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ReactionIntervention
	{

		/// <remarks/>
		public string CCRDataObjectID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public CodedDescriptionType Status;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Procedure", IsNullable = false)]
		public ProcedureType[] Procedures;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Product", IsNullable = false)]
		public StructuredProductType[] Products;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Medication", IsNullable = false)]
		public StructuredProductType[] Medications;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Immunization", IsNullable = false)]
		public StructuredProductType[] Immunizations;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Service", IsNullable = false)]
		public EncounterType[] Services;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Encounter", IsNullable = false)]
		public EncounterType[] Encounters;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(GoalType))]
	public class EncounterType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public Location[] Locations;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Practitioner", IsNullable = false)]
		public ActorReferenceType[] Practitioners;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Frequency")]
		public FrequencyType[] Frequency;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Interval")]
		public IntervalType[] Interval;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Duration")]
		public DurationType[] Duration;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Indication", typeof(IndicationType), IsNullable = false)]
		public IndicationType[][] Indications;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Instruction", IsNullable = false)]
		public CodedDescriptionType[] Instructions;

		/// <remarks/>
		public CCRCodedDataObjectType Consent;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class OrderRxHistoryType : CCRCodedDataObjectType
	{

		/// <remarks/>
		public CodedDescriptionType FulfillmentMethod;

		/// <remarks/>
		public ActorReferenceType Provider;

		/// <remarks/>
		public ActorReferenceType Location;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Reaction")]
		public Reaction[] Reaction;

		/// <remarks/>
		public CodedDescriptionType ProductName;

		/// <remarks/>
		public string BrandName;

		/// <remarks/>
		public ActorReferenceType Manufacturer;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("IDs")]
		public IDType[] IDs1;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Strength")]
		public MeasureType[] Strength;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Form")]
		public CodedDescriptionType[] Form;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Concentration")]
		public MeasureType[] Concentration;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Quantity")]
		public MeasureType[] Quantity;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("LabelInstructions")]
		public InstructionType[] LabelInstructions;

		/// <remarks/>
		public string SeriesNumber;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class MethodType : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string MethodSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultipleMethodModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class PositionType : CodedDescriptionType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string PositionSequencePosition;

		/// <remarks/>
		public CodedDescriptionType MultiplePositionModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class TestType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Method")]
		public CodedDescriptionType[] Method;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Agent")]
		public Agent[] Agent;

		/// <remarks/>
		public TestResultType TestResult;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Normal", IsNullable = false)]
		public NormalType[] NormalResult;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Flag")]
		public CodedDescriptionType[] Flag;

		/// <remarks/>
		public CodedDescriptionType ConfidenceValue;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Agent
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Product", IsNullable = false)]
		public StructuredProductType[] Products;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("EnvironmentalAgent", IsNullable = false)]
		public CCRCodedDataObjectType[] EnvironmentalAgents;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Problem", IsNullable = false)]
		public ProblemType[] Problems;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Procedure", IsNullable = false)]
		public ProcedureType[] Procedures;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Result", IsNullable = false)]
		public ResultType[] Results;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class TestResultType
	{

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public RateTypeUnits Units;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Description")]
		public CodedDescriptionType[] Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ResultSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableResultModifier;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class NormalType
	{

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public RateTypeUnits Units;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Code")]
		public CodeType[] Code;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ValueSequencePosition;

		/// <remarks/>
		public CodedDescriptionType VariableNormalModifier;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class FamilyHistoryType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("FamilyMember")]
		public FamilyHistoryTypeFamilyMember[] FamilyMember;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Problem")]
		public FamilyHistoryTypeProblem[] Problem;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class FamilyHistoryTypeProblem
	{

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		public EpisodesType Episodes;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PlanOfCareType))]
	public class InterventionType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Procedure", IsNullable = false)]
		public ProcedureType[] Procedures;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Product", IsNullable = false)]
		public StructuredProductType[] Products;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Medication", IsNullable = false)]
		public StructuredProductType[] Medications;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Immunization", IsNullable = false)]
		public StructuredProductType[] Immunizations;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Service", IsNullable = false)]
		public EncounterType[] Services;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Encounter", IsNullable = false)]
		public EncounterType[] Encounters;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Authorization", IsNullable = false)]
		public AuthorizationType[] Authorizatons;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class AlertType : CCRCodedDataObjectType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Agent")]
		public Agent[] Agent;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Reaction")]
		public Reaction[] Reaction;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ContinuityOfCareRecordPatient
	{

		/// <remarks/>
		public string ActorID;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class SignatureType
	{

		/// <remarks/>
		public string SignatureObjectID;

		/// <remarks/>
		public string ExactDateTime;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("IDs")]
		public IDType[] IDs;

		/// <remarks/>
		public ActorReferenceType Source;

		/// <remarks/>
		public object Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ReferenceType
	{

		/// <remarks/>
		public string ReferenceObjectID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DateTime")]
		public DateTimeType[] DateTime;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public CodedDescriptionType Description;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public ActorReferenceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public Location[] Locations;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class CommunicationType
	{

		/// <remarks/>
		public string Value;

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public string Priority;

		/// <remarks/>
		public CodedDescriptionType Status;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ActorTypeAddress
	{

		/// <remarks/>
		public CodedDescriptionType Type;

		/// <remarks/>
		public string Line1;

		/// <remarks/>
		public string Line2;

		/// <remarks/>
		public string City;

		/// <remarks/>
		public string County;

		/// <remarks/>
		public string State;

		/// <remarks/>
		public string Country;

		/// <remarks/>
		public string PostalCode;

		/// <remarks/>
		public string Priority;

		/// <remarks/>
		public CodedDescriptionType Status;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Preferred;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ActorTypeInformationSystem
	{

		/// <remarks/>
		public string Name;

		/// <remarks/>
		public string Type;

		/// <remarks/>
		public string Version;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ActorTypeOrganization
	{

		/// <remarks/>
		public string Name;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class PersonNameType
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Given")]
		public string[] Given;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Middle")]
		public string[] Middle;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Family")]
		public string[] Family;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Suffix")]
		public string[] Suffix;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Title")]
		public string[] Title;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("NickName")]
		public object[] NickName;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ActorTypePersonName
	{

		/// <remarks/>
		public PersonNameType BirthName;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AdditionalName")]
		public PersonNameType[] AdditionalName;

		/// <remarks/>
		public PersonNameType CurrentName;

		/// <remarks/>
		public string DisplayName;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ActorTypePerson
	{

		/// <remarks/>
		public ActorTypePersonName Name;

		/// <remarks/>
		public DateTimeType DateOfBirth;

		/// <remarks/>
		public CodedDescriptionType Gender;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	public class ActorType
	{

		/// <remarks/>
		public string ActorObjectID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Organization", typeof(ActorTypeOrganization))]
		[System.Xml.Serialization.XmlElementAttribute("InformationSystem", typeof(ActorTypeInformationSystem))]
		[System.Xml.Serialization.XmlElementAttribute("Person", typeof(ActorTypePerson))]
		public object Item;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("IDs")]
		public IDType[] IDs;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Relation")]
		public CodedDescriptionType[] Relation;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Specialty")]
		public CodedDescriptionType[] Specialty;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Address")]
		public ActorTypeAddress[] Address;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Telephone")]
		public CommunicationType[] Telephone;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("EMail")]
		public CommunicationType[] EMail;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("URL")]
		public CommunicationType[] URL;

		/// <remarks/>
		public CodedDescriptionType Status;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Source")]
		public SourceType[] Source;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InternalCCRLink")]
		public InternalCCRLink[] InternalCCRLink;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ReferenceID")]
		public string[] ReferenceID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CommentID")]
		public string[] CommentID;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Signature")]
		public IDTypeSignature[] Signature;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Directions
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Direction")]
		public Direction[] Direction;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Encounters
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Encounter")]
		public EncounterType[] Encounter;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Goals
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Goal")]
		public GoalType[] Goal;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Immunizations
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Immunization")]
		public StructuredProductType[] Immunization;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Indications
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Indication")]
		public IndicationType[] Indication;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Instructions
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Instruction")]
		public CodedDescriptionType[] Instruction;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Locations
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Location")]
		public Location[] Location;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Medications
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Medication")]
		public StructuredProductType[] Medication;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Practitioners
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Practitioner")]
		public ActorReferenceType[] Practitioner;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Procedures
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Procedure")]
		public ProcedureType[] Procedure;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Products
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Product")]
		public StructuredProductType[] Product;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:astm-org:CCR")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:astm-org:CCR", IsNullable = false)]
	public class Services
	{

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Service")]
		public EncounterType[] Service;
	}
}