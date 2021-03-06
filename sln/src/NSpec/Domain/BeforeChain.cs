namespace NSpec.Domain
{
    public class BeforeChain : TraversingHookChain
    {
        protected override bool CanRun(nspec instance)
        {
            return !context.BeforeAllChain.AnyThrew();
        }

        public BeforeChain(Context context, Conventions conventions)
            : base(context, "before", "beforeAsync", "before_each")
        {
            methodSelector = conventions.GetMethodLevelBefore;
            asyncMethodSelector = conventions.GetAsyncMethodLevelBefore;
            chainSelector = c => c.BeforeChain;
        }
    }
}
