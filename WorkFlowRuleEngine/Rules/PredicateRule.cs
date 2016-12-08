﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowRuleEngine.Rules
{
    public class PredicateRule<T>
    {
        protected internal Predicate<T> predicate = null;
        protected internal Rule<T> ruleCondition = null;

        protected internal Action<T> inCaseOfTrue = null;
        protected internal Rule<T> ruleInCaseOfTrue = null;

        protected internal Action<T> inCaseOfFalse = null;
        protected internal Rule<T> ruleInCaseOfFalse = null;

        protected PredicateRule<T> ruleChild = null;

        #region Condition
        public PredicateRule<T> Condition(Predicate<T> predicate)
        {
            this.ruleCondition = null;

            this.predicate = predicate;
            return this;
        }

        public PredicateRule<T> Condition(string predicate)
        {
            this.predicate = null;

            ruleCondition = new Rule<T>();
            ruleCondition.Expression(predicate);

            Type returnType = ruleCondition.GetReturnType();
            if (returnType != typeof(bool))
            {
                throw new Exception(string.Concat(predicate, " must return a boolean value"));
            }
            return this;
        }
        #endregion

        #region When False
        
        public PredicateRule<T> WhenFalse(Action<T> inCaseOfFalse)
        {
            ruleInCaseOfFalse = null;

            this.inCaseOfFalse = inCaseOfFalse;
            return this;
        }

        public PredicateRule<T> WhenFalse(string inCaseOfFalse)
        {
            this.inCaseOfFalse = null;

            ruleInCaseOfFalse = new Rule<T>();
            ruleInCaseOfFalse.Expression(inCaseOfFalse);

            return this;
        }

        public PredicateRule<T> WhenFalse(PredicateRule<T> inCaseOfFalse)
        {
            this.ruleChild = inCaseOfFalse;
            return this;
        }
        #endregion

        #region When True
        public PredicateRule<T> WhenTrue(Action<T> inCaseOfTrue)
        {
            ruleInCaseOfTrue = null;

            this.inCaseOfTrue = inCaseOfTrue;
            return this;
        }

        public PredicateRule<T> WhenTrue(string inCaseOfTrue)
        {
            this.inCaseOfTrue = null;

            ruleInCaseOfTrue = new Rule<T>();
            ruleInCaseOfTrue.Expression(inCaseOfTrue);

            return this;
        }
        public PredicateRule<T> WhenTrue(PredicateRule<T> inCaseOfTrue)
        {
            this.ruleChild = inCaseOfTrue;
            return this;
        }
        #endregion

    }
}
