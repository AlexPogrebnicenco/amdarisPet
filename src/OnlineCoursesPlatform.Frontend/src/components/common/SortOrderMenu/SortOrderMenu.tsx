import React, { useState } from "react";
import { Box, Button, Menu, MenuItem, useTheme } from "@mui/material";
import { HiSortDescending } from "react-icons/hi";

interface SortOrderMenuProps {
  onChange: (value: string) => void;
  defaultValue?: string;
}

const SortOrderMenu: React.FC<SortOrderMenuProps> = ({
  onChange,
  defaultValue,
}) => {
  const menuItems: {
    id: number;
    label: string;
    value: string;
    icon?: React.ReactNode;
  }[] = [
    {
      id: 0,
      label: "Last Created",
      value: "lastCreated",
      icon: <HiSortDescending size={18} />,
    },
    {
      id: 1,
      label: "Most Popular",
      value: "mostPopular",
      icon: <HiSortDescending size={18} />,
    },

    {
      id: 2,
      label: "Last Modified",
      value: "lastModified",
      icon: <HiSortDescending size={18} />,
    },
    {
      id: 3,
      label: "Longest",
      value: "longest",
      icon: <HiSortDescending size={18} />,
    },
    {
      id: 4,
      label: "Shortest",
      value: "shortest",
      icon: <HiSortDescending size={18} />,
    },
  ];

  const defaultItem =
    menuItems.find((i) => i.value === defaultValue) || menuItems[0];

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selected, setSelected] = useState(defaultItem);
  const theme = useTheme();

  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    if (anchorEl) {
      handleClose();
    } else {
      setAnchorEl(event.currentTarget);
    }
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleSelect = (item: (typeof menuItems)[number]) => {
    setSelected(item);
    onChange(item.value); // Оповещаю родителя
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

      <Menu
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        disableScrollLock
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "center",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "center",
        }}
        PaperProps={{
          sx: {
            mt: 1,
            backgroundColor: theme.palette.background.paper,
            color: theme.palette.text.primary,
          },
        }}
      >
        {menuItems.map((item) => (
          <MenuItem
            key={item.id}
            onClick={() => handleSelect(item)}
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
                gap: 1,
              }}
            >
              <HiSortDescending size={18} />
              {item.label}
            </Box>
          </MenuItem>
        ))}
      </Menu>
    </>
  );
};

export default SortOrderMenu;
