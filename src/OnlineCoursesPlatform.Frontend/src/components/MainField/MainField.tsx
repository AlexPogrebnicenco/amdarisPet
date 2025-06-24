import React from "react";
import { Box, Toolbar, Typography, useTheme } from "@mui/material";
import { Outlet, useLocation } from "react-router-dom";
import { mainNavbarItems } from "../Navbar/consts/navbarItems";

interface MainFieldProps {
  drawerWidth: number;
}

const MainField: React.FC<MainFieldProps> = ({ drawerWidth }) => {
  const location = useLocation();
  const theme = useTheme();

  const currentRoute = location.pathname.replace(/^\/+/, "");
  const currentItem = mainNavbarItems.find((item) => item.route === currentRoute);

  const title = currentItem?.label;
  const description = currentItem?.description;
  const TabsComponent = currentItem?.tabsComponent;

  return (
    <Box
      component="main"
      sx={{
        flexGrow: 1,
        p: 3,
        width: { xs: "100%", sm: `calc(100% - ${drawerWidth}px)` },
      }}
    >
      <Toolbar />

      {title && (
        <Typography
          variant="h4"
          gutterBottom
          sx={{ color: theme.palette.text.secondary }}
        >
          {title}
        </Typography>
      )}

      {description && (
        <Box sx={{ textAlign: "left", mb: 2 }}>
          <Typography variant="body1" sx={{ color: theme.palette.text.primary }}>
            {description}
          </Typography>
        </Box>
      )}

      {TabsComponent && <TabsComponent />}

      <Box sx={{ mt: 1 }}>
        <Outlet />
      </Box>
    </Box>
  );
};

export default MainField;
