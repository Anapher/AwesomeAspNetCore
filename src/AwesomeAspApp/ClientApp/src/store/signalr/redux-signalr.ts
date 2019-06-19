import {
   HubConnection,
   HubConnectionBuilder,
   LogLevel,
   HttpTransportType,
   IHttpConnectionOptions,
} from '@aspnet/signalr';
import { MiddlewareAPI, Dispatch } from 'redux';
import { Action } from './types';
import * as actions from './actions';

interface ReduxSignalROptions {
   prefix: string;
   url: string;
   onOpen?: (hub: HubConnection) => void;
   getOptions?: (getState: () => any) => IHttpConnectionOptions;
}

export default class ReduxSignalR {
   private connection: HubConnection | null = null;

   constructor(private options: ReduxSignalROptions) {}

   public connect = ({ dispatch, getState }: MiddlewareAPI) => {
      const { prefix, url, getOptions } = this.options;

      const transport = HttpTransportType.WebSockets | HttpTransportType.LongPolling;

      this.connection = new HubConnectionBuilder()
         .configureLogging(LogLevel.Trace)
         .withUrl(url, getOptions(getState))
         .build();

      this.connection.start().then(() => dispatch(actions.connected(prefix)));
   };

   public send = (_store: MiddlewareAPI, { payload }: Action) => {
      if (this.connection === null) {
         throw new Error('SignalR is not inialized. Dispatch SIGNALR_CONNECT first.');
      }

      this.connection.send(payload.methodName, ...payload.args);
   };

   public disconnect = (_store: MiddlewareAPI, _action: Action) => {
      if (this.connection === null) {
         throw new Error('SignalR is not inialized. Dispatch SIGNALR_CONNECT first.');
      }

      this.connection.stop();
      this.connection = null;
   };

   public addHandler = ({ dispatch }: MiddlewareAPI, { payload }: Action) => {
      if (this.connection === null) {
         throw new Error('SignalR is not inialized. Dispatch SIGNALR_CONNECT first.');
      }

      const { prefix } = this.options;

      const methodNames: string[] = payload;
      for (const methodName of methodNames) {
         this.connection.on(methodName, (...args) =>
            this.handleMessage(dispatch, prefix, methodName, args),
         );
      }
   };

   public removeHandler = (_store: MiddlewareAPI, { payload }: Action) => {
      if (this.connection === null) {
         throw new Error('SignalR is not inialized. Dispatch SIGNALR_CONNECT first.');
      }

      const methodNames: string[] = payload;
      for (const methodName of methodNames) {
         this.connection.off(methodName);
      }
   };

   /**
    * Handle a message event.
    *
    * @param {Dispatch} dispatch
    * @param {string} prefix
    * @param {MessageEvent} event
    */
   private handleMessage = (dispatch: Dispatch, prefix: string, name: string, args: any[]) => {
      dispatch(actions.message(prefix, name, args.length === 1 ? args[0] : args));
   };
}
