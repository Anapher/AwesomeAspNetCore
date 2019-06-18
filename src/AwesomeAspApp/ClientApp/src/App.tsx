import { createMuiTheme, CssBaseline } from '@material-ui/core';
import { ThemeProvider } from '@material-ui/styles';
import { RootState } from 'awesome-asp-app';
import React from 'react';
import { connect } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import AnonymousRoutes from './routes/anonymous';
import AuthenticatedRoutes from './routes/authenticated';

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
            {isAuthenticated ? <AuthenticatedRoutes /> : <AnonymousRoutes />}
         </BrowserRouter>
      </ThemeProvider>
   );
}

export default connect(mapStateToProps)(App);
