import type { ReactNode } from 'react';
import { AppBar, Toolbar, Container, IconButton, Typography, Box } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { Button } from '@mui/material';

interface MainLayoutProps {
  children: ReactNode;
}

const MainLayout = ({ children }: MainLayoutProps) => {
  return (
    <>
      <AppBar position="fixed">
        <Container fixed>
            <Toolbar>
                <IconButton edge="start" 
                color="inherit" aria-label='menu'>
                    <MenuIcon />
                </IconButton>
                <Typography variant='h6'>Online Courses Platform</Typography>
                <Box mr={3}>
                    <Button color="inherit" variant="outlined">Log In</Button>
                </Box>
                <Button color="secondary" variant="contained">Sign Up</Button>
            </Toolbar>
        </Container>
      </AppBar>

      <Container sx={{ mt: 4 }}>
        {children}
      </Container>
    </>
  );
};

export default MainLayout;
