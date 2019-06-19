import { DEFAULT_PREFIX, SIGNALR_CONNECTED } from './action-types';

interface SignalRState {
    isConnected: boolean;
}

const initialState: SignalRState = {
    isConnected: false,
};

export default function(prefix: string = DEFAULT_PREFIX) {
    return function(state = initialState, action: any): SignalRState {
        switch (action.type) {
            case `${prefix}::${SIGNALR_CONNECTED}`:
                return { ...state, isConnected: true };
            default:
                return state;
        }
    };
}
