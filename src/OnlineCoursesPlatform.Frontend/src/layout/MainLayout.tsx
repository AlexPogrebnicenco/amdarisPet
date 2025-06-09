import type { ReactNode } from 'react';
import { AppBar, Toolbar, Container, Typography, Box } from '@mui/material';
import { Button } from '@mui/material';
import NavigationDrawer from '../components/NavigationDrawer';


interface MainLayoutProps {
  children: ReactNode;
}

const MainLayout = ({ children }: MainLayoutProps) => {
  return (
    <>
      <AppBar position="fixed">
  <Container fixed>
    <Toolbar>
      <NavigationDrawer /> {/* ← иконка меню */}
      <Typography variant='h6' sx={{ flexGrow: 1, marginLeft: 2 }}>
        Online Courses Platform
      </Typography>
      <Box mr={2}>
        <Button color="inherit" variant="outlined">Log In</Button>
      </Box>
      <Button color="secondary" variant="contained">Sign Up</Button>
    </Toolbar>
  </Container>
</AppBar>


      <Container sx={{ mt: 10 }}>
  {children}
</Container>

    </>
  );
};

export default MainLayout;
