declare module 'MyModels' {
   export type AccessInfo = {
      accessToken: string;
      refreshToken: string;
   };

   export type SignInRequest = {
      username: string;
      password: string;
      rememberMe: boolean;
   };
}
