import React, { useState } from "react";
import { Button, useTheme } from "@mui/material";
import BasicMenu from "../BasicMenu/BasicMenu";
import { ViewList } from "@mui/icons-material";
import { MdFilterListOff, MdOutlineRestore } from "react-icons/md";

interface ViewMenuButtonProps {
  onClearFilters: () => void;
  onRestoreDefaults: () => void;
}

const ViewMenuButton: React.FC<ViewMenuButtonProps> = ({
  onClearFilters,
  onRestoreDefaults,
}) => {
  const menuItems: { id: number; label: string; icon?: React.ReactNode }[] = [
    { id: 0, label: "Clear Filters", icon: <MdFilterListOff size={18} /> },
    { id: 1, label: "Restore Defaults", icon: <MdOutlineRestore size={18} /> },
  ];

  const theme = useTheme();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selected, setSelected] = useState(menuItems[0]);

  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleSelect = (item: (typeof menuItems)[number]) => {
    setSelected(item);
    if (item.id === 0) onClearFilters();
    if (item.id === 1) onRestoreDefaults();
    handleClose();
  };

  return (
    <>
      <Button
        variant="outlined"
        size="small"
        onClick={handleClick}
        sx={{
          height: "100%",
          minWidth: 0,
          whiteSpace: "nowrap",
          border: "1px solid #33363E",
          borderRadius: 1,
          color: open ? theme.palette.button.hover : theme.palette.button.main,
          backgroundColor: open ? "#1c1c1c" : "transparent",
          "&:hover": {
            color: theme.palette.button.hover,
            backgroundColor: "#1c1c1c",
          },
        }}
      >
        <ViewList />
      </Button>

      <BasicMenu
        anchorEl={anchorEl}
        open={open}
        handleClose={handleClose}
        menuItems={menuItems}
        onSelect={handleSelect}
        disableScrollLock
        sx={{ mt: 1 }}
      />
    </>
  );
};

export default ViewMenuButton;
