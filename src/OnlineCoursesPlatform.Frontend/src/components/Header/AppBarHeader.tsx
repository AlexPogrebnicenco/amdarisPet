import {
  AppBar,
  Box,
  IconButton,
  Toolbar,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { useTheme } from "@mui/material/styles";
import SearchBar from "../common/SearchBar/SearchBar";
import HeaderActions from "./HeaderActions";

interface AppBarHeaderProps {
  handleDrawerToggle: () => void;
  drawerWidth: number;
  isMobileMenuOpen: boolean;
  mobileMenuId: string;
  menuId: string;
  handleProfileMenuOpen: (event: React.MouseEvent<HTMLElement>) => void;
  handleMobileMenuOpen: (event: React.MouseEvent<HTMLElement>) => void;
}

const AppBarHeader = ({
  handleDrawerToggle,
  drawerWidth,
  isMobileMenuOpen,
  mobileMenuId,
  menuId,
  handleProfileMenuOpen,
  handleMobileMenuOpen,
}: AppBarHeaderProps) => {
  const theme = useTheme();

  return (
    <AppBar
      position="fixed"
      sx={{
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
            // sx={{ mr: 2, display: { sm: "none" } }}
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
          {/* Твоя иконка поиска */}
          <SearchBar />

          {/* Будущая дополнительная иконка */}
          <HeaderActions
            isMobileMenuOpen={isMobileMenuOpen}
            mobileMenuId={mobileMenuId}
            menuId={menuId}
            onProfileMenuOpen={handleProfileMenuOpen}
            onMobileMenuOpen={handleMobileMenuOpen}
          />
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default AppBarHeader;
