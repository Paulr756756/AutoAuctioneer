using Fluxor;

namespace auc_client.Store.Base;

public class BaseReducer {

    [ReducerMethod]
    public static BaseState ReduceBaseFetchDataResultAction(BaseState state, FetchBaseDataResultAction action) {
        
        return new BaseState(action.UserId, action.Username, action.JwtToken);
    }
}