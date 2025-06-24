import React from "react";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import { useTheme } from "@mui/material/styles";
import { Box } from "@mui/material";

interface BasicMenuProps {
  anchorEl: null | HTMLElement;
  open: boolean;
  handleClose: () => void;
  menuItems: { id: number; label: string; icon?: React.ReactNode }[];
  onSelect: (item: {
    id: number;
    label: string;
    icon?: React.ReactNode;
  }) => void;
}

const BasicMenu: React.FC<BasicMenuProps> = ({
  anchorEl,
  handleClose,
  open,
  menuItems,
  onSelect,
}) => {
  const theme = useTheme();

  return (
    <Menu
      id="basic-menu"
      anchorEl={anchorEl}
      open={open}
      onClose={handleClose}
      slotProps={{
        paper: {
          sx: {
            backgroundColor: theme.palette.background.paper, // светлый фон из темы
            color: theme.palette.text.primary,
          },
        },
      }}
    >
      {menuItems.map((item) => (
        <MenuItem
          key={item.id}
          onClick={() => {
            onSelect(item); // вызвать выбор
            handleClose(); // закрыть меню
          }}
          sx={{
            color: "#8A96A5",
            mx: 0.5,
            borderRadius: 1,
            "&:hover": {
              backgroundColor: "#2F333A",
            },
          }}
        >
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              gap: 1,
            }}
          >
            {item.icon}
            {item.label}
          </Box>
        </MenuItem>
      ))}
    </Menu>
  );
};

export default BasicMenu;
