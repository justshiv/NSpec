﻿using System;
using NSpec.Domain.Extensions;

namespace NSpec.Domain
{
    public class ClassContext : Context
    {
        private void BuildMethodLevelBefore()
        {
            var before = conventions.GetMethodLevelBefore(type);

            if (before != null) BeforeInstance = i => before.Invoke(i, null);
        }

        private void BuildMethodLevelAct()
        {
            var act = conventions.GetMethodLevelAct(type);

            if (act != null) ActInstance = i => act.Invoke(i, null);
        }

        private void BuildMethodLevelAfter()
        {
            var after = conventions.GetMethodLevelAfter(type);

            if (after != null) AfterInstance = i => after.Invoke(i, null);
        }

        public ClassContext(Type type, Conventions conventions = null)
            : base( type.Name, null, 0 )
        {
            this.type = type;

            this.conventions = conventions ?? new DefaultConventions().Initialize();
        }

        public override void Build(nspec instance=null)
        {
            BuildMethodLevelBefore();

            BuildMethodLevelAct();

            BuildMethodLevelAfter();

            var nspec = type.Instance<nspec>();

            base.Build(nspec);
        }

        public override bool IsSub(Type baseType)
        {
            return baseType == type;
        }

        Conventions conventions;
        public Type type;
    }
}