declare module 'MyModels' {
   export type AccessInfo = {
      accessToken: string;
      refreshToken: string;
   };

   export interface SignInRequest {
      username: string;
      password: string;
      rememberMe: boolean;
   };
}
