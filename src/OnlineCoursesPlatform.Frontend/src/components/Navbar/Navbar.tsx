import { Box, Drawer } from "@mui/material";
import { useTheme } from "@mui/material/styles";
import NavbarContent from "./NavbarContent";

type NavbarProps = {
  drawerWidth: number;
  mobileOpen: boolean;
  handleDrawerTransitionEnd: () => void;
  handleDrawerClose: () => void;
};

const Navbar = ({ drawerWidth, mobileOpen, handleDrawerTransitionEnd, handleDrawerClose }: NavbarProps) => {
  const theme = useTheme();

  return (
    <Box component="nav" sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }} aria-label="navigation">
      <Drawer
        variant="temporary"
        open={mobileOpen}
        onTransitionEnd={handleDrawerTransitionEnd}
        onClose={handleDrawerClose}
        sx={{
          display: { xs: "block", sm: "none" },
          "& .MuiDrawer-paper": {
            boxSizing: "border-box",
            width: drawerWidth,
            backgroundColor: theme.palette.background.default,
          },
        }}
        slotProps={{ root: { keepMounted: true } }}
      >
        <NavbarContent />
      </Drawer>

      <Drawer
        variant="permanent"
        sx={{
          display: { xs: "none", sm: "block" },
          "& .MuiDrawer-paper": {
            boxSizing: "border-box",
            width: drawerWidth,
            backgroundColor: theme.palette.background.default,
          },
        }}
        open
      >
        <NavbarContent />
      </Drawer>
    </Box>
  );
};

export default Navbar;
