using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace ProtoNet
{
     public class ProtoObject : DynamicObject
    {
        private readonly Dictionary<string, object> _members = new Dictionary<string, object>();
        private static readonly Dictionary<Type, ExpandoObject> Prototypes = new Dictionary<Type, ExpandoObject>();

        protected ProtoObject()
        {
            if (Prototype == null) Prototypes.Add(GetType(), new ExpandoObject());
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;

            using (Proto.CreateContext(this))
            {
                var success = _members.TryGetValue(binder.Name, out var member);

                switch (success)
                {
                    case true when member is Delegate action:
                        result = InvokeDelegate(action, args);
                        break;
                    case false:
                    {
                        member = FindPrototypeMember(binder.Name, GetType());

                        if (member is Delegate action)
                        {
                            result = InvokeDelegate(action, args);
                            success = true;
                        }

                        break;
                    }
                }

                return success;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var success = _members.TryGetValue(binder.Name, out result);

            if (!success)
            {
                var member = FindPrototypeMember(binder.Name, GetType());

                if (member != null)
                {
                    result = member;
                    success = true;
                }
            }

            if (result == null)
            {
                result = Proto.Undefined;
                success = true;
            }

            return success;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_members.ContainsKey(binder.Name))
                _members[binder.Name] = value;
            else
                _members.Add(binder.Name, value);

            return true;
        }

        private static object InvokeDelegate(Delegate member, object[] args)
        {
            object result;

            try
            {
                result = member.DynamicInvoke(args);
            }
            catch (TargetInvocationException ex)
            {
                var newEx = typeof(Exception)
                    .GetMethod("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.Invoke(ex.InnerException, new object[0]);

                throw (Exception) newEx;
            }

            return result;
        }

        private static object FindPrototypeMember(string memberName, Type type)
        {
            while (true)
            {
                if (string.IsNullOrWhiteSpace(memberName) || type == null) return null;

                if (!Prototypes.ContainsKey(type)) return null;

                var prototype = Prototypes[type] as IDictionary<string, object>;

                if (prototype.ContainsKey(memberName)) return prototype[memberName];
                type = type.BaseType;
            }
        }

        private dynamic Prototype => Prototypes.ContainsKey(GetType()) ? Prototypes[GetType()] : null;
    }

     internal class ProtoObjectImpl : ProtoObject
     {
     }
}