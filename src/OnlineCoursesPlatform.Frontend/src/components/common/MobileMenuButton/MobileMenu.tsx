// src/components/common/MobileMenu/MobileMenu.tsx
import React from "react";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import { useTheme } from "@mui/material/styles";

interface MobileMenuProps {
  anchorEl: null | HTMLElement;
  open: boolean;
  onClose: () => void;
  children: React.ReactNode[];
}

const MobileMenu: React.FC<MobileMenuProps> = ({
  anchorEl,
  open,
  onClose,
  children,
}) => {
  const theme = useTheme();

  return (
    <Menu
      id="mobile-menu"
      anchorEl={anchorEl}
      open={open}
      onClose={onClose}
      slotProps={{
        paper: {
          sx: {
            backgroundColor: theme.palette.background.paper,
            color: theme.palette.text.primary,
          },
        },
      }}
    >
      {children.map((child, index) => (
        <MenuItem
          key={index}
          disableRipple
          sx={{
            color: theme.palette.text.primary,
            "&:hover": {
              color: theme.palette.text.secondary,
              backgroundColor: theme.palette.action.selected,
            },
          }}
        >
          {child}
        </MenuItem>
      ))}
    </Menu>
  );
};

export default MobileMenu;
