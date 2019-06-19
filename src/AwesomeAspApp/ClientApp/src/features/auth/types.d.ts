declare module 'MyModels' {
   export type AccessInfo = Readonly<{
      accessToken: string;
      refreshToken: string;
   }>;

   export interface SignInRequest {
      username: string;
      password: string;
      rememberMe: boolean;
   }
}
