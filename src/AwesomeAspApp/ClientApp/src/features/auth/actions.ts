import { AccessInfo, SignInRequest } from 'MyModels';
import { IRequestErrorResponse } from 'src/utils/error-result';
import { createAction, createAsyncAction } from 'typesafe-actions';

export const signInAsync = createAsyncAction(
   'AUTH/SIGNIN_REQUEST',
   'AUTH/SIGNIN_SUCCESS',
   'AUTH/SIGNIN_FAILED',
)<SignInRequest, AccessInfo, IRequestErrorResponse>();

export const signOut = createAction('AUTH/SIGNOUT');
