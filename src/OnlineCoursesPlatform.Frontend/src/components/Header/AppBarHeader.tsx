import { AppBar, Box, IconButton, Toolbar } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { useTheme } from "@mui/material/styles";
import SearchBar from "../common/SearchBar/SearchBar";
import HeaderActions from "./HeaderActions";

interface AppBarHeaderProps {
   handleDrawerToggle: () => void;
  drawerWidth: number;
  mobileMenuId: string;
  isMobileMenuOpen: boolean;
  onMobileMenuOpen: () => void;
  onMobileMenuClose: () => void;
}

const AppBarHeader = ({
   handleDrawerToggle,
  drawerWidth,
  mobileMenuId,
  isMobileMenuOpen,
  onMobileMenuOpen,
  onMobileMenuClose,
}: AppBarHeaderProps) => {
  const theme = useTheme();

  return (
    <AppBar
      position="fixed"
      sx={{
        zIndex: 1500,
        width: { sm: `calc(100% - ${drawerWidth}px)` },
        ml: { sm: `${drawerWidth}px` },
        backgroundColor: theme.palette.background.default,
        color: theme.palette.text.secondary,
      }}
    >
      <Toolbar
        sx={{
          display: "flex",
          justifyContent: "space-between",
          position: "relative",
        }}
      >
        <Box sx={{ display: "flex", alignItems: "center" }}>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerToggle}
            sx={{
              position: "absolute",
              left: 0,
              ml: 1,
              display: { sm: "none" },
            }}
          >
            <MenuIcon />
          </IconButton>
        </Box>

        <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
          {/* <SearchBar /> */}
          <HeaderActions
            mobileMenuId={mobileMenuId}
            isMobileMenuOpen={isMobileMenuOpen}
            onMobileMenuOpen={onMobileMenuOpen}
            onMobileMenuClose={onMobileMenuClose}
          />
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default AppBarHeader;
