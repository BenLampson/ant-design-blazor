﻿using Microsoft.AspNetCore.Components;

namespace Append.AntDesign.Components
{
    public partial class Content
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}