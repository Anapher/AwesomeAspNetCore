import { FormikActions } from 'formik';
import {
   IRequestErrorResponse,
   isRestError,
   isServerUnavailable,
   toString,
} from 'src/utils/error-result';

/**
 * Apply the error using formik action. If field errors are specified, they will be set using setErrors.
 * Else, the status will be set using setStatus.
 * @param error the response error
 * @param param1 formik actions
 */
export function applyError<T>(
   error: IRequestErrorResponse,
   { setStatus, setErrors }: FormikActions<T>,
) {
   const response = error as IRequestErrorResponse;
   if (
      isServerUnavailable(response) ||
      !isRestError(response.response) ||
      !response.response.fields
   ) {
      setStatus(toString(response));
   } else {
      setStatus(null);
      setErrors(convertMapToObject(response.response.fields) as any);
   }
}

function convertMapToObject<T>(map: Map<string, T>): { [key: string]: T } {
   const obj: { [key: string]: T } = {};

   map.forEach((value, key) => (obj[key] = value));
   return obj;
}
