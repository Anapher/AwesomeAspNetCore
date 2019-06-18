import { combineReducers } from 'redux';

import auth from '../features/auth/reducer';

const rootReducer = combineReducers({
   auth,
});

export default rootReducer;
