import { HubConnection, HubConnectionBuilder, LogLevel, HttpTransportType } from '@aspnet/signalr';
import { MiddlewareAPI, Dispatch } from 'redux';
import { Action } from './types';
import * as actions from './actions';

interface ReduxSignalROptions {
    prefix: string;
    onOpen?: (hub: HubConnection) => void;
}

export default class ReduxSignalR {
    private connection: HubConnection | null = null;

    constructor(private options: ReduxSignalROptions) {}

    connect = ({ dispatch }: MiddlewareAPI, { payload }: Action) => {
        const { prefix } = this.options;

        const transport = HttpTransportType.WebSockets | HttpTransportType.LongPolling;

        this.connection = new HubConnectionBuilder()
            .configureLogging(LogLevel.Trace)
            .withUrl(payload.url, {
                accessTokenFactory: () => payload.accessToken,
                transport,
            })
            .build();

        this.connection.start().then(() => dispatch(actions.connected(prefix)));
    };

    send = (_store: MiddlewareAPI, { payload }: Action) => {
        if (this.connection === null) {
            throw new Error('SignalR is not inialized. Dispatch SIGNALR_CONNECT first.');
        }

        this.connection.send(payload.methodName, ...payload.args);
    };

    disconnect = (_store: MiddlewareAPI, _action: Action) => {
        if (this.connection === null) {
            throw new Error('SignalR is not inialized. Dispatch SIGNALR_CONNECT first.');
        }

        this.connection.stop();
        this.connection = null;
    };

    addHandler = ({ dispatch }: MiddlewareAPI, { payload }: Action) => {
        if (this.connection === null) {
            throw new Error('SignalR is not inialized. Dispatch SIGNALR_CONNECT first.');
        }

        const { prefix } = this.options;

        const methodNames: string[] = payload;
        for (const methodName of methodNames) {
            this.connection.on(methodName, (...args) =>
                this.handleMessage(dispatch, prefix, methodName, args)
            );
        }
    };

    removeHandler = (_store: MiddlewareAPI, { payload }: Action) => {
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
