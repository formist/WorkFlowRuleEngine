using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowRuleEngine.Rules
{
    public class PredicateRuleWrapper<T> : PredicateRule<T>
    {
        
        public void Evaluate(T input)
        {
            bool result = EvaluateCondition(input);

            if (result)
            {
                EvaluateInCaseOfTrue(input);
            }else{
                EvaluateInCaseOfFalse(input);
            }
        }


        private bool EvaluateCondition(T input)
        {
            if (predicate == null && ruleCondition == null)
                throw new Exception("Predicate is null");

            bool result = false;

            if (predicate != null)
            {
                result = predicate(input);
            }
            else
            {
                object rez = ruleCondition.Evaluate(input);
                result = bool.Parse(rez.ToString());
            }

            return result;
        }
        private bool EvaluateCondition(T input, PredicateRule<T> pred)
        {
            if (pred == null)
                throw new Exception("PredicateRule is null");
            if (pred.predicate == null && pred.ruleCondition == null)
                throw new Exception("PredicateRule is Empty");
            bool result = false;

            if (pred.predicate != null)
            {
                result = pred.predicate(input);
            }
            else
            {
                object rez = pred.ruleCondition.Evaluate(input);
                result = bool.Parse(rez.ToString());
            }


            return result;
        }
        private object EvaluateInCaseOfTrue(T input)
        {
            System.Diagnostics.Debug.WriteLine("True");
            if (ruleChild != null)
            {
                bool result = EvaluateCondition(input, ruleChild);
                if (result)
                {
                    inCaseOfTrue = ruleChild.inCaseOfTrue;
                    ruleInCaseOfTrue = ruleChild.ruleInCaseOfTrue;
                }
                else
                {
                    inCaseOfTrue = ruleChild.inCaseOfFalse;
                    ruleInCaseOfTrue = ruleChild.ruleInCaseOfFalse;
                }
            }
            if (inCaseOfTrue == null && ruleInCaseOfTrue == null)
                throw new Exception("Please set the method in case of the method is evaluated to True");

            if (inCaseOfTrue != null)
            {
                inCaseOfTrue(input);
            }
            else
            {
                ruleInCaseOfTrue.Evaluate(input);
            }

            return null;
        }


        private object EvaluateInCaseOfFalse(T input)
        {
            System.Diagnostics.Debug.WriteLine("False");
            if (ruleChild != null)
            {
                bool result = EvaluateCondition(input, ruleChild);
                if (result)
                {
                    inCaseOfFalse = ruleChild.inCaseOfTrue;
                    ruleInCaseOfFalse = ruleChild.ruleInCaseOfTrue;
                }
                else
                {
                    inCaseOfFalse = ruleChild.inCaseOfFalse;
                    ruleInCaseOfFalse = ruleChild.ruleInCaseOfFalse;
                }
            }
            if (inCaseOfFalse == null && ruleInCaseOfFalse == null)
                throw new Exception("Please set the method in case of the method is evaluated to True");

            if (inCaseOfFalse != null)
            {
                inCaseOfFalse(input);
            }
            else
            {
                ruleInCaseOfFalse.Evaluate(input);
            }

            return null;
        }
    }
}
