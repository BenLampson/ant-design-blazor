﻿using Append.AntDesign.Core;
using Microsoft.AspNetCore.Components;
using System;

namespace Append.AntDesign.Components
{
    public partial class TimelineItem : IDisposable
    {
        public const string ItemPrefix = "ant-timeline-item";
        public const string DotPrefix = "ant-timeline-item-head";

        [Parameter] public RenderFragment Dot { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public TimelinePosition Position { get; set; }
        [Parameter] public TimelineColor Color { get; set; } = TimelineColor.Blue;
        [CascadingParameter] public Timeline Parent { get; set; }
        public bool IsLast { get; set; }

        public string ItemCssClasses =>
            ItemPrefix
            .AddClassWhen($"{ItemPrefix}-{Position}", Position != null)
            .AddClassWhen($"{ItemPrefix}-last", IsLast)
            .AddCssClass(Class);

        public string DotCssClasses =>
            DotPrefix
            .AddClassWhen($"{DotPrefix}-custom", Dot != null)
            .AddCssClass($"{DotPrefix}-{Color}");

        protected override void OnInitialized()
        {
            if (Parent == null)
                throw new ArgumentException($"A {nameof(TimelineItem)} has to be contained in a ${nameof(Timeline)}.");

            Parent.Subscribe(this);
            if(Position == null) 
                UpdatePosition();
        }

        internal void UpdatePosition()
        {
            Position = Parent.DetermineItemPosition(this);
        }

        internal void UpdateIsLast(bool isLast)
        {
            IsLast = isLast;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        public void Dispose()
        {
            Parent.Unsubscribe(this);
        }
    }
}