namespace AdaptiveTriggerLibrary.Triggers.SoftwareInterfaceTriggers
{
    using Windows.Foundation.Metadata;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if a specified API (type or contract) availability
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class ApiInformationTrigger : AdaptiveTriggerBase<bool, IGenericModifier>,
                                         IStaticTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private string _contractName;
        private int _contractMajorVersion;
        private int? _contractMinorVersion;
        private string _typeName;
        private bool _requireTypeAndContract;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets the required contract name.
        /// </summary>
        public string ContractName
        {
            get { return _contractName; }
            set
            {
                if (value == _contractName) return;
                _contractName = value;
                CurrentValue = GetCurrentValue();
            }
        }

        /// <summary>
        ///  Gets or sets the required contract major version.
        /// </summary>
        /// <remarks>Default is 1.</remarks>
        public int ContractMajorVersion
        {
            get { return _contractMajorVersion; }
            set
            {
                if (value == _contractMajorVersion) return;
                _contractMajorVersion = value;
                CurrentValue = GetCurrentValue();
            }
        }

        /// <summary>
        /// Gets or sets the required contract minor version.
        /// </summary>
        public int? ContractMinorVersion
        {
            get { return _contractMinorVersion; }
            set
            {
                if (value == _contractMinorVersion) return;
                _contractMinorVersion = value;
                CurrentValue = GetCurrentValue();
            }
        }

        /// <summary>
        /// Gets or sets the required type name.
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set
            {
                if (value == _typeName) return;
                _typeName = value;
                CurrentValue = GetCurrentValue();
            }
        }

        /// <summary>
        /// Gets or sets if type and contract must be present.
        /// </summary>
        /// <remarks>Default is false.</remarks>
        public bool RequireTypeAndContract
        {
            get { return _requireTypeAndContract; }
            set
            {
                if (value == _requireTypeAndContract) return;
                _requireTypeAndContract = value;
                CurrentValue = GetCurrentValue();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiInformationTrigger"/> class.
        /// Default condition: true.
        /// Default modifier: <see cref="EqualsModifier{Boolean}"/>.
        /// </summary>
        public ApiInformationTrigger()
            : base(true, new EqualsModifier<bool>())
        {
            _contractMajorVersion = 1;
            _contractMinorVersion = null;
            _requireTypeAndContract = false;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool GetCurrentValue()
        {
            // Evaluation Flags
            var anySpecified = false;
            var anyApiPresent = false;
            var allApiPresent = true;

            // Check type availability
            if (!string.IsNullOrEmpty(TypeName))
            {
                anySpecified = true;
                if (ApiInformation.IsTypePresent(TypeName))
                    anyApiPresent = true;
                else
                    allApiPresent = false;
            }

            // Check contract availability
            if (!string.IsNullOrEmpty(ContractName))
            {
                anySpecified = true;

                // Evaluate, using minor version if specified
                var contractMet = ContractMinorVersion.HasValue
                    ? ApiInformation.IsApiContractPresent(ContractName, (ushort)ContractMajorVersion, (ushort)ContractMinorVersion.Value)
                    : ApiInformation.IsApiContractPresent(ContractName, (ushort)ContractMajorVersion);
                if (contractMet)
                    anyApiPresent = true;
                else
                    allApiPresent = false;
            }

            // Don't trigger if no APIs were specified at all
            if (!anySpecified)
                return false;
            
            // Type and contract required
            if (RequireTypeAndContract)
                return allApiPresent;

            // Type or contract is enough
            return anyApiPresent;
        }

        #endregion
    }
}