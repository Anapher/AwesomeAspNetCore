import { RootAction, RootState, Services } from 'awesome-asp-app';
import { AxiosError } from 'axios';
import { Epic } from 'redux-observable';
import { from, of } from 'rxjs';
import { catchError, filter, map, mapTo, switchMap } from 'rxjs/operators';
import * as signalr from 'src/store/signalr';
import toErrorResult from 'src/utils/error-result';
import { isActionOf } from 'typesafe-actions';
import * as actions from './actions';

export const signInEpic: Epic<RootAction, RootAction, RootState, Services> = (
   action$,
   _,
   { api },
) =>
   action$.pipe(
      filter(isActionOf(actions.signInAsync.request)),
      switchMap(({ payload: { username, password } }) =>
         from(api.auth.signIn(username, password)).pipe(
            map(response => actions.signInAsync.success(response)),
            catchError((error: AxiosError) =>
               of(actions.signInAsync.failure(toErrorResult(error))),
            ),
         ),
      ),
   );

// its very important that SignalR disconnected on sign out, because when a different user signs in, it might still run with the
// auth token from the previous user -> very bad
export const signOutEpic: Epic<RootAction, RootAction, RootState, Services> = action$ =>
   action$.pipe(
      filter(isActionOf(actions.signOut)),
      mapTo(signalr.disconnect()) as any,
   );
