namespace AdaptiveTriggerLibrary.Triggers.SoftwareInterfaceTriggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Windows.System.Profile;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current device family version (OS version)
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class DeviceFamilyVersionTrigger : AdaptiveTriggerBase<Version, IComparableModifier>,
                                              IStaticTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceFamilyVersionTrigger"/> class.
        /// Default condition: 10.0.10240.16405 (first GA release of Windows 10).
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public DeviceFamilyVersionTrigger()
            : base(new Version(10, 0, 10240, 16405), new GreaterThanEqualToModifier())
        {
            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static Version GetCurrentValue()
        {
            var encodedVersion = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
            long parsedEncodedVersion;
            // Try parse to Int64
            if (!long.TryParse(encodedVersion, out parsedEncodedVersion))
                return new Version(0, 0);
            // Convert to HEX
            var hexVersion = parsedEncodedVersion.ToString("X");
            // Split into portions
            var portions = GetPortionsOfHexVersion(hexVersion);
            // Build version
            return new Version(Convert.ToInt32(portions[0], 16),
                               Convert.ToInt32(portions[1], 16),
                               Convert.ToInt32(portions[2], 16),
                               Convert.ToInt32(portions[3], 16));
        }

        private static string[] GetPortionsOfHexVersion(string hexVersion)
        {
            var result = new string[4];
            var chars = hexVersion.ToCharArray().ToList();

            result[3] = GetPortionFromEnd(chars, 4); // Revision number
            result[2] = GetPortionFromEnd(chars, 4); // Build number
            result[1] = GetPortionFromEnd(chars, 4); // Minor version
            result[0] = GetPortionFromEnd(chars, chars.Count); // Major version
            return result;
        }

        private static string GetPortionFromEnd(IList<char> chars, int numberOfElements)
        {
            var resultChars = new List<char>(numberOfElements);

            for (var i = numberOfElements; i > 0; i--)
            {
                resultChars.Insert(0, chars.LastOrDefault());
                chars.RemoveAt(chars.Count - 1);
            }
            
            var sb = new StringBuilder(numberOfElements);
            foreach (var resultChar in resultChars)
                sb.Append(resultChar);

            return sb.ToString();
        }

        #endregion
    }
}