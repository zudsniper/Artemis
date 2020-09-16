﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Artemis.Core.DefaultTypes
{
    internal class StringNotEqualConditionOperator : ConditionOperator
    {
        private readonly MethodInfo _toLower;

        public StringNotEqualConditionOperator()
        {
            _toLower = typeof(string).GetMethod("ToLower", new Type[] { });
        }

        public override IReadOnlyCollection<Type> CompatibleTypes => new List<Type> {typeof(string)};

        public override string Description => "Does not equal";
        public override string Icon => "NotEqualVariant";

        public override BinaryExpression CreateExpression(Expression leftSide, Expression rightSide)
        {
            return Expression.NotEqual(Expression.Call(leftSide, _toLower), Expression.Call(rightSide, _toLower));
        }
    }
}