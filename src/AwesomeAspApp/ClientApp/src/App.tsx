import { createMuiTheme, CssBaseline } from '@material-ui/core';
import { ThemeProvider } from '@material-ui/styles';
import { RootState } from 'awesome-asp-app';
import React from 'react';
import { connect } from 'react-redux';
import { BrowserRouter, Redirect, Route, Switch } from 'react-router-dom';
import AuthRoute from './routes/AuthRoute';
import MainRoute from './routes/MainRoute';

const theme = createMuiTheme({
   palette: {
      type: 'dark',
   },
});

const mapStateToProps = (state: RootState) => ({ isAuthenticated: state.auth.isAuthenticated });

type Props = ReturnType<typeof mapStateToProps>;

function App({ isAuthenticated }: Props) {
   return (
      <ThemeProvider theme={theme}>
         <CssBaseline />
         <BrowserRouter>
            <Switch>
               <Route
                  path="/login"
                  render={() => (!isAuthenticated ? <AuthRoute /> : <Redirect to="/" />)}
               />
               <Route
                  path="/"
                  render={() => (isAuthenticated ? <MainRoute /> : <Redirect to="/login" />)}
               />
            </Switch>
         </BrowserRouter>
      </ThemeProvider>
   );
}

export default connect(mapStateToProps)(App);
