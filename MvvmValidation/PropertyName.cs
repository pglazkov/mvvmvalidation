﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace MvvmValidation
{
	/// <summary>
	/// Gets property name using lambda expressions.
	/// </summary>
	public static class PropertyName
	{
		/// <summary>
		/// Returns the property name by given expression.
		/// </summary>
		/// <typeparam name="T">Type of property's return value</typeparam>
		/// <param name="expression">The expression.</param>
		/// <param name="compound"><c>True</c> if the full expression path should be used to build the string. For example, 
		/// call PropertyName.For(() => MyObj.Property.NestedProperty) will result in string "MyObj.Property.NestedProperty".
		/// If <c>False</c> it will return only the last part, which is "NestedProperty" in the example above.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public static string For<T>(Expression<Func<T>> expression, bool compound = true)
		{
			Expression body = expression.Body;
			return GetMemberName(body, compound);
		}

		/// <summary>
		/// Returns the property name by given expression.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="compound"><c>True</c> if the full expression path should be used to build the string. For example, 
		/// call PropertyName.For(() => MyObj.Property.NestedProperty) will result in string "MyObj.Property.NestedProperty".
		/// If <c>False</c> it will return only the last part, which is "NestedProperty" in the example above.</param>
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public static string For(Expression<Func<object>> expression, bool compound = true)
		{
			Expression body = expression.Body;
			return GetMemberName(body, compound);
		}

		/// <summary>
		/// Gets the member name by give expression.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="compound"><c>True</c> if the full expression path should be used to build the string. For example, 
		/// call GetMemberName(() => MyObj.Property.NestedProperty) will result in string "MyObj.Property.NestedProperty".
		/// If <c>False</c> it will return only the last part, which is "NestedProperty" in the example above.</param>
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		private static string GetMemberName(Expression expression, bool compound = true)
		{
			if (expression is MemberExpression)
			{
				var memberExpression = expression as MemberExpression;

				if (compound && memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
				{
					return GetMemberName(memberExpression.Expression) + "." + memberExpression.Member.Name;
				}

				return memberExpression.Member.Name;
			}

			if (expression is UnaryExpression)
			{
				var unaryExpression = expression as UnaryExpression;

				if (unaryExpression.NodeType != ExpressionType.Convert)
				{
					throw new InvalidOperationException(string.Format("Cannot interpret member from {0}", expression));
				}

				return GetMemberName(unaryExpression.Operand);
			}

			throw new InvalidOperationException(string.Format("Could not determine member from {0}", expression));
		}
	}
}