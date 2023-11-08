using auc_client.Shared;
using auc_client.Store.Garage;
using Blazored.LocalStorage;
using Fluxor;
using System.Runtime.CompilerServices;

namespace auc_client.Store.Base;

public class BaseEffects {

    private readonly GenericMethods GenericMethods;
    private readonly ILocalStorageService LocalStorageService;

    public BaseEffects(GenericMethods genericMethods, ILocalStorageService localStorageService ) {
        GenericMethods = genericMethods;
        LocalStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task HandleFetchBaseDataAction(FetchBaseDataAction action, IDispatcher dispatcher) {
        var userId = (await LocalStorageService.GetItemAsStringAsync("userId")).Trim('"').Trim();
        var userName = (await LocalStorageService.GetItemAsStringAsync("userName")).Trim('"').Trim();
        var token = (await LocalStorageService.GetItemAsStringAsync("token")).Trim('"').Trim();
        dispatcher.Dispatch(new FetchGarageDataAction());
        dispatcher.Dispatch(new FetchBaseDataResultAction(userId, userName, token));
    }

}