using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace MvvmValidation
{
	[ContractClassFor(typeof(IValidationTarget))]
// ReSharper disable InconsistentNaming
	internal abstract class IValidationTargetContract : IValidationTarget
// ReSharper restore InconsistentNaming
	{
		public void NotifyValidtionCompleted(ValidationResult validationResult)
		{
			Contract.Requires(validationResult != null);

			throw new NotImplementedException();
		}

		public IEnumerable<object> UnwrapTargets()
		{
			Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);

			throw new NotImplementedException();
		}

		public bool IsMatch(object target)
		{
			Contract.Requires(target != null);

			throw new NotImplementedException();
		}
	}
}