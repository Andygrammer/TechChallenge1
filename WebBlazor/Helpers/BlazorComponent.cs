﻿using Microsoft.AspNetCore.Components;

namespace WebBlazor.Helpers;

public class BlazorComponent : ComponentBase
{
    private readonly RefreshBroadcast _refresh = RefreshBroadcast.Instance;

    protected override void OnInitialized()
    {
        _refresh.RefreshRequested += DoRefresh;
        base.OnInitialized();
    }

    public void CallRequestRefresh()
    {
        _refresh.CallRequestRefresh();
    }

    private void DoRefresh()
    {
        StateHasChanged();
    }

}
