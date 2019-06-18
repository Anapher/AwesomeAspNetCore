import Axios from 'axios';
import { AccessInfo } from 'MyModels';

export async function signIn(
   username: string,
   password: string,
): Promise<AccessInfo> {
   const response = await Axios.post<AccessInfo>('/api/v1/auth/login', {
      username,
      password,
   });

   return response.data;
}

export async function refreshToken(access: AccessInfo): Promise<AccessInfo> {
   const response = await Axios.post<AccessInfo>(
      '/api/v1/auth/refreshtoken',
      access,
   );
   return response.data;
}
