using Client.Shared;
using Client.Store.Garage;
using Blazored.LocalStorage;
using Fluxor;
using System.Runtime.CompilerServices;

namespace Client.Store.Base;

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
        var isLoggedIn = !(string.IsNullOrEmpty(token) || string.Compare(token, "null")==0);
        dispatcher.Dispatch(new FetchGarageDataAction());
        dispatcher.Dispatch(new FetchBaseDataResultAction(userId, userName, token, isLoggedIn));
    }

}