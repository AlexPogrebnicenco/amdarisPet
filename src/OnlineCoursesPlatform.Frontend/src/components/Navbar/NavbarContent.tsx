import {
  AppBar,
  Divider,
  List,
  Toolbar,
  Typography,
  useTheme,
} from "@mui/material";
import { mainNavbarItems } from "./consts/navbarItems";
import NavbarItem from "./NavbarItem";
import { teacherNavbarItems } from "./consts/teacherNavbarItems";
import { adminNavbarItems } from "./consts/adminNavbaritems";

const NavbarContent = () => {
  const theme = useTheme();
  return (
    <>
      <Toolbar sx={{ justifyContent: "center" }}>
        <Typography
          variant="h6"
          noWrap
          component="div"
          sx={{
            fontFamily: '"Audiowide", sans-serif',
            color: theme.palette.text.secondary,
          }}
        >
          /OCP\
        </Typography>
      </Toolbar>
      <Divider
        variant="middle"
        sx={{ backgroundColor: theme.palette.divider }}
      />

      {/* Main fields */}
      <List>
        {mainNavbarItems.map((item) => (
          <NavbarItem key={item.id} {...item} />
        ))}
      </List>

      <Divider
        variant="middle"
        sx={{ backgroundColor: theme.palette.divider }}
      />

      {/* Teacher's fields */}
      <List>
        {teacherNavbarItems.map((item) => (
          <NavbarItem key={item.id} {...item} />
        ))}
      </List>

      <Divider
        variant="middle"
        sx={{ backgroundColor: theme.palette.divider }}
      />

      <List>
        {adminNavbarItems.map((item) => (
          <NavbarItem key={item.id} {...item} />
        ))}
      </List>
    </>
  );
};

export default NavbarContent;
