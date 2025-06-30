import {
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
import RoleBasedRender from "../common/RoleBasedRender/RoleBasedRender";

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
      <RoleBasedRender allowedRoles={['Teacher']}>
        <List>
          {teacherNavbarItems.map((item) => (
            <NavbarItem key={item.id} {...item} />
          ))}
        </List>

        <Divider variant="middle" sx={{ backgroundColor: theme.palette.divider }} />
      </RoleBasedRender>

      {/* Admin fields */}
      <RoleBasedRender allowedRoles={['Admin']}>
        <List>
          {adminNavbarItems.map((item) => (
            <NavbarItem key={item.id} {...item} />
          ))}
        </List>

        <Divider variant="middle" sx={{ backgroundColor: theme.palette.divider }} />
      </RoleBasedRender>
    </>
  );
};

export default NavbarContent;
