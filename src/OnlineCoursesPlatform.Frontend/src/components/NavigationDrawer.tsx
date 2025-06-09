import { useState, type MouseEvent } from 'react';
import {
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemButton,
  IconButton,
  Divider,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import { Link as RouterLink } from 'react-router-dom';

interface RouteItem {
  path: string;
  label: string;
}

const NavigationDrawer = () => {
  const [open, setOpen] = useState<boolean>(false);

  const toggleDrawer = (state: boolean) => (event: MouseEvent) => {
    setOpen(state);
  };

  const routes: RouteItem[] = [
    { path: '/login', label: 'Login' },
    { path: '/courses', label: 'Courses' },
    // добавь свои пункты тут
  ];

  return (
    <>
      <IconButton edge="start" color="inherit" onClick={toggleDrawer(true)}>
        <MenuIcon />
      </IconButton>

      <Drawer anchor="left" open={open} onClose={toggleDrawer(false)}>
        <List sx={{ width: 250 }}>
          {routes.map(({ path, label }) => (
            <ListItem key={path} disablePadding>
  <ListItemButton
    component={RouterLink}
    to={path}
    onClick={toggleDrawer(false)}
  >
    <ListItemText primary={label} />
  </ListItemButton>
</ListItem>
          ))}
          <Divider />
        </List>
      </Drawer>
    </>
  );
};

export default NavigationDrawer;
