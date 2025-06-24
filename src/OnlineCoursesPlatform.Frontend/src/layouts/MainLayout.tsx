import React, { useState } from "react";
import Box from "@mui/material/Box";
import { CssBaseline } from "@mui/material";
import Navbar from "../components/Navbar/Navbar";
import AppBarHeader from "../components/Header/AppBarHeader";
import MainField from "../components/MainField/MainField";
import HideOnScroll from "../components/common/HideOnScroll/HideOnScroll";

const drawerWidth = 220;

const MainLayout: React.FC = () => {
  const [mobileOpen, setMobileOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [mobileMoreAnchorEl, setMobileMoreAnchorEl] =
    useState<null | HTMLElement>(null);

  const handleDrawerClose = () => {
    setIsClosing(true);
    setMobileOpen(false);
  };

  const handleDrawerTransitionEnd = () => {
    setIsClosing(false);
  };

  const handleDrawerToggle = () => {
    if (!isClosing) {
      setMobileOpen(!mobileOpen);
    }
  };

  const isMobileMenuOpen = Boolean(mobileMoreAnchorEl);
  const menuId = "primary-search-account-menu";
  const mobileMenuId = "primary-search-account-menu-mobile";

  const handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMobileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setMobileMoreAnchorEl(event.currentTarget);
  };

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      {/* <HideOnScroll> */}
       
        <AppBarHeader
          handleDrawerToggle={handleDrawerToggle}
          drawerWidth={drawerWidth}
          isMobileMenuOpen={isMobileMenuOpen}
          mobileMenuId={mobileMenuId}
          menuId={menuId}
          handleProfileMenuOpen={handleProfileMenuOpen}
          handleMobileMenuOpen={handleMobileMenuOpen}
        />
       
      {/* </HideOnScroll> */}
      {/* DRAWER */}
      <Navbar
        drawerWidth={drawerWidth}
        mobileOpen={mobileOpen}
        handleDrawerTransitionEnd={handleDrawerTransitionEnd}
        handleDrawerClose={handleDrawerClose}
      />
      {/* HEADER */}
      <MainField drawerWidth={drawerWidth} />
    </Box>
  );
};

export default MainLayout;
