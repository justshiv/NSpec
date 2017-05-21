using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using NSpec.Domain.Extensions;

namespace NSpec.Domain
{
    public class BeforeChain : HookChainBase
    {
        protected override void RunHooks(nspec instance)
        {
            // parent chain
            RecurseAncestors(c => c.BeforeChain.RunHooks(instance));


            // class (method-level)
            RunContextHooks();
            // context-level
            RunClassHooks(instance);
        }

        protected override bool CanRun(nspec instance)
        {
            return !context.BeforeAllChain.AnyBeforeAllsThrew();
        }

        public BeforeChain(Context context, Conventions conventions)
            : base(context, "before", "beforeAsync", "before_each")
        {
            methodSelector = conventions.GetMethodLevelBefore;
            asyncMethodSelector = conventions.GetAsyncMethodLevelBefore;
        }
    }
}