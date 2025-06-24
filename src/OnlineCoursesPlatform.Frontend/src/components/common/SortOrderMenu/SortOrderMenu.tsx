import React, { useState } from "react";
import { Box, Button, useTheme } from "@mui/material";
import BasicMenu from "../BasicMenu/BasicMenu";
import { HiSortDescending } from "react-icons/hi";

const SortOrderMenu: React.FC = () => {
  const menuItems: { id: number; label: string; icon?: React.ReactNode }[] = [
    { id: 0, label: "Most Popular", icon: <HiSortDescending size={18} /> },
    { id: 1, label: "Last Interacted", icon: <HiSortDescending size={18} /> },
    { id: 2, label: "Last Created", icon: <HiSortDescending size={18} /> },
    { id: 3, label: "Last Modified", icon: <HiSortDescending size={18} /> },
    { id: 4, label: "Last Visited", icon: <HiSortDescending size={18} /> },
    { id: 5, label: "Longest", icon: <HiSortDescending size={18} /> },
    { id: 6, label: "Shortest", icon: <HiSortDescending size={18} /> },
  ];

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selected, setSelected] = useState(menuItems[0]);

  const theme = useTheme();

  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleSelect = (item: (typeof menuItems)[number]) => {
    setSelected(item);
    handleClose();
  };

  return (
    <>
      <Box>
        <Button
          variant="outlined"
          size="small"
          endIcon={<HiSortDescending />}
          onClick={handleClick}
          sx={{
            height: "100%",
            whiteSpace: "nowrap",
            border: "1px solid #33363E",
            borderRadius: 1,
            color: open
              ? theme.palette.button.hover
              : theme.palette.button.main,
            backgroundColor: open ? "#1c1c1c" : "transparent",
            "&:hover": {
              color: theme.palette.button.hover,
              backgroundColor: "#1c1c1c",
            },
          }}
        >
          {selected.label}
        </Button>
      </Box>
      <BasicMenu
        anchorEl={anchorEl}
        open={open}
        handleClose={handleClose}
        menuItems={menuItems}
        onSelect={handleSelect}
      />
    </>
  );
};

export default SortOrderMenu;
