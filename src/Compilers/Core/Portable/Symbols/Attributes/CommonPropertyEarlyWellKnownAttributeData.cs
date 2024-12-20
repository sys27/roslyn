﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.CodeAnalysis
{
    /// <summary>
    /// Information decoded from early well-known custom attributes applied on a property.
    /// </summary>
    internal class CommonPropertyEarlyWellKnownAttributeData : EarlyWellKnownAttributeData
    {
        #region ObsoleteAttribute
        private ObsoleteAttributeData _obsoleteAttributeData = ObsoleteAttributeData.Uninitialized;
        [DisallowNull]
        public ObsoleteAttributeData? ObsoleteAttributeData
        {
            get
            {
                VerifySealed(expected: true);
                return _obsoleteAttributeData.IsUninitialized ? null : _obsoleteAttributeData;
            }
            set
            {
                VerifySealed(expected: false);
                Debug.Assert(value != null);
                Debug.Assert(!value.IsUninitialized);

                if (PEModule.IsMoreImportantObsoleteKind(_obsoleteAttributeData.Kind, value.Kind))
                    return;

                _obsoleteAttributeData = value;
                SetDataStored();
            }
        }
        #endregion

        #region OverloadResolutionPriorityAttribute
        private int _overloadResolutionPriority = 0;
        [DisallowNull]
        public int OverloadResolutionPriority
        {
            get
            {
                VerifySealed(expected: true);
                return _overloadResolutionPriority;
            }
            set
            {
                VerifySealed(expected: false);
                _overloadResolutionPriority = value;
                SetDataStored();
            }
        }
        #endregion
    }
}
