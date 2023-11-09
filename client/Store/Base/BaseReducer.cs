using Fluxor;

namespace Client.Store.Base;

public class BaseReducer {

    [ReducerMethod]
    public static BaseState ReduceBaseFetchDataResultAction(BaseState state, FetchBaseDataResultAction action) {
        
        return new BaseState(action.UserId, action.Username, action.JwtToken, action.IsLoggedIn);
    }
}