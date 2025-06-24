import {
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { NavLink } from "react-router-dom";
import { useTheme } from "@mui/material/styles";

interface NavbarItemProps {
  id: number;
  icon: React.ReactNode;
  label: string;
  route: string;
}

const NavbarItem = ({ id, icon, label, route }: NavbarItemProps) => {
  const theme = useTheme();

  return (
    <ListItem key={id} disablePadding sx={{ justifyContent: "center" }}>
      <ListItemButton
        component={NavLink}
        to={route}
        sx={{
          width: "80%", // или '80%' — кнопка не на весь ListItem
          borderRadius: "12px", // округлённые углы
          mx: "auto", // центрирует по горизонтали
          mr: 1,
          ml: 1,
          color: theme.palette.text.primary,
          "&.active": {
            backgroundColor: theme.palette.action.selected,
            color: theme.palette.text.secondary,
            "& .MuiListItemIcon-root": {
              color: theme.palette.text.secondary,
            },
          },
          "&:hover": {
            backgroundColor: theme.palette.action.hover,
            color: theme.palette.text.secondary,
            "& .MuiListItemIcon-root": {
              color: theme.palette.text.secondary,
            },
          },
        }}
      >
        <ListItemIcon
          sx={{
            color: theme.palette.text.primary,
          }}
        >
          {icon}
        </ListItemIcon>
        <ListItemText primary={label} />
      </ListItemButton>
    </ListItem>
  );
};

export default NavbarItem;
