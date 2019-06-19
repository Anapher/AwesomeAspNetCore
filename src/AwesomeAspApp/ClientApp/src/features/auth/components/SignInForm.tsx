import { Box, Button, Grid, LinearProgress, Link, Typography } from '@material-ui/core';
import { Field, Form, Formik, FormikActions } from 'formik';
import { CheckboxWithLabel, TextField } from 'formik-material-ui';
import { SignInRequest } from 'MyModels';
import React, { useCallback } from 'react';
import to from 'src/utils/to';
import useAsyncFunction from 'src/hooks/use-async-function';
import { IRequestErrorResponse } from 'src/utils/error-result';
import * as yup from 'yup';
import * as actions from '../actions';

const schema = yup.object().shape({
   username: yup.string().required(),
   password: yup.string().required(),
   rememberMe: yup.boolean(),
});

const initialValues: SignInRequest = {
   username: '',
   password: '',
   rememberMe: false,
};

export default function SignInForm() {
   const signInAction = useAsyncFunction(
      actions.signInAsync.request,
      actions.signInAsync.success,
      actions.signInAsync.failure,
   );
   const signInCallback = useCallback(
      async (values: SignInRequest, { setSubmitting }: FormikActions<SignInRequest>) => {
         try {
            await signInAction!(values);
            // the view will automatically change when the user is authenticated
         } catch (error) {
            const response = error as IRequestErrorResponse;
         } finally {
            setSubmitting(false);
         }
      },
      [signInAction],
   );

   return (
      <Formik<SignInRequest>
         validationSchema={schema}
         initialValues={initialValues}
         onSubmit={signInCallback}
      >
         {({ isSubmitting, isValid, status }) => (
            <Form>
               <Grid container spacing={2}>
                  <Grid item xs={12}>
                     <Field
                        label="Username"
                        name="username"
                        fullWidth
                        required
                        component={TextField}
                     />
                  </Grid>
                  <Grid item xs={12}>
                     <Field
                        label="Password"
                        name="password"
                        fullWidth
                        type="password"
                        autoComplete="current-password"
                        required
                        component={TextField}
                     />
                  </Grid>
                  <Grid
                     item
                     container
                     alignItems="center"
                     justify="space-between"
                     style={{ padding: 8, paddingTop: 4 }}
                  >
                     <Grid item>
                        <Field
                           name="rememberMe"
                           Label={{
                              label: 'Remember Me',
                           }}
                           component={CheckboxWithLabel}
                        />
                     </Grid>
                     <Grid item>
                        <Typography>
                           <Link {...to('/login/forgot-password')}>Forgot Password</Link>
                        </Typography>
                     </Grid>
                  </Grid>
                  <Grid item xs={12} style={{ padding: 4 }}>
                     {!isSubmitting ? (
                        <Button
                           type="submit"
                           fullWidth
                           variant="contained"
                           disabled={!isValid}
                           color="primary"
                        >
                           Sign in
                        </Button>
                     ) : (
                        <Box mt={1} width="100%">
                           <LinearProgress />
                        </Box>
                     )}
                  </Grid>
                  {status && <Typography color="error">{status}</Typography>}
               </Grid>
            </Form>
         )}
      </Formik>
   );
}
