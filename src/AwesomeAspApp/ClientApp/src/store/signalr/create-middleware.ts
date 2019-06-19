import { MiddlewareAPI, Dispatch, Middleware } from 'redux';
import * as actionTypes from './action-types';
import { Options, Action } from './types';
import ReduxSignalR from './redux-signalr';
import { error } from './actions';

/**
 * Default middleware creator options.
 * @private
 */
const defaultOptions = {
   prefix: actionTypes.DEFAULT_PREFIX,
};

export default (rawOptions?: Options): Middleware => {
   const options = { ...defaultOptions, ...rawOptions };
   const { prefix } = options;
   const actionPrefixExp = RegExp(`^${prefix}::`);

   const signalr = new ReduxSignalR(options);

   // Define the list of handlers, now that we have an instance of ReduxWebSocket.
   const handlers: {
      [s: string]: (middleware: MiddlewareAPI, action: Action) => void;
   } = {
      [actionTypes.SIGNALR_CONNECT]: signalr.connect,
      [actionTypes.SIGNALR_DISCONNECT]: signalr.disconnect,
      [actionTypes.SIGNALR_SEND]: signalr.send,
      [actionTypes.SIGNALR_ADDHANDLER]: signalr.addHandler,
      [actionTypes.SIGNALR_REMOVEHANDLER]: signalr.removeHandler,
   };

   // Middleware function.
   return (store: MiddlewareAPI) => next => (action: Action) => {
      const { dispatch } = store;
      const { type: actionType } = action;

      // Check if action type matches prefix
      if (actionType && actionType.match(actionPrefixExp)) {
         const baseActionType = action.type.replace(actionPrefixExp, '');
         const handler = handlers[baseActionType];

         if (handler) {
            try {
               handler(store, action);
            } catch (err) {
               dispatch(error(action, err, prefix));
            }
         }
      }

      return next(action);
   };
};
