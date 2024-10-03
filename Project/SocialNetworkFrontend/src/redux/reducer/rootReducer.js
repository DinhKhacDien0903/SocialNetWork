import { combineReducers } from 'redux';
import userReducer from './userReducer';
import chatReducer from './chatReducer';
import loadingReducer from './loadingReducer';

const rootReducer = combineReducers({
    user: userReducer,
    chat: chatReducer,
    loading: loadingReducer,
});

export default rootReducer;
