import React, { useEffect, useState } from "react";
import Box from "@mui/material/Box";
import { CssBaseline, useMediaQuery } from "@mui/material";
import Navbar from "../components/Navbar/Navbar";
import AppBarHeader from "../components/Header/AppBarHeader";
import MainField from "../components/MainField/MainField";
import HideOnScroll from "../components/common/HideOnScroll/HideOnScroll";
import { useTheme } from "@mui/material/styles";

const drawerWidth = 220;

const MainLayout: React.FC = () => {
  const theme = useTheme();
  const [mobileOpen, setMobileOpen] = useState(false);
  const [isClosing, setIsClosing] = useState(false);
  const isDesktop = useMediaQuery(theme.breakpoints.up("md"));
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);

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

  const handleMobileMenuOpen = () => {
    setIsMobileMenuOpen(true);
  };

  const handleMobileMenuClose = () => {
    setIsMobileMenuOpen(false);
  };

  const handleMobileMenuToggle = () => {
    setIsMobileMenuOpen((prev) => !prev);
  };

  const mobileMenuId = "primary-search-account-menu-mobile";

  useEffect(() => {
    if (isDesktop && isMobileMenuOpen) {
      handleMobileMenuClose();
    }
  }, [isDesktop]);

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      {/* <HideOnScroll> */}

      <AppBarHeader
        handleDrawerToggle={handleDrawerToggle}
        drawerWidth={drawerWidth}
        mobileMenuId={mobileMenuId}
        isMobileMenuOpen={isMobileMenuOpen}
        onMobileMenuOpen={handleMobileMenuToggle}
        onMobileMenuClose={handleMobileMenuClose}
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
